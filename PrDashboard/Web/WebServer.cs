using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PrDashboard.Models;

namespace PrDashboard.Web;

public class WebServer(PrsDataService dataService, AppConfig config)
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
            if (body.IntervalSeconds is int iv && iv >= 30 && iv <= 3600) config.IntervalSeconds = iv;
            if (body.HideBotReviews is not null) config.HideBotReviews = body.HideBotReviews.Value;

            dataService.RemapCached(config.Language);

            _ = Task.Run(() => SaveConfigAsync());

            var data = dataService.Cached ?? new PrsApiResponse { IsLoading = true, Language = config.Language, Theme = config.Theme };
            return Results.Json(data, JsonOpts);
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

    private record SettingsBody(
        string? Theme, string? Language,
        List<string>? CollapsedRepos, string? MyPrsFilter,
        string? MyPrsView, bool? SidebarOpen, List<string>? RepoOrder,
        bool? NotifCommentVisual, bool? NotifCommentAudio,
        bool? NotifCiVisual, bool? NotifCiAudio,
        bool? NotifMergedVisual, bool? NotifMergedAudio,
        int? IntervalSeconds, bool? HideBotReviews);
}
