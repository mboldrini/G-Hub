using PrDashboard.GitHub;
using PrDashboard.Logic;
using PrDashboard.Models;
using PrDashboard.Notifications;
using Spectre.Console;

namespace PrDashboard.IO;

public class OnceRunner(GhApiClient client, string login, AppConfig config)
{
    public async Task<int> RunAsync(CancellationToken ct = default)
    {
        var outDir = Path.GetFullPath(config.OutputDir);
        using var logger = new AppLogger(outDir);
        var store = new StateStore(outDir);
        var cacheStore = new RepoCacheStore(Path.GetFullPath(config.CacheDir));

        logger.Info($"--once iniciado, login={login}");

        List<PullRequest> myPrs, reviewPrs;
        List<MergedPr> merged;
        List<ClosedPr> closed;

        try
        {
            AnsiConsole.MarkupLine("[grey]Buscando PRs...[/]");
            myPrs = await client.FetchMyOpenPrsAsync(login, config, ct);
            reviewPrs = await client.FetchReviewRequestedPrsAsync(login, config, ct);
            merged = await client.FetchMergedPrsAsync(login, config.HistoryDays, ct);
            closed = await client.FetchClosedUnmergedPrsAsync(login, config.HistoryDays, ct);
            logger.Info($"Buscados: {myPrs.Count} meus, {reviewPrs.Count} reviews, {merged.Count} mergeados, {closed.Count} fechados");
        }
        catch (GhException ex)
        {
            logger.Error(ex);
            AnsiConsole.MarkupLine($"[red]Erro na API:[/] {Markup.Escape(ex.Message)}");
            return 1;
        }

        var now = DateTimeOffset.UtcNow;

        var myKeys = myPrs.Select(p => $"{p.Repo}#{p.Number}").ToHashSet();
        reviewPrs = reviewPrs
            .Where(p => !myKeys.Contains($"{p.Repo}#{p.Number}"))
            .ToList();

        foreach (var pr in myPrs.Concat(reviewPrs))
            pr.Status = PrAnalyzer.Analyze(pr, login, now);

        try
        {
            store.UpdateAndSave(myPrs, reviewPrs, merged, closed);
            logger.Info("Estado salvo");
        }
        catch (Exception ex)
        {
            logger.Error(ex);
            AnsiConsole.MarkupLine($"[yellow]Aviso:[/] Não foi possível salvar estado: {Markup.Escape(ex.Message)}");
        }

        UpdateRepoCache(cacheStore, myPrs, now, logger);

        var state = store.Load();
        var localNow = now.ToLocalTime();
        var monthFolder = localNow.ToString("yyyy-MM");

        // standups/YYYY-MM/YYYY-MM-DD.md
        var standupDir = Path.Combine(outDir, "standups", monthFolder);
        Directory.CreateDirectory(standupDir);
        var standupContent = MarkdownWriter.BuildStandup(myPrs, state.MergedHistory, now);
        var standupPath = Path.Combine(standupDir, $"{localNow:yyyy-MM-dd}.md");
        WriteWithHashCheck(standupPath, standupContent, logger);

        // review-queue/YYYY-MM/YYYY-MM-DD.md
        var reviewQueueDir = Path.Combine(outDir, "review-queue", monthFolder);
        Directory.CreateDirectory(reviewQueueDir);
        var reviewsContent = MarkdownWriter.BuildReviews(reviewPrs, now);
        var reviewsPath = Path.Combine(reviewQueueDir, $"{localNow:yyyy-MM-dd}.md");
        WriteWithHashCheck(reviewsPath, reviewsContent, logger);

        // prs/dashboard.md
        var prsDir = Path.Combine(outDir, "prs");
        Directory.CreateDirectory(prsDir);
        var mdContent = MarkdownWriter.BuildMeusPrs(myPrs, state.MergedHistory, state.ClosedHistory, config.HistoryDays, now);
        var mdPath = Path.Combine(prsDir, "dashboard.md");
        WriteWithHashCheck(mdPath, mdContent, logger);

        // prs/history.md
        var histContent = MarkdownWriter.BuildHistorico(state.MergedHistory, state.ClosedHistory, now);
        var histPath = Path.Combine(prsDir, "history.md");
        WriteWithHashCheck(histPath, histContent, logger);

        ToastNotifier.NotifyNewNeedsAction(myPrs);

        var needsAction = myPrs.Count(p => p.Status?.NeedsMyAction == true);
        AnsiConsole.MarkupLine($"[green]Concluído.[/] {myPrs.Count} PRs, {needsAction} precisam de ação.");
        AnsiConsole.MarkupLine($"[grey]Arquivos salvos em: {Markup.Escape(outDir)}[/]");

        logger.Info("--once concluído com sucesso");
        return 0;
    }

    private static void UpdateRepoCache(RepoCacheStore cacheStore, List<PullRequest> myPrs, DateTimeOffset now, AppLogger logger)
    {
        try
        {
            foreach (var repoGroup in myPrs.GroupBy(p => p.Repo))
            {
                var entry = cacheStore.LoadRepo(repoGroup.Key);
                var existingByNumber = entry.OpenPrs.ToDictionary(p => p.Number);

                entry.OpenPrs = repoGroup.Select(pr =>
                {
                    existingByNumber.TryGetValue(pr.Number, out var existing);
                    var descriptionUnchanged = existing != null && existing.UpdatedAt == pr.UpdatedAt;
                    return new CachedPr
                    {
                        Number = pr.Number,
                        Title = pr.Title,
                        Branch = pr.HeadRef,
                        Author = pr.Author ?? "",
                        CreatedAt = pr.CreatedAt,
                        UpdatedAt = pr.UpdatedAt,
                        Status = new CachedPrStatus
                        {
                            Ci = pr.Status?.Ci.State switch
                            {
                                CiState.Passing => "passing",
                                CiState.Failing => "failing",
                                CiState.Pending => "pending",
                                _ => "none"
                            },
                            ReviewState = pr.Status?.Review.State switch
                            {
                                ReviewState.Approved => "approved",
                                ReviewState.ChangesRequested => "changes_requested",
                                ReviewState.Pending => "awaiting_review",
                                _ => "none"
                            },
                            HasConflicts = pr.Status?.Merge.State == MergeState.Conflict,
                            IsDraft = pr.IsDraft,
                        },
                        Description = descriptionUnchanged ? existing!.Description : pr.Body,
                        JiraTickets = existing?.JiraTickets ?? RepoCacheStore.ExtractJiraTickets(pr.Body),
                    };
                }).ToList();

                entry.Repo = repoGroup.Key;
                entry.UpdatedAt = now;
                cacheStore.SaveRepo(entry);
            }

            var meta = cacheStore.LoadMeta();
            foreach (var repo in myPrs.Select(p => p.Repo).Distinct())
                meta.LastUpdated[repo] = now;
            cacheStore.SaveMeta(meta);

            logger.Info($"Cache atualizado: {myPrs.GroupBy(p => p.Repo).Count()} repos");
        }
        catch (Exception ex)
        {
            logger.Error(ex);
        }
    }

    private static void WriteWithHashCheck(string path, string content, AppLogger logger)
    {
        var newHash = System.Security.Cryptography.SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(content));
        var newHashHex = Convert.ToHexString(newHash);
        var existingHash = StateStore.HashContent(path);

        if (existingHash == newHashHex)
        {
            logger.Info($"Sem mudanças em {Path.GetFileName(path)}, não regravado");
            return;
        }

        try
        {
            StateStore.WriteAtomic(path, content);
            logger.Info($"Gravado {Path.GetFileName(path)}");
        }
        catch (Exception ex)
        {
            logger.Error(ex);
            // Never delete the last valid file — just warn
            AnsiConsole.MarkupLine($"[yellow]Aviso:[/] Não foi possível gravar {Markup.Escape(Path.GetFileName(path))}: {Markup.Escape(ex.Message)}");
        }
    }
}
