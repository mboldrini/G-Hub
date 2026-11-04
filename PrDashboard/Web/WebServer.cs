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

        // Compute content hashes once at startup for cache-busting query params
        var jsV  = Math.Abs(WebAssets.Js.GetHashCode()).ToString("X8");
        var cssV = Math.Abs(WebAssets.Css.GetHashCode()).ToString("X8");
        var indexHtml = WebAssets.Html
            .Replace("/app.js\"",  $"/app.js?v={jsV}\"")
            .Replace("/app.css\"", $"/app.css?v={cssV}\"");

        app.MapGet("/", (HttpContext ctx) =>
        {
            ctx.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
            ctx.Response.Headers.Pragma = "no-cache";
            ctx.Response.Headers.Expires = "0";
            return Results.Content(indexHtml, "text/html; charset=utf-8");
        });

        app.MapGet("/app.css", (HttpContext ctx) =>
        {
            var content = WebAssets.Css;
            ctx.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
            ctx.Response.Headers.Pragma = "no-cache";
            ctx.Response.Headers.Expires = "0";
            return Results.Content(content, "text/css; charset=utf-8");
        });

        app.MapGet("/app.js", (HttpContext ctx) =>
        {
            var content = WebAssets.Js;
            ctx.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
            ctx.Response.Headers.Pragma = "no-cache";
            ctx.Response.Headers.Expires = "0";
            return Results.Content(content, "application/javascript; charset=utf-8");
        });

        app.MapGet("/icons.svg", (HttpContext ctx) =>
        {
            var content = WebAssets.Icons;
            ctx.Response.Headers.CacheControl = "public, max-age=86400"; // 24 hours for SVG
            ctx.Response.Headers.ETag = $"\"{content.GetHashCode():X}\"";
            return Results.Content(content, "image/svg+xml; charset=utf-8");
        });

        // Helper to serve embedded resources with caching
        IResult ServeEmbedded(HttpContext ctx, string resourcePath, string contentType, bool enableCache = true)
        {
            var assembly = typeof(WebAssets).Assembly;
            var fullPath = $"PrDashboard.Web.{resourcePath.Replace("/", ".")}";
            using var stream = assembly.GetManifestResourceStream(fullPath);
            if (stream == null) return Results.NotFound();
            using var reader = new StreamReader(stream);
            var content = reader.ReadToEnd();

            // Minification disabled - preserves original formatting and prevents syntax errors
            // if (contentType.Contains("css") || contentType.Contains("javascript"))
            // {
            //     content = MinifyContent(content, contentType.Contains("css"));
            // }

            // Add cache headers for static assets
            if (enableCache)
            {
                ctx.Response.Headers.CacheControl = "public, max-age=3600"; // 1 hour cache
                ctx.Response.Headers.ETag = $"\"{content.GetHashCode():X}\"";
            }
            else
            {
                ctx.Response.Headers.CacheControl = "no-cache, no-store, must-revalidate";
            }

            return Results.Content(content, contentType);
        }

        // Basic minification: remove comments and extra whitespace
        static string MinifyContent(string content, bool isCss)
        {
            if (string.IsNullOrEmpty(content)) return content;

            // Remove single-line comments (// ...) but NOT in strings or URLs
            // Match // at beginning of line or after whitespace, but not after : (for URLs like http://)
            content = System.Text.RegularExpressions.Regex.Replace(
                content,
                @"(^|\s)//.*?(?=\r?\n|$)",
                "$1",
                System.Text.RegularExpressions.RegexOptions.Multiline
            );

            // Remove multi-line comments (/* ... */) preserving line structure
            content = System.Text.RegularExpressions.Regex.Replace(
                content,
                @"/\*[\s\S]*?\*/",
                " "
            );

            // Remove extra whitespace (multiple spaces/tabs/newlines to single space)
            content = System.Text.RegularExpressions.Regex.Replace(
                content,
                @"\s+",
                " "
            );

            // Remove whitespace around CSS/JS operators (optional, for more aggressive minification)
            if (isCss)
            {
                content = System.Text.RegularExpressions.Regex.Replace(content, @"\s*([{}:;,])\s*", "$1");
            }

            return content.Trim();
        }

        // Serve modular CSS files
        app.MapGet("/css/{file}", (HttpContext ctx, string file) =>
        {
            if (!file.EndsWith(".css", StringComparison.OrdinalIgnoreCase)) return Results.NotFound();
            return ServeEmbedded(ctx, $"css.{file}", "text/css; charset=utf-8");
        });

        // Serve modular JS files
        app.MapGet("/js/{file}", (HttpContext ctx, string file) =>
        {
            if (!file.EndsWith(".js", StringComparison.OrdinalIgnoreCase)) return Results.NotFound();
            return ServeEmbedded(ctx, $"js.{file}", "application/javascript; charset=utf-8");
        });

        // Serve JS component files
        app.MapGet("/js/components/{file}", (HttpContext ctx, string file) =>
        {
            if (!file.EndsWith(".js", StringComparison.OrdinalIgnoreCase)) return Results.NotFound();
            return ServeEmbedded(ctx, $"js.components.{file}", "application/javascript; charset=utf-8");
        });

        // Serve JS view files
        app.MapGet("/js/views/{file}", (HttpContext ctx, string file) =>
        {
            if (!file.EndsWith(".js", StringComparison.OrdinalIgnoreCase)) return Results.NotFound();
            return ServeEmbedded(ctx, $"js.views.{file}", "application/javascript; charset=utf-8");
        });

        // Serve HTML view templates (no cache for HTML views)
        app.MapGet("/views/{file}", (HttpContext ctx, string file) =>
        {
            if (!file.EndsWith(".html", StringComparison.OrdinalIgnoreCase)) return Results.NotFound();
            return ServeEmbedded(ctx, $"views.{file}", "text/html; charset=utf-8", enableCache: false);
        });

        app.MapGet("/api/stats/logs", async () =>
        {
            var outDir = Path.GetFullPath(config.OutputDir);
            var entries = new List<object>();
            var seen = new HashSet<string>();
            int fileCount = 0;

            var sources = new (string Dir, string Status, SearchOption Opt)[]
            {
                (Path.Combine(outDir, "prs", "merged"), "merged", SearchOption.AllDirectories),
                (Path.Combine(outDir, "prs", "open"),   "open",   SearchOption.TopDirectoryOnly),
            };

            foreach (var src in sources)
            {
                if (!Directory.Exists(src.Dir)) continue;
                foreach (var f in Directory.GetFiles(src.Dir, "*.json", src.Opt))
                {
                    try
                    {
                        var snap = JsonSerializer.Deserialize<PrSnapshot>(await File.ReadAllTextAsync(f));
                        if (snap == null) continue;
                        if (!seen.Add($"{snap.Repo}#{snap.Number}")) continue;
                        entries.Add(new {
                            repo = snap.Repo, number = snap.Number, title = snap.Title,
                            body = snap.Body, url = snap.Url, author = snap.Author,
                            headRef = snap.HeadRef, baseRef = snap.BaseRef,
                            additions = snap.Additions, deletions = snap.Deletions, changedFiles = snap.ChangedFiles,
                            createdAt = snap.CreatedAt, mergedAt = snap.MergedAt,
                            reviewers = snap.Reviewers, jiraTickets = snap.JiraTickets,
                            status = src.Status,
                        });
                        fileCount++;
                    }
                    catch { }
                }
            }

            return Results.Json(new { entries, _meta = new { files = fileCount, readAt = DateTimeOffset.UtcNow } }, JsonOpts);
        });

        app.MapGet("/api/prs", () =>
        {
            var data = dataService.Cached ?? new PrsApiResponse { IsLoading = true, Language = config.Language, Theme = config.Theme };
            return Results.Json(data, JsonOpts);
        });

        app.MapGet("/api/config/worktrees", () =>
        {
            var wtConfig = config.WorktreesConfig ?? new WorktreesConfig();
            return Results.Json(new { rootPath = wtConfig.RootPath, worktreesPath = wtConfig.WorktreesPath }, JsonOpts);
        });

        app.MapGet("/api/worktrees/status", async (HttpContext ctx) =>
        {
            var repoPath = ctx.Request.Query["repoPath"].ToString();

            if (string.IsNullOrEmpty(repoPath) || !Directory.Exists(repoPath))
                return Results.Json(new { status = "unknown" }, JsonOpts);

            var psi = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "status -sb --porcelain",
                WorkingDirectory = repoPath,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(psi);
            if (process == null)
                return Results.Json(new { status = "error" }, JsonOpts);

            var output = await process.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                return Results.Json(new { status = "error" }, JsonOpts);

            var firstLine = output.Split('\n').FirstOrDefault() ?? "";
            var ahead = 0;
            var behind = 0;
            var hasUncommitted = output.Split('\n').Length > 1;

            if (firstLine.Contains("[ahead "))
            {
                var match = System.Text.RegularExpressions.Regex.Match(firstLine, @"ahead (\d+)");
                if (match.Success) ahead = int.Parse(match.Groups[1].Value);
            }

            if (firstLine.Contains("behind "))
            {
                var match = System.Text.RegularExpressions.Regex.Match(firstLine, @"behind (\d+)");
                if (match.Success) behind = int.Parse(match.Groups[1].Value);
            }

            return Results.Json(new { ahead, behind, hasUncommitted, isClean = !hasUncommitted && ahead == 0 }, JsonOpts);
        });

        app.MapGet("/api/worktrees/list", async (HttpContext ctx) =>
        {
            var repoPath = ctx.Request.Query["repoPath"].ToString();

            if (string.IsNullOrEmpty(repoPath) || !Directory.Exists(repoPath))
                return Results.Json(new { worktrees = new List<object>() });

            var psi = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "worktree list --porcelain",
                WorkingDirectory = repoPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(psi);
            var output = await process!.StandardOutput.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                return Results.Json(new { worktrees = new List<object>(), error = true });

            var worktrees = new List<object>();
            var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            object? current = null;
            foreach (var line in lines)
            {
                if (line.StartsWith("worktree "))
                {
                    if (current != null) worktrees.Add(current);
                    current = new { path = line.Replace("worktree ", "").Trim(), branch = "", head = "", isMain = false };
                }
                else if (line.StartsWith("branch "))
                {
                    var branch = line.Replace("branch ", "").Replace("refs/heads/", "").Trim();
                    current = new { ((dynamic)current!).path, branch, ((dynamic)current).head, ((dynamic)current).isMain };
                }
                else if (line.StartsWith("HEAD "))
                {
                    var head = line.Replace("HEAD ", "").Trim();
                    current = new { ((dynamic)current!).path, ((dynamic)current).branch, head, ((dynamic)current).isMain };
                }
                else if (line == "bare")
                {
                    current = new { ((dynamic)current!).path, ((dynamic)current).branch, ((dynamic)current).head, isMain = true };
                }
            }
            if (current != null) worktrees.Add(current);

            return Results.Json(new { worktrees });
        });

        app.MapGet("/api/config/worktrees/validate", () =>
        {
            var wt = config.WorktreesConfig;
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(wt?.RootPath) || wt.RootPath == "C:\\")
                errors.Add("rootPath");

            if (string.IsNullOrWhiteSpace(wt?.WorktreesPath) || wt.WorktreesPath == "C:\\")
                errors.Add("worktreesPath");

            if (!string.IsNullOrEmpty(wt?.RootPath) && wt.RootPath != "C:\\" && !Directory.Exists(wt.RootPath))
                errors.Add("rootPathNotFound");

            if (!string.IsNullOrEmpty(wt?.WorktreesPath) && wt.WorktreesPath != "C:\\" && !Directory.Exists(wt.WorktreesPath))
                errors.Add("worktreesPathNotFound");

            return Results.Json(new { isValid = errors.Count == 0, errors }, JsonOpts);
        });

        app.MapPost("/api/worktrees/delete", async (HttpContext ctx) =>
        {
            using var reader = new System.IO.StreamReader(ctx.Request.Body);
            var json = await reader.ReadToEndAsync();
            var body = JsonSerializer.Deserialize<JsonElement>(json);
            var repoPath = body.TryGetProperty("repoPath", out var rp) ? rp.GetString() : null;
            var worktreePath = body.TryGetProperty("worktreePath", out var wp) ? wp.GetString() : null;

            if (string.IsNullOrEmpty(repoPath) || string.IsNullOrEmpty(worktreePath))
                return Results.Json(new { success = false, error = "invalidParams" }, JsonOpts);

            var psi = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = $"worktree remove \"{worktreePath}\" --force",
                WorkingDirectory = repoPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(psi);
            if (process != null)
            {
                await process.WaitForExitAsync();

                // Se falhar via git, tentar excluir pasta diretamente
                if (process.ExitCode != 0 && Directory.Exists(worktreePath))
                {
                    try { Directory.Delete(worktreePath, true); }
                    catch { return Results.Json(new { success = false, error = "cannotDelete" }, JsonOpts); }
                }
            }

            return Results.Json(new { success = true }, JsonOpts);
        });

        app.MapGet("/api/worktrees/repositories", async (HttpContext ctx) =>
        {
            var rootPath = config.WorktreesConfig?.RootPath;

            if (string.IsNullOrEmpty(rootPath) || !Directory.Exists(rootPath))
                return Results.Json(new { repositories = new List<object>() }, JsonOpts);

            var repos = new List<object>();

            foreach (var dir in Directory.GetDirectories(rootPath))
            {
                var gitDir = Path.Combine(dir, ".git");
                if (!Directory.Exists(gitDir) && !File.Exists(gitDir)) continue;

                var repoName = Path.GetFileName(dir);

                // Obter branch atual
                var headFile = Path.Combine(dir, ".git", "HEAD");
                var currentBranch = "unknown";
                if (File.Exists(headFile))
                {
                    var headContent = await File.ReadAllTextAsync(headFile);
                    if (headContent.StartsWith("ref: refs/heads/"))
                        currentBranch = headContent.Replace("ref: refs/heads/", "").Trim();
                }

                repos.Add(new
                {
                    name = repoName,
                    path = dir,
                    currentBranch,
                    lastModified = Directory.GetLastWriteTime(dir)
                });
            }

            return Results.Json(new { repositories = repos.OrderBy(r => ((dynamic)r).name) }, JsonOpts);
        });

        app.MapGet("/api/worktrees/cache", async () =>
        {
            var outDir = Path.GetFullPath(config.OutputDir);
            var cachePath = Path.Combine(outDir, "worktrees-cache.json");

            if (!File.Exists(cachePath))
                return Results.Json(new { cached = false }, JsonOpts);

            try
            {
                var json = await File.ReadAllTextAsync(cachePath);
                var data = JsonSerializer.Deserialize<JsonElement>(json);
                return Results.Json(new { cached = true, data }, JsonOpts);
            }
            catch
            {
                return Results.Json(new { cached = false }, JsonOpts);
            }
        });

        app.MapPost("/api/worktrees/cache", async (HttpContext ctx) =>
        {
            using var reader = new System.IO.StreamReader(ctx.Request.Body);
            var json = await reader.ReadToEndAsync();

            var outDir = Path.GetFullPath(config.OutputDir);
            var cachePath = Path.Combine(outDir, "worktrees-cache.json");

            try
            {
                await File.WriteAllTextAsync(cachePath, json);
                return Results.Json(new { success = true }, JsonOpts);
            }
            catch (Exception ex)
            {
                return Results.Json(new { success = false, error = ex.Message }, JsonOpts);
            }
        });

        app.MapGet("/api/worktrees/stats", async (HttpContext ctx) =>
        {
            var rootPath = config.WorktreesConfig?.RootPath;
            var worktreesPath = config.WorktreesConfig?.WorktreesPath;

            if (string.IsNullOrEmpty(rootPath) || !Directory.Exists(rootPath))
                return Results.Json(new { totalRepos = 0, totalWorktrees = 0, diskSpace = "0 B", reposWithUpdates = 0 }, JsonOpts);

            var repos = Directory.GetDirectories(rootPath)
                .Where(d => Directory.Exists(Path.Combine(d, ".git")) || File.Exists(Path.Combine(d, ".git")))
                .ToList();

            var totalRepos = repos.Count;
            var totalWorktrees = 0;
            var reposWithUpdates = 0;

            foreach (var repo in repos)
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "git",
                    Arguments = "worktree list --porcelain",
                    WorkingDirectory = repo,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var process = Process.Start(psi);
                if (process != null)
                {
                    var output = await process.StandardOutput.ReadToEndAsync();
                    await process.WaitForExitAsync();

                    if (process.ExitCode == 0)
                    {
                        var count = output.Split("worktree ", StringSplitOptions.RemoveEmptyEntries).Length - 1;
                        totalWorktrees += Math.Max(0, count - 1); // -1 para não contar o repo principal
                    }
                }
            }

            long diskSpaceBytes = 0;
            if (!string.IsNullOrEmpty(worktreesPath) && Directory.Exists(worktreesPath))
            {
                var di = new DirectoryInfo(worktreesPath);
                diskSpaceBytes = di.EnumerateFiles("*", SearchOption.AllDirectories).Sum(f => f.Length);
            }

            var diskSpaceStr = diskSpaceBytes switch
            {
                < 1024 => $"{diskSpaceBytes} B",
                < 1024 * 1024 => $"{diskSpaceBytes / 1024.0:F1} KB",
                < 1024 * 1024 * 1024 => $"{diskSpaceBytes / (1024.0 * 1024):F1} MB",
                _ => $"{diskSpaceBytes / (1024.0 * 1024 * 1024):F1} GB"
            };

            return Results.Json(new
            {
                totalRepos,
                totalWorktrees,
                diskSpace = diskSpaceStr,
                reposWithUpdates
            }, JsonOpts);
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
            if (body.DisplayName is not null) config.DisplayName = body.DisplayName.Length > 0 ? body.DisplayName : null;
            if (body.WorktreesConfig is not null)
            {
                config.WorktreesConfig ??= new WorktreesConfig();
                if (body.WorktreesConfig.RootPath is not null) config.WorktreesConfig.RootPath = body.WorktreesConfig.RootPath;
                if (body.WorktreesConfig.WorktreesPath is not null) config.WorktreesConfig.WorktreesPath = body.WorktreesConfig.WorktreesPath;
            }

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

        app.MapPost("/api/worktrees/create", async (HttpContext ctx) =>
        {
            var body = await ctx.Request.ReadFromJsonAsync<JsonElement>();
            var repoPath = body.GetProperty("repoPath").GetString();
            var branch = body.GetProperty("branch").GetString();
            var worktreeName = body.GetProperty("worktreeName").GetString();

            var worktreesBasePath = config.WorktreesConfig?.WorktreesPath;

            if (string.IsNullOrEmpty(repoPath) || string.IsNullOrEmpty(branch) || string.IsNullOrEmpty(worktreesBasePath))
                return Results.Json(new { success = false, error = "invalidParams" });

            var repoName = Path.GetFileName(repoPath);
            var worktreeFullPath = Path.Combine(worktreesBasePath, repoName, worktreeName!);

            var psi = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = $"worktree add \"{worktreeFullPath}\" {branch}",
                WorkingDirectory = repoPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(psi);
            var output = await process!.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                return Results.Json(new { success = false, error = error });

            return Results.Json(new { success = true, path = worktreeFullPath });
        });

        app.MapPost("/api/worktrees/open", async (HttpContext ctx) =>
        {
            var body = await ctx.Request.ReadFromJsonAsync<JsonElement>();
            var path = body.GetProperty("path").GetString();
            var action = body.GetProperty("action").GetString(); // "explorer", "terminal", "vscode"

            if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
                return Results.Json(new { success = false, error = "invalidPath" }, JsonOpts);

            try
            {
                if (action == "explorer")
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "explorer.exe",
                        Arguments = $"\"{path}\"",
                        UseShellExecute = true
                    });
                }
                else if (action == "terminal")
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "cmd.exe",
                        Arguments = $"/K cd /d \"{path}\"",
                        UseShellExecute = true
                    });
                }
                else if (action == "vscode")
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = "code",
                        Arguments = $"\"{path}\"",
                        UseShellExecute = true
                    });
                }
                else
                {
                    return Results.Json(new { success = false, error = "invalidAction" }, JsonOpts);
                }

                return Results.Json(new { success = true }, JsonOpts);
            }
            catch (Exception ex)
            {
                return Results.Json(new { success = false, error = ex.Message }, JsonOpts);
            }
        });

        app.MapPost("/api/worktrees/update", async (HttpContext ctx) =>
        {
            using var reader = new System.IO.StreamReader(ctx.Request.Body);
            var json = await reader.ReadToEndAsync();
            var body = JsonSerializer.Deserialize<JsonElement>(json);
            var repoPath = body.TryGetProperty("repoPath", out var rp) ? rp.GetString() : null;

            if (string.IsNullOrEmpty(repoPath) || !Directory.Exists(repoPath))
                return Results.Json(new { success = false, error = "invalidPath" }, JsonOpts);

            var psi = new ProcessStartInfo
            {
                FileName = "git",
                Arguments = "pull",
                WorkingDirectory = repoPath,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            var process = Process.Start(psi);
            if (process == null)
                return Results.Json(new { success = false, error = "failedToStartProcess" }, JsonOpts);

            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0)
                return Results.Json(new { success = false, error = error }, JsonOpts);

            return Results.Json(new { success = true, output }, JsonOpts);
        });

        app.MapPost("/api/select-folder", async (HttpContext ctx) =>
        {
            using var reader = new System.IO.StreamReader(ctx.Request.Body);
            var json = await reader.ReadToEndAsync();
            var body = JsonSerializer.Deserialize<SelectFolderBody>(json, ReadOpts);
            if (body == null) return Results.BadRequest();

            string? selectedPath = null;
            var thread = new Thread(() =>
            {
                try
                {
                    var dialog = new System.Windows.Forms.FolderBrowserDialog
                    {
                        Description = body.Description ?? "Selecione uma pasta",
                        UseDescriptionForTitle = true,
                        SelectedPath = body.InitialPath ?? "C:\\"
                    };

                    // Create a dummy form to ensure dialog appears on top
                    var dummyForm = new System.Windows.Forms.Form
                    {
                        TopMost = true,
                        StartPosition = System.Windows.Forms.FormStartPosition.Manual,
                        Location = new System.Drawing.Point(-2000, -2000),
                        Size = new System.Drawing.Size(1, 1),
                        ShowInTaskbar = false
                    };

                    dummyForm.Show();
                    var result = dialog.ShowDialog(dummyForm);
                    dummyForm.Close();
                    dummyForm.Dispose();

                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        selectedPath = dialog.SelectedPath;
                    }
                    dialog.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error opening folder dialog: {ex.Message}");
                }
            });
            thread.SetApartmentState(System.Threading.ApartmentState.STA);
            thread.Start();
            thread.Join();

            return Results.Json(new { path = selectedPath }, JsonOpts);
        });

        app.MapGet("/favicon.svg", () =>
        {
            var exeDir = Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory;
            var path = Path.Combine(exeDir, "github.svg");
            return File.Exists(path) ? Results.File(path, "image/svg+xml") : Results.NotFound();
        });

        // Audio: serve MP3/WAV files from exe directory, detect MIME by magic bytes
        static string DetectAudioMime(byte[] header)
        {
            if (header.Length >= 4 && header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x46)
                return "audio/wav";
            return "audio/mpeg";
        }

        var exeDirAudio = Path.GetDirectoryName(Environment.ProcessPath) ?? AppContext.BaseDirectory;
        foreach (var name in new[] { "done.mp3", "error.mp3", "message.mp3" })
        {
            var filePath = Path.Combine(exeDirAudio, name);
            var captured = name;
            IResult ServeAudio()
            {
                if (!File.Exists(filePath)) return Results.NotFound();
                var data = File.ReadAllBytes(filePath);
                var mime = DetectAudioMime(data.Length >= 4 ? data[..4] : data);
                return Results.Bytes(data, mime);
            }
            app.MapGet($"/audio/{captured}", ServeAudio);
            app.MapGet($"/{captured}",       ServeAudio);
        }

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

        if (client != null && login != null)
            _ = Task.Run(() => RunAutoBackfillAsync(ct), ct);

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

    private async Task RunAutoBackfillAsync(CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        for (int i = 0; i < 3; i++)
        {
            if (ct.IsCancellationRequested) return;
            var startDate = new DateOnly(today.Year, today.Month, 1).AddMonths(-i);
            var jobId = $"auto-{startDate:yyyy-MM}";
            var prog = new BackfillProgress();
            _backfillJobs[jobId] = prog;
            await RunBackfillAsync(jobId, prog, startDate, ct);
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
        int? IntervalSeconds, bool? HideBotReviews,
        string? DisplayName,
        WorktreesConfigBody? WorktreesConfig);

    private record WorktreesConfigBody(string? RootPath, string? WorktreesPath);
    private record SelectFolderBody(string? InitialPath, string? Description);

    private record NotifyLogBody(string Type, string Repo, int PrNumber, string PrTitle);
    private record BackfillStartBody(string? Month);
}
