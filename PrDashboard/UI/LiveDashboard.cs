using PrDashboard.GitHub;
using PrDashboard.IO;
using PrDashboard.Logic;
using PrDashboard.Models;
using Spectre.Console;
using Spectre.Console.Rendering;
using System.Diagnostics;

namespace PrDashboard.UI;

public class LiveDashboard(GhApiClient client, string login, AppConfig config, bool useEmoji, bool useLinks)
{
    private readonly PrRenderer _renderer = new(useEmoji, useLinks);

    private List<PullRequest> _myPrs     = [];
    private List<PullRequest> _reviewPrs = [];
    private List<MergedPr>    _mergedPrs = [];
    private HashSet<string>   _changedKeys = [];
    private string?           _lastError;
    private DateTimeOffset    _lastUpdate = DateTimeOffset.MinValue;
    private bool              _updating;
    private int               _selectedIndex;
    private List<PullRequest> _allForNav = [];
    private bool              _exit;

    public async Task RunAsync(CancellationToken ct)
    {
        AppLogger.Instance?.Info($"Live dashboard iniciado, login={login}");
        await FetchAsync(ct);

        await AnsiConsole.Live(BuildLayout())
            .AutoClear(false)
            .Overflow(VerticalOverflow.Ellipsis)
            .StartAsync(async ctx =>
            {
                ctx.Refresh();

                using var timer    = new PeriodicTimer(TimeSpan.FromSeconds(config.IntervalSeconds));
                var timerTask  = timer.WaitForNextTickAsync(ct).AsTask();
                var inputTask  = Task.Run(() => ReadKeyAsync(ct), ct);

                while (!_exit && !ct.IsCancellationRequested)
                {
                    var nextSec = (int)(_lastUpdate.AddSeconds(config.IntervalSeconds) - DateTimeOffset.UtcNow).TotalSeconds;
                    ctx.UpdateTarget(BuildLayout(Math.Max(0, nextSec)));
                    ctx.Refresh();

                    var done = await Task.WhenAny(timerTask, inputTask, Task.Delay(1000, ct));

                    if (done == timerTask)
                    {
                        _updating = true;
                        ctx.UpdateTarget(BuildLayout());
                        ctx.Refresh();
                        await FetchAsync(ct);
                        _updating = false;
                        timerTask = timer.WaitForNextTickAsync(ct).AsTask();
                    }
                    else if (done == inputTask)
                    {
                        var key = await inputTask;
                        HandleKey(key);
                        inputTask = Task.Run(() => ReadKeyAsync(ct), ct);
                    }
                }
            });

        AppLogger.Instance?.Info("Live dashboard encerrado");
    }

    private async Task<ConsoleKeyInfo> ReadKeyAsync(CancellationToken ct)
    {
        return await Task.Run(() =>
        {
            while (!ct.IsCancellationRequested)
            {
                if (Console.KeyAvailable) return Console.ReadKey(true);
                Thread.Sleep(50);
            }
            return new ConsoleKeyInfo();
        }, ct);
    }

    private void HandleKey(ConsoleKeyInfo key)
    {
        switch (key.Key)
        {
            case ConsoleKey.Q: _exit = true; break;
            case ConsoleKey.R: _ = FetchAsync(CancellationToken.None); break;
            case ConsoleKey.UpArrow:   if (_selectedIndex > 0) _selectedIndex--; break;
            case ConsoleKey.DownArrow: if (_selectedIndex < _allForNav.Count - 1) _selectedIndex++; break;
            case ConsoleKey.Enter:
                var pr = _allForNav.ElementAtOrDefault(_selectedIndex);
                if (pr?.Url is { Length: > 0 } url) OpenUrl(url);
                break;
        }
    }

    private static void OpenUrl(string url)
    {
        try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
        catch { /* ignore */ }
    }

    private async Task FetchAsync(CancellationToken ct)
    {
        try
        {
            var prevKeys = _myPrs.Select(PrKey).ToHashSet();

            var myPrs     = await client.FetchMyOpenPrsAsync(login, config, ct);
            var reviewPrs = await client.FetchReviewRequestedPrsAsync(login, config, ct);
            var mergedPrs = await client.FetchMergedPrsAsync(login, 7, ct); // last 7 days for live view

            // Filter own PRs and bot-authored PRs from review section
            var myKeys = myPrs.Select(PrKey).ToHashSet();
            reviewPrs = reviewPrs
                .Where(p => !myKeys.Contains(PrKey(p)))
                .Where(p => !PrAnalyzer.IsBot(p.Author ?? ""))
                .ToList();

            var now = DateTimeOffset.UtcNow;
            foreach (var pr in myPrs.Concat(reviewPrs))
                pr.Status = PrAnalyzer.Analyze(pr, login, now);

            myPrs = [.. myPrs.OrderBy(p => p.Status?.Urgency ?? UrgencyLevel.Normal)];

            _changedKeys = myPrs
                .Where(p => prevKeys.Contains(PrKey(p)) && p.UpdatedAt > now.AddSeconds(-config.IntervalSeconds - 30))
                .Select(PrKey)
                .ToHashSet();

            _myPrs     = myPrs;
            _reviewPrs = reviewPrs;
            _mergedPrs = mergedPrs;
            _allForNav = [.. myPrs, .. reviewPrs];
            _lastError = null;
            _lastUpdate = now;

            AppLogger.Instance?.Info($"Refresh: {myPrs.Count} meus, {reviewPrs.Count} reviews, {mergedPrs.Count} mergeados");
        }
        catch (Exception ex)
        {
            _lastError = ex.Message;
            AppLogger.Instance?.Error(ex);
        }
    }

    private static string PrKey(PullRequest pr) => $"{pr.Repo}#{pr.Number}";

    private IRenderable BuildLayout(int nextSec = 0)
    {
        var now = DateTimeOffset.UtcNow;
        int width; try { width = Console.WindowWidth > 0 ? Console.WindowWidth - 4 : 116; } catch { width = 116; }

        var needsAction  = _myPrs.Count(p => p.Status?.NeedsMyAction == true);
        var readyToMerge = _myPrs.Count(PrRenderer.IsReadyToMerge);
        var drafts       = _myPrs.Count(p => p.IsDraft);

        var rows = new List<IRenderable>();

        rows.Add(_renderer.BuildHeaderPanel(
            _myPrs.Count, needsAction, readyToMerge, drafts,
            _lastUpdate, _updating ? 0 : nextSec));

        if (_updating && !_myPrs.Any())
        {
            rows.Add(_renderer.RenderUpdating());
        }
        else
        {
            int navIdx = 0;
            foreach (var group in _myPrs.GroupBy(p => p.Repo).OrderBy(g => g.Key))
            {
                rows.Add(_renderer.BuildRepoSection(
                    group.Key, group.ToList(), _selectedIndex, navIdx, width, now));
                navIdx += group.Count();
            }

            if (!_myPrs.Any())
                rows.Add(new Markup("[grey]  Nenhum PR aberto.[/]"));

            rows.Add(_renderer.BuildReviewSection(_reviewPrs, _selectedIndex, navIdx, now));

            var concluded = _renderer.BuildConcludedSection(_mergedPrs);
            if (concluded is not null) rows.Add(concluded);
        }

        if (_updating)
            rows.Add(_renderer.RenderUpdating());

        if (_lastError is not null)
            rows.Add(_renderer.RenderError(_lastError));

        rows.Add(_renderer.RenderFooter());

        return new Rows(rows);
    }
}
