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
        var weeklyDir = WeeklyFolder.GetPath(outDir, DateTime.Now);
        using var logger = new AppLogger(weeklyDir);
        var store = new StateStore(outDir);

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

        var state = store.Load();

        Directory.CreateDirectory(weeklyDir);

        // Grava meus-prs.md atomicamente
        var mdContent = MarkdownWriter.BuildMeusPrs(myPrs, state.MergedHistory, state.ClosedHistory, config.HistoryDays, now);
        var mdPath = Path.Combine(weeklyDir, "meus-prs.md");
        WriteWithHashCheck(mdPath, mdContent, logger);

        // Grava historico.md atomicamente
        var histContent = MarkdownWriter.BuildHistorico(state.MergedHistory, state.ClosedHistory, now);
        var histPath = Path.Combine(weeklyDir, "historico.md");
        WriteWithHashCheck(histPath, histContent, logger);

        // Grava standup-YYYY-MM-DD.md (snapshot diário, sobrescrito a cada execução)
        var standupContent = MarkdownWriter.BuildStandup(myPrs, state.MergedHistory, now);
        var standupPath = Path.Combine(weeklyDir, $"standup-{now.ToLocalTime():yyyy-MM-dd}.md");
        WriteWithHashCheck(standupPath, standupContent, logger);

        // Grava reviews em sub-pasta separada
        var reviewsDir = Path.Combine(weeklyDir, "reviews");
        Directory.CreateDirectory(reviewsDir);
        var reviewsContent = MarkdownWriter.BuildReviews(reviewPrs, now);
        var reviewsPath = Path.Combine(reviewsDir, $"{now.ToLocalTime():yyyy-MM-dd}.md");
        WriteWithHashCheck(reviewsPath, reviewsContent, logger);

        ToastNotifier.NotifyNewNeedsAction(myPrs);

        var needsAction = myPrs.Count(p => p.Status?.NeedsMyAction == true);
        AnsiConsole.MarkupLine($"[green]Concluído.[/] {myPrs.Count} PRs, {needsAction} precisam de ação.");
        AnsiConsole.MarkupLine($"[grey]Arquivos salvos em: {Markup.Escape(weeklyDir)}[/]");

        logger.Info("--once concluído com sucesso");
        return 0;
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
