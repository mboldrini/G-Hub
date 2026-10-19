using System.Text.Json;
using PrDashboard.Config;
using PrDashboard.GitHub;
using PrDashboard.IO;
using PrDashboard.Models;
using PrDashboard.Web;
using Spectre.Console;

var configPath = Path.Combine(AppContext.BaseDirectory, "config.json");
var config = ConfigLoader.Load(configPath);

AppLogger.Initialize(WeeklyFolder.GetPath(Path.GetFullPath(config.OutputDir), DateTime.Now));

var exeName = Path.GetFileNameWithoutExtension(Environment.ProcessPath ?? "");
bool demo = exeName.Contains("demo", StringComparison.OrdinalIgnoreCase);
bool once = false, checkpoint = false;
bool noEmoji = false;
string? intervalArg = null, hostnameArg = null;

for (int i = 0; i < args.Length; i++)
{
    switch (args[i])
    {
        case "--help" or "-h":
            PrintHelp();
            return 0;
        case "--demo":
            demo = true;
            break;

        case "--once":
            once = true;
            break;
        case "--no-emoji":
            noEmoji = true;
            break;
        case "--checkpoint":
            checkpoint = true;
            break;
        case "--interval" when i + 1 < args.Length:
            intervalArg = args[++i];
            break;
        case "--hostname" when i + 1 < args.Length:
            hostnameArg = args[++i];
            break;
    }
}

if (hostnameArg != null) config.Hostname = hostnameArg;
if (intervalArg != null && int.TryParse(intervalArg, out int iv)) config.IntervalSeconds = iv;

_ = noEmoji;

// --demo: web server with fake data, no pre-checks or API calls
if (demo)
{
    using var demoCts = new CancellationTokenSource();
    Console.CancelKeyPress += (_, e) => { e.Cancel = true; demoCts.Cancel(); };
    AppDomain.CurrentDomain.ProcessExit += (_, _) => demoCts.Cancel();
    AnsiConsole.MarkupLine($"[grey]Modo demo — abrindo http://localhost:{config.WebPort} ...[/]");
    var demoClient = new GhApiClient(config.Hostname);
    var demoSvc = new PrsDataService(demoClient, "demo-user", config, demoMode: true);
    await demoSvc.InitAsync(demoCts.Token);
    var demoServer = new WebServer(demoSvc, config);
    _ = Task.Run(async () =>
    {
        await Task.Delay(400);
        try { System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo($"http://localhost:{config.WebPort}") { UseShellExecute = true }); } catch { }
    });
    try { await demoServer.RunAsync(demoCts.Token); }
    catch (OperationCanceledException) { }
    return 0;
}

// Pre-checks
string login;
try
{
    AnsiConsole.MarkupLine("[grey]Verificando pré-requisitos...[/]");
    login = await PreChecks.RunAllAsync(config.Hostname);
    AnsiConsole.MarkupLine($"[grey]Autenticado como [bold]{Markup.Escape(login)}[/][/]");
}
catch (PreCheckException ex)
{
    AnsiConsole.MarkupLine($"[red]Erro de configuração:[/]\n{Markup.Escape(ex.Message)}");
    WaitKeyIfDoubleClick();
    return 1;
}
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]Erro inesperado nos pré-checks:[/]\n{Markup.Escape(ex.Message)}");
    WaitKeyIfDoubleClick();
    return 1;
}

var client = new GhApiClient(config.Hostname);

if (checkpoint)
{
    await RunCheckpointAsync(client, login, config);
    return 0;
}

if (once)
{
    var runner = new OnceRunner(client, login, config);
    using var onceCts = new CancellationTokenSource();
    Console.CancelKeyPress += (_, e) => { e.Cancel = true; onceCts.Cancel(); };
    AppDomain.CurrentDomain.ProcessExit += (_, _) => onceCts.Cancel();
    return await runner.RunAsync(onceCts.Token);
}

// Default: web dashboard
using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => { e.Cancel = true; cts.Cancel(); };
AppDomain.CurrentDomain.ProcessExit += (_, _) => { try { cts.Cancel(); } catch (ObjectDisposedException) { } };

AnsiConsole.MarkupLine($"[grey]Iniciando dashboard web em http://localhost:{config.WebPort} ...[/]");

var dataService = new PrsDataService(client, login, config, demoMode: false);
await dataService.InitAsync(cts.Token);

var webServer = new WebServer(dataService, config);
try { await webServer.RunAsync(cts.Token); }
catch (OperationCanceledException) { /* normal exit via Ctrl+C */ }
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]Erro fatal:[/] {Markup.Escape(ex.Message)}");
    WaitKeyIfDoubleClick();
    return 1;
}

return 0;

static void PrintHelp()
{
    AnsiConsole.Write(new Panel("""
        [bold]PrDashboard[/] — Dashboard de Pull Requests do GitHub

        [bold]Uso:[/]
          PrDashboard [opções]

        [bold]Opções:[/]
          (sem args)          Abre dashboard web em http://localhost:5555
          --once              Busca uma vez, salva arquivos, sai
          --demo              Dashboard web com dados falsos (sem chamar a API)
          --interval <seg>    Intervalo de atualização (sobrescreve config.json)
          --hostname <host>   GitHub Enterprise hostname
          --checkpoint        Salva JSON bruto em fixtures/real/ e exibe na tela
          --help, -h          Exibe esta ajuda

        [bold]Configuração:[/]
          config.json ao lado do executável (webPort: 5555)
        """).Header("[bold]Ajuda[/]"));
}

static void WaitKeyIfDoubleClick()
{
    if (!Console.IsInputRedirected)
    {
        AnsiConsole.WriteLine("\nPressione qualquer tecla para sair...");
        Console.ReadKey(true);
    }
}

async Task RunCheckpointAsync(GhApiClient c, string l, AppConfig cfg)
{
    AnsiConsole.MarkupLine("[bold yellow]CHECKPOINT: buscando PRs reais...[/]");
    List<PullRequest> myPrs;
    List<PullRequest> reviewPrs;
    try
    {
        myPrs = await c.FetchMyOpenPrsAsync(l, cfg);
        reviewPrs = await c.FetchReviewRequestedPrsAsync(l, cfg);
    }
    catch (GhException ex)
    {
        AnsiConsole.MarkupLine($"[red]Erro na API:[/] {Markup.Escape(ex.Message)}");
        return;
    }
    AnsiConsole.MarkupLine($"[green]Meus PRs abertos:[/] {myPrs.Count}");
    AnsiConsole.MarkupLine($"[green]Reviews pedidos a mim:[/] {reviewPrs.Count}");
    var sample = myPrs.Take(3).ToList();
    AnsiConsole.MarkupLine("\n[bold]Amostra (primeiros 3 PRs — dados sensíveis mascarados):[/]");
    foreach (var pr in sample)
    {
        var masked = MaskSensitive(pr);
        AnsiConsole.WriteLine(JsonSerializer.Serialize(masked, new JsonSerializerOptions { WriteIndented = true }));
        AnsiConsole.Write(new Spectre.Console.Rule());
    }
    var fixturesDir = Path.Combine(AppContext.BaseDirectory, "fixtures", "real");
    Directory.CreateDirectory(fixturesDir);
    var outPath = Path.Combine(fixturesDir, $"checkpoint-{DateTime.UtcNow:yyyyMMdd-HHmmss}.json");
    var payload = new { myPrs, reviewPrs };
    await File.WriteAllTextAsync(outPath, JsonSerializer.Serialize(payload, new JsonSerializerOptions { WriteIndented = true }));
    AnsiConsole.MarkupLine($"\n[grey]JSON completo salvo em (ignorado pelo git): {Markup.Escape(outPath)}[/]");
}

static object MaskSensitive(PullRequest pr) => new
{
    repo = pr.Repo,
    number = pr.Number,
    title = MaskTitle(pr.Title),
    url = "https://github.com/***MASKED***",
    isDraft = pr.IsDraft,
    createdAt = pr.CreatedAt,
    updatedAt = pr.UpdatedAt,
    headRef = pr.HeadRef,
    baseRef = pr.BaseRef,
    reviewDecision = pr.ReviewDecision,
    mergeable = pr.Mergeable,
    mergeStateStatus = pr.MergeStateStatus,
    additions = pr.Additions,
    deletions = pr.Deletions,
    changedFiles = pr.ChangedFiles,
    overallCiStatus = pr.OverallCiStatus,
    latestReviewsCount = pr.LatestReviews.Count,
    reviewRequestsCount = pr.ReviewRequests.Count,
    openThreads = pr.ReviewThreads.Count(t => !t.IsResolved),
    lastCommitDate = pr.LastCommit?.CommittedDate,
    statusChecksCount = pr.StatusChecks.Count,
    failingChecks = pr.StatusChecks
        .Where(c => c.Conclusion == "FAILURE" || c.Conclusion == "failure")
        .Select(c => c.Name)
        .ToList(),
};

static string MaskTitle(string title)
{
    if (title.Length <= 4) return "***";
    return title[..2] + new string('*', Math.Min(title.Length - 4, 20)) + title[^2..];
}
