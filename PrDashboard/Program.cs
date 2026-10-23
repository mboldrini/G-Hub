using System.Text.Json;
using System.Windows.Forms;
using PrDashboard.Config;
using PrDashboard.GitHub;
using PrDashboard.IO;
using PrDashboard.Models;
using PrDashboard.UI;
using PrDashboard.Web;
using Spectre.Console;
using SpectrePanel = Spectre.Console.Panel;

var configPath = Path.Combine(AppContext.BaseDirectory, "config.json");
var config = ConfigLoader.Load(configPath);

// ── GUI mode: double-click / no flags ──────────────────────────────────────
if (args.Length == 0)
{
    AppLogger.Initialize(WeeklyFolder.GetPath(Path.GetFullPath(config.OutputDir), DateTime.Now));
    NotificationLogger.Initialize(WeeklyFolder.GetPath(Path.GetFullPath(config.OutputDir), DateTime.Now));

    // SDK Web inicializa o thread principal como MTA; WinForms/WebView2 exige STA.
    var staThread = new Thread(() =>
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        using var cts = new CancellationTokenSource();
        SplashWindow? splash = null;

        async Task RunStartupAsync()
        {
            try
            {
                splash?.UpdateStatus("Checking gh CLI in PATH...", 15);
                var login = await PreChecks.RunAllAsync(config.Hostname, cts.Token);

                splash?.UpdateStatus($"Authenticated as {login} — fetching PRs...", 45);
                var client = new GhApiClient(config.Hostname);
                var dataService = new PrsDataService(client, login, config, demoMode: false);
                await dataService.InitAsync(cts.Token);

                splash?.UpdateStatus("Starting web server...", 80);
                var server = new WebServer(dataService, config, NotificationLogger.Instance, client, login);
                _ = Task.Run(async () =>
                {
                    try { await server.RunAsync(cts.Token, silent: true); }
                    catch (OperationCanceledException) { }
                    catch (Exception ex) { AppLogger.Instance?.Error(ex); }
                });

                splash?.UpdateStatus("Server ready", 95);
                AppLogger.Instance?.Info($"Servidor iniciado — login={login} port={config.WebPort}");
                splash?.SetReady(login, config.WebPort);
            }
            catch (OperationCanceledException) { }
            catch (PreCheckException ex)
            {
                AppLogger.Instance?.Info($"PreCheck falhou: {ex.Message}");
                splash?.ShowError(ex);
            }
            catch (Exception ex)
            {
                AppLogger.Instance?.Error(ex);
                splash?.ShowError(ex.Message);
            }
        }

        splash = new SplashWindow(RunStartupAsync, cts);
        Application.Run(splash);
        cts.Cancel();
    });
    staThread.SetApartmentState(ApartmentState.STA);
    staThread.Name = "GUI";
    staThread.Start();
    staThread.Join();
    return 0;
}

// ── CLI mode: qualquer flag → re-attach ao console do pai ─────────────────
bool attached = NativeMethods.AttachConsole(-1);
if (!attached) NativeMethods.AllocConsole();
try
{
    var cout = new FileStream("CONOUT$", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
    var writer = new StreamWriter(cout) { AutoFlush = true };
    Console.SetOut(writer);
    Console.SetError(writer);
    var cin = new FileStream("CONIN$", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    Console.SetIn(new StreamReader(cin));
}
catch { }

AppLogger.Initialize(WeeklyFolder.GetPath(Path.GetFullPath(config.OutputDir), DateTime.Now));
NotificationLogger.Initialize(WeeklyFolder.GetPath(Path.GetFullPath(config.OutputDir), DateTime.Now));

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
    if (ex.FixCommand != null)
        AnsiConsole.MarkupLine($"[grey]Execute:[/] [bold]{Markup.Escape(ex.FixCommand)}[/]");
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

using var mainCts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) => { e.Cancel = true; mainCts.Cancel(); };
AppDomain.CurrentDomain.ProcessExit += (_, _) => { try { mainCts.Cancel(); } catch (ObjectDisposedException) { } };

AnsiConsole.MarkupLine($"[grey]Iniciando dashboard web em http://localhost:{config.WebPort} ...[/]");

var dataService = new PrsDataService(client, login, config, demoMode: false);
await dataService.InitAsync(mainCts.Token);

var webServer = new WebServer(dataService, config, NotificationLogger.Instance, client, login);
try { await webServer.RunAsync(mainCts.Token); }
catch (OperationCanceledException) { }
catch (Exception ex)
{
    AnsiConsole.MarkupLine($"[red]Erro fatal:[/] {Markup.Escape(ex.Message)}");
    WaitKeyIfDoubleClick();
    return 1;
}

return 0;

static void PrintHelp()
{
    AnsiConsole.Write(new SpectrePanel("""
        [bold]PrDashboard[/] — Dashboard de Pull Requests do GitHub

        [bold]Uso:[/]
          PrDashboard [opções]

        [bold]Opções:[/]
          (sem args)          Abre janela de launcher gráfico
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
