using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PrDashboard.GitHub;
using PrDashboard.IO;
using PrDashboard.Models;

namespace PrDashboard.Web;

public class WebServer(PrsDataService dataService, AppConfig config, NotificationLogger? notificationLogger = null, GhApiClient? client = null, string? login = null)
{
    private static readonly JsonSerializerOptions JsonOpts = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    private static readonly JsonSerializerOptions ReadOpts = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    public async Task RunAsync(CancellationToken ct, bool silent = false)
    {
        var builder = WebApplication.CreateBuilder();
        builder.WebHost.UseUrls($"http://localhost:{config.WebPort}");
        builder.WebHost.SuppressStatusMessages(true);
        builder.Logging.SetMinimumLevel(LogLevel.Warning);
        builder.Services.Configure<Microsoft.Extensions.Hosting.HostOptions>(o =>
            o.ShutdownTimeout = TimeSpan.FromSeconds(3));

        var app = builder.Build();

        app.MapGet("/", () =>
            Results.Content(WebAssets.Html, "text/html; charset=utf-8"));

        app.MapGet("/api/prs", () =>
        {
            var data = dataService.Cached ?? new PrsApiResponse { IsLoading = true, Language = config.Language, Theme = config.Theme };
            return Results.Json(data, JsonOpts);
        });

        app.MapPost("/api/refresh", async () =>
        {
            await dataService.RefreshAsync(ct);
            var data = dataService.Cached ?? new PrsApiResponse { IsLoading = true, Language = config.Language, Theme = config.Theme };
            return Results.Json(data, JsonOpts);
        });

        app.MapPost("/api/settings", async (HttpContext ctx) =>
        {
            using var reader = new System.IO.StreamReader(ctx.Request.Body);
            var json = await reader.ReadToEndAsync();
            var body = JsonSerializer.Deserialize<SettingsBody>(json, ReadOpts);
            if (body == null) return Results.BadRequest();

            if (body.Theme is "dark" or "light") config.Theme = body.Theme;
            if (body.Language is "pt-BR" or "en-US") config.Language = body.Language;
            if (body.CollapsedRepos is not null) config.CollapsedRepos = body.CollapsedRepos;
            if (body.MyPrsFilter is not null) config.MyPrsFilter = body.MyPrsFilter;
            if (body.MyPrsView is "repo" or "status") config.MyPrsView = body.MyPrsView;
            if (body.SidebarOpen is not null) config.SidebarOpen = body.SidebarOpen.Value;
        if (body.RepoOrder is not null) config.RepoOrder = body.RepoOrder;
            if (body.NotifCommentVisual is not null) config.NotifCommentVisual = body.NotifCommentVisual.Value;
            if (body.NotifCommentAudio is not null) config.NotifCommentAudio = body.NotifCommentAudio.Value;
            if (body.NotifCiVisual is not null) config.NotifCiVisual = body.NotifCiVisual.Value;
            if (body.NotifCiAudio is not null) config.NotifCiAudio = body.NotifCiAudio.Value;
            if (body.NotifMergedVisual is not null) config.NotifMergedVisual = body.NotifMergedVisual.Value;
            if (body.NotifMergedAudio is not null) config.NotifMergedAudio = body.NotifMergedAudio.Value;
            if (body.NotifApprovedVisual is not null) config.NotifApprovedVisual = body.NotifApprovedVisual.Value;
            if (body.NotifApprovedAudio is not null) config.NotifApprovedAudio = body.NotifApprovedAudio.Value;
            if (body.NotifReadyVisual is not null) config.NotifReadyVisual = body.NotifReadyVisual.Value;
            if (body.NotifReadyAudio is not null) config.NotifReadyAudio = body.NotifReadyAudio.Value;
            if (body.IntervalSeconds is int iv && iv >= 30 && iv <= 3600) config.IntervalSeconds = iv;
            if (body.HideBotReviews is not null) config.HideBotReviews = body.HideBotReviews.Value;

            dataService.RemapCached(config.Language);

            _ = Task.Run(() => SaveConfigAsync());

            var data = dataService.Cached ?? new PrsApiResponse { IsLoading = true, Language = config.Language, Theme = config.Theme };
            return Results.Json(data, JsonOpts);
        });

        app.MapPost("/api/notify-log", async (HttpContext ctx) =>
        {
            if (notificationLogger == null) return Results.NoContent();
            using var reader = new System.IO.StreamReader(ctx.Request.Body);
            var json = await reader.ReadToEndAsync();
            var body = JsonSerializer.Deserialize<NotifyLogBody>(json, ReadOpts);
            if (body == null) return Results.BadRequest();
            notificationLogger.Log(body.Type, body.Repo, body.PrNumber, body.PrTitle);
            return Results.NoContent();
        });

        app.MapPost("/api/backfill/start", async (HttpContext ctx) =>
        {
            if (client == null || login == null)
                return Results.Problem("Backfill não disponível no modo demo.", statusCode: 503);

            using var reader = new System.IO.StreamReader(ctx.Request.Body);
            var json = await reader.ReadToEndAsync();
            var body = JsonSerializer.Deserialize<BackfillStartBody>(json, ReadOpts);
            if (body?.Month is not { Length: > 0 } month)
                return Results.BadRequest("Campo 'month' obrigatório (ex: 2026-10)");

            if (!DateOnly.TryParseExact($"{month}-01", "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out var startDate))
                return Results.BadRequest("Formato inválido. Use YYYY-MM.");

            var jobId = Guid.NewGuid().ToString("N")[..8];
            var progress = new BackfillProgress();
            _backfillJobs[jobId] = progress;

            _ = Task.Run(() => RunBackfillAsync(jobId, progress, startDate, ct));

            return Results.Json(new { jobId }, JsonOpts);
        });

        app.MapGet("/api/backfill/status/{id}", (string id) =>
        {
            if (!_backfillJobs.TryGetValue(id, out var prog))
                return Results.NotFound();

            lock (prog.Lock)
            {
                return Results.Json(new
                {
                    lines = prog.Lines.ToArray(),
                    current = prog.Current,
                    total = prog.Total,
                    done = prog.Done,
                    error = prog.Error,
                }, JsonOpts);
            }
        });

        app.MapGet("/favicon.svg", () =>
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory;
            var path = Path.Combine(exeDir, "github.svg");
            return File.Exists(path) ? Results.File(path, "image/svg+xml") : Results.NotFound();
        });

        app.MapGet("/audio/{file}", (string file) =>
        {
            if (file != "message.mp3" && file != "error.mp3" && file != "done.mp3")
                return Results.NotFound();
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory;
            var path = Path.Combine(exeDir, file);
            return File.Exists(path) ? Results.File(path, "audio/mpeg") : Results.NotFound();
        });

        app.Lifetime.ApplicationStarted.Register(() =>
        {
            if (!silent)
            {
                var url = $"http://localhost:{config.WebPort}";
                Console.WriteLine($"Dashboard rodando em {url}");
                Console.WriteLine("Pressione CTRL+C para encerrar.");
                try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
                catch { }
            }
        });

        dataService.StartBackgroundRefresh(ct);
        await app.RunAsync(ct);
        Console.WriteLine("Encerrado.");
    }

    private async Task SaveConfigAsync()
    {
        try
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory;
            var configPath = Path.Combine(exeDir, "config.json");
            var json = JsonSerializer.Serialize(config, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            await File.WriteAllTextAsync(configPath, json);
        }
        catch { }
    }

    private readonly ConcurrentDictionary<string, BackfillProgress> _backfillJobs = new();

    private sealed class BackfillProgress
    {
        public List<string> Lines { get; } = [];
        public int Current { get; set; }
        public int Total { get; set; }
        public bool Done { get; set; }
        public string? Error { get; set; }
        public readonly object Lock = new();
    }

    private async Task RunBackfillAsync(string jobId, BackfillProgress prog, DateOnly startDate, CancellationToken ct)
    {
        try
        {
            var lastDay = startDate.AddMonths(1).AddDays(-1);
            var monthFolder = startDate.ToString("yyyy-MM");
            var store = new SnapshotStore(Path.GetFullPath(config.OutputDir));

            // ── Phase 1: merged PRs ──────────────────────────────────
            lock (prog.Lock) { prog.Lines.Add($"🔄 Buscando PRs mergeados em {monthFolder}..."); }

            List<PrSnapshot> mergedSnapshots;
            try { mergedSnapshots = await client!.FetchSnapshotsInRangeAsync(login!, startDate, lastDay, ct); }
            catch (Exception ex)
            {
                lock (prog.Lock) { prog.Lines.Add($"❌ Erro: {ex.Message}"); prog.Error = ex.Message; prog.Done = true; }
                return;
            }

            // ── Phase 2: open PRs ────────────────────────────────────
            lock (prog.Lock) { prog.Lines.Add($"🔄 Buscando PRs abertos atualmente..."); }

            List<PullRequest> openPrs;
            try { openPrs = await client!.FetchMyOpenPrsAsync(login!, config, ct); }
            catch (Exception ex)
            {
                lock (prog.Lock) { prog.Lines.Add($"⚠️ Não foi possível buscar PRs abertos: {ex.Message}"); }
                openPrs = [];
            }

            lock (prog.Lock)
            {
                prog.Lines.Add($"✓ {mergedSnapshots.Count} mergeado(s), {openPrs.Count} aberto(s). Salvando...");
                prog.Total = mergedSnapshots.Count + openPrs.Count;
            }

            // ── Save merged ──────────────────────────────────────────
            int mSaved = 0, mSkipped = 0;
            foreach (var snap in mergedSnapshots)
            {
                ct.ThrowIfCancellationRequested();
                if (store.ExistsMerged(snap.Repo, snap.Number, monthFolder))
                {
                    mSkipped++;
                    lock (prog.Lock) { prog.Lines.Add($"→ {snap.Repo} #{snap.Number}: já existe, pulado."); prog.Current++; }
                    continue;
                }
                store.SaveMerged(snap);
                mSaved++;
                lock (prog.Lock) { prog.Lines.Add($"✓ {snap.Repo} #{snap.Number}: \"{Trunc(snap.Title)}\" — salvo."); prog.Current++; }
            }

            // ── Save open ────────────────────────────────────────────
            foreach (var pr in openPrs)
            {
                ct.ThrowIfCancellationRequested();
                store.SaveOpen(new PrSnapshot
                {
                    Repo = pr.Repo, Number = pr.Number, Title = pr.Title, Body = pr.Body,
                    Url = pr.Url, Author = pr.Author ?? "", HeadRef = pr.HeadRef, BaseRef = pr.BaseRef,
                    Additions = pr.Additions, Deletions = pr.Deletions, ChangedFiles = pr.ChangedFiles,
                    CreatedAt = pr.CreatedAt, GeneratedAt = DateTimeOffset.UtcNow,
                    Reviewers = pr.LatestReviews.Select(r => r.Author).ToList(),
                    JiraTickets = IO.RepoCacheStore.ExtractJiraTickets(pr.Body),
                });
                lock (prog.Lock) { prog.Lines.Add($"✓ {pr.Repo} #{pr.Number}: \"{Trunc(pr.Title)}\" (aberto) — salvo."); prog.Current++; }
            }

            lock (prog.Lock)
            {
                var parts = new List<string>();
                if (mSaved > 0) parts.Add($"{mSaved} mergeado(s) salvo(s)");
                if (mSkipped > 0) parts.Add($"{mSkipped} já existia(m)");
                if (openPrs.Count > 0) parts.Add($"{openPrs.Count} aberto(s) salvo(s)");
                prog.Lines.Add($"✅ Concluído! {string.Join(", ", parts)}.");
                prog.Done = true;
            }

            _ = Task.Delay(TimeSpan.FromMinutes(5)).ContinueWith(t => { _backfillJobs.TryRemove(jobId, out _); });
        }
        catch (OperationCanceledException)
        {
            lock (prog.Lock) { prog.Lines.Add("⚠️ Operação cancelada."); prog.Done = true; }
        }
        catch (Exception ex)
        {
            lock (prog.Lock) { prog.Lines.Add($"❌ Erro inesperado: {ex.Message}"); prog.Error = ex.Message; prog.Done = true; }
        }
    }

    private static string Trunc(string s) => s.Length <= 60 ? s : s[..60] + "…";

    private record SettingsBody(
        string? Theme, string? Language,
        List<string>? CollapsedRepos, string? MyPrsFilter,
        string? MyPrsView, bool? SidebarOpen, List<string>? RepoOrder,
        bool? NotifCommentVisual, bool? NotifCommentAudio,
        bool? NotifCiVisual, bool? NotifCiAudio,
        bool? NotifMergedVisual, bool? NotifMergedAudio,
        bool? NotifApprovedVisual, bool? NotifApprovedAudio,
        bool? NotifReadyVisual, bool? NotifReadyAudio,
        int? IntervalSeconds, bool? HideBotReviews);

    private record NotifyLogBody(string Type, string Repo, int PrNumber, string PrTitle);
    private record BackfillStartBody(string? Month);
}
