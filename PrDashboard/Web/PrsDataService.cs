using PrDashboard.GitHub;
using PrDashboard.IO;
using PrDashboard.Logic;
using PrDashboard.Models;
using PrDashboard.UI;

namespace PrDashboard.Web;

public class PrsDataService(GhApiClient client, string login, AppConfig config, bool demoMode)
{
    private volatile PrsApiResponse? _cached;
    private List<PullRequest> _rawMyPrs = [];
    private List<PullRequest> _rawReviewPrs = [];
    private List<MergedPr> _rawMergedPrs = [];

    public PrsApiResponse? Cached => _cached;

    public async Task InitAsync(CancellationToken ct)
    {
        if (demoMode)
        {
            _cached = BuildDemoResponse();
            return;
        }
        await RefreshAsync(ct);
    }

    public void StartBackgroundRefresh(CancellationToken ct)
    {
        if (demoMode) return;
        _ = Task.Run(async () =>
        {
            while (!ct.IsCancellationRequested)
            {
                try { await Task.Delay(TimeSpan.FromSeconds(config.IntervalSeconds), ct); }
                catch (OperationCanceledException) { return; }
                await RefreshAsync(ct);
            }
        }, ct);
    }

    public async Task RefreshAsync(CancellationToken ct)
    {
        try
        {
            var myPrs = await client.FetchMyOpenPrsAsync(login, config, ct);
            var reviewPrs = await client.FetchReviewRequestedPrsAsync(login, config, ct);
            var mergedPrs = await client.FetchMergedPrsAsync(login, config.HistoryDays, ct);

            foreach (var repo in config.WatchedRepos)
            {
                var watched = await client.FetchWatchedRepoPrsAsync(repo, login, config, ct);
                myPrs.AddRange(watched);
            }
            myPrs = myPrs.DistinctBy(p => $"{p.Repo}#{p.Number}").ToList();

            var myKeys = myPrs.Select(p => $"{p.Repo}#{p.Number}").ToHashSet();
            reviewPrs = reviewPrs
                .Where(p => !myKeys.Contains($"{p.Repo}#{p.Number}"))
                .ToList();

            var now = DateTimeOffset.UtcNow;
            foreach (var pr in myPrs.Concat(reviewPrs))
                pr.Status = PrAnalyzer.Analyze(pr, login, now);

            _rawMyPrs = myPrs;
            _rawReviewPrs = reviewPrs;
            _rawMergedPrs = mergedPrs;

            _cached = BuildResponse(login, now);
            WriteStandup(myPrs, mergedPrs, now);
            WriteReviews(reviewPrs, now);
        }
        catch (OperationCanceledException) { throw; }
        catch (Exception ex)
        {
            var prev = _cached;
            _cached = new PrsApiResponse
            {
                Login = prev?.Login ?? login,
                UpdatedAt = prev?.UpdatedAt ?? "",
                IntervalSeconds = config.IntervalSeconds,
                Language = config.Language,
                Theme = config.Theme,
                CollapsedRepos = config.CollapsedRepos,
                MyPrsFilter = config.MyPrsFilter,
                MyPrsView = config.MyPrsView,
                SidebarOpen = config.SidebarOpen,
                RepoOrder = config.RepoOrder,
                HideBotReviews = config.HideBotReviews,
                Error = ex.Message,
                MyPrs = prev?.MyPrs ?? [],
                ReviewPrs = prev?.ReviewPrs ?? [],
                MergedPrs = prev?.MergedPrs ?? [],
            };
        }
    }

    public void RemapCached(string lang)
    {
        if (_rawMyPrs.Count == 0 && _rawMergedPrs.Count == 0) return;
        var now = DateTimeOffset.UtcNow;
        _cached = BuildResponse(_cached?.Login ?? login, now, lang);
    }

    private PrsApiResponse BuildResponse(string user, DateTimeOffset now, string? langOverride = null)
    {
        var lang = langOverride ?? config.Language;
        return new PrsApiResponse
        {
            Login = user,
            UpdatedAt = now.ToLocalTime().ToString("HH:mm:ss"),
            IntervalSeconds = config.IntervalSeconds,
            Language = lang,
            Theme = config.Theme,
            CollapsedRepos = config.CollapsedRepos,
            MyPrsFilter = config.MyPrsFilter,
            MyPrsView = config.MyPrsView,
            SidebarOpen = config.SidebarOpen,
            RepoOrder = config.RepoOrder,
            NotifCommentVisual = config.NotifCommentVisual,
            NotifCommentAudio = config.NotifCommentAudio,
            NotifCiVisual = config.NotifCiVisual,
            NotifCiAudio = config.NotifCiAudio,
            NotifMergedVisual = config.NotifMergedVisual,
            NotifMergedAudio = config.NotifMergedAudio,
            NotifApprovedVisual = config.NotifApprovedVisual,
            NotifApprovedAudio = config.NotifApprovedAudio,
            NotifReadyVisual = config.NotifReadyVisual,
            NotifReadyAudio = config.NotifReadyAudio,
            HideBotReviews = config.HideBotReviews,
            MyPrs = _rawMyPrs.Select(p => PrsApiDtos.MapMyPr(p, now, lang)).ToList(),
            ReviewPrs = _rawReviewPrs.Select(p => PrsApiDtos.MapReviewPr(p, now, lang)).ToList(),
            MergedPrs = _rawMergedPrs.Select(p => PrsApiDtos.MapMergedPr(p, now, lang)).ToList(),
        };
    }

    private void WriteReviews(List<PullRequest> reviewPrs, DateTimeOffset now)
    {
        try
        {
            var outDir = Path.GetFullPath(config.OutputDir);
            var monthFolder = now.ToLocalTime().ToString("yyyy-MM");
            var reviewQueueDir = Path.Combine(outDir, "review-queue", monthFolder);
            Directory.CreateDirectory(reviewQueueDir);
            var path = Path.Combine(reviewQueueDir, $"{now.ToLocalTime():yyyy-MM-dd}.md");
            var content = MarkdownWriter.BuildReviews(reviewPrs, now);
            StateStore.WriteAtomic(path, content);
        }
        catch { }
    }

    private void WriteStandup(List<PullRequest> myPrs, List<MergedPr> mergedPrs, DateTimeOffset now)
    {
        try
        {
            var outDir = Path.GetFullPath(config.OutputDir);
            var monthFolder = now.ToLocalTime().ToString("yyyy-MM");
            var standupDir = Path.Combine(outDir, "standups", monthFolder);
            Directory.CreateDirectory(standupDir);
            var path = Path.Combine(standupDir, $"{now.ToLocalTime():yyyy-MM-dd}.md");
            var content = MarkdownWriter.BuildStandup(myPrs, mergedPrs, now);
            StateStore.WriteAtomic(path, content);
        }
        catch { }
    }

    private PrsApiResponse BuildDemoResponse()
    {
        var (myPrs, reviewPrs, mergedPrs) = DemoData.Generate();
        var now = DateTimeOffset.UtcNow;
        foreach (var pr in myPrs.Concat(reviewPrs))
            pr.Status = PrAnalyzer.Analyze(pr, "demo-user", now);

        _rawMyPrs = myPrs;
        _rawReviewPrs = reviewPrs.Where(p => !PrAnalyzer.IsBot(p.Author ?? "")).ToList();
        _rawMergedPrs = mergedPrs;

        return BuildResponse("demo-user", now);
    }
}
