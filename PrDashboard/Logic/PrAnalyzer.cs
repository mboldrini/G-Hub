using PrDashboard.Models;

namespace PrDashboard.Logic;

public static class PrAnalyzer
{
    private static readonly HashSet<string> BotSuffixes = ["[bot]", "-bot", "_bot"];
    private static readonly HashSet<string> BotLogins = ["dependabot", "renovate", "github-actions", "snyk-bot", "codecov-commenter"];

    public static PrStatus Analyze(PullRequest pr, string myLogin, DateTimeOffset now)
    {
        var review = ComputeReview(pr, myLogin);
        var comment = ComputeComment(pr, myLogin, now);
        var ci = ComputeCi(pr);
        var merge = ComputeMerge(pr);
        var needsAction = ComputeNeedsAction(ci, review, comment);
        var urgency = ComputeUrgency(ci, review, comment);

        return new PrStatus
        {
            Review = review,
            Comment = comment,
            Ci = ci,
            Merge = merge,
            NeedsMyAction = needsAction,
            Urgency = urgency,
        };
    }

    private static ReviewStatus ComputeReview(PullRequest pr, string myLogin)
    {
        // Deduplicate: keep most recent review per reviewer
        var byReviewer = pr.LatestReviews
            .Where(r => !IsBot(r.Author) && r.Author != myLogin)
            .GroupBy(r => r.Author)
            .Select(g => g.OrderByDescending(r => r.SubmittedAt).First())
            .ToList();

        var approved = byReviewer.Where(r => r.State == "APPROVED").ToList();
        var changesRequested = byReviewer.Where(r => r.State == "CHANGES_REQUESTED").ToList();

        ReviewState state;
        string? reviewer = null;
        TimeSpan? since = null;

        if (pr.ReviewDecision == "APPROVED" || (pr.ReviewDecision is null && approved.Any() && !changesRequested.Any()))
        {
            state = ReviewState.Approved;
            var latest = approved.OrderByDescending(r => r.SubmittedAt).FirstOrDefault();
            reviewer = latest?.Author;
            since = latest != null ? DateTimeOffset.UtcNow - latest.SubmittedAt : null;
        }
        else if (pr.ReviewDecision == "CHANGES_REQUESTED" || (pr.ReviewDecision is null && changesRequested.Any()))
        {
            state = ReviewState.ChangesRequested;
            var latest = changesRequested.OrderByDescending(r => r.SubmittedAt).FirstOrDefault();
            reviewer = latest?.Author;
            since = latest != null ? DateTimeOffset.UtcNow - latest.SubmittedAt : null;
        }
        else
        {
            state = pr.ReviewRequests.Any() ? ReviewState.Pending : ReviewState.None;
        }

        var pendingReviewers = pr.ReviewRequests
            .Where(r => !byReviewer.Any(rev => rev.Author == r.Login))
            .Select(r => r.Login)
            .ToList();

        return new ReviewStatus { State = state, Reviewer = reviewer, Since = since, PendingReviewers = pendingReviewers };
    }

    private static CommentStatus ComputeComment(PullRequest pr, string myLogin, DateTimeOffset now)
    {
        var myLastCommit = pr.LastCommit?.CommittedDate ?? pr.CreatedAt;

        var openThreads = pr.ReviewThreads.Count(t => !t.IsResolved && !t.IsOutdated);

        // Comments from others, not bots, newer than my last commit
        var newComments = pr.RecentComments
            .Where(c => c.Author != myLogin && !IsBot(c.Author) && c.CreatedAt > myLastCommit)
            .ToList();

        var newReviewComments = pr.RecentReviews
            .Where(r => r.Author != myLogin && !IsBot(r.Author) && r.SubmittedAt > myLastCommit)
            .ToList();

        if (newComments.Any() || newReviewComments.Any())
        {
            var allNew = newComments.Select(c => (c.Author, c.CreatedAt))
                .Concat(newReviewComments.Select(r => (r.Author, r.SubmittedAt)))
                .OrderByDescending(x => x.Item2)
                .First();

            return new CommentStatus
            {
                State = CommentState.New,
                LastAuthor = allNew.Author,
                Since = now - allNew.Item2,
                LastCommentAt = allNew.Item2,
                OpenThreads = openThreads,
            };
        }

        var anyOtherComments = pr.RecentComments.Any(c => c.Author != myLogin && !IsBot(c.Author));
        return new CommentStatus
        {
            State = anyOtherComments || openThreads > 0 ? CommentState.HasComments : CommentState.Quiet,
            OpenThreads = openThreads,
        };
    }

    private static CiStatus ComputeCi(PullRequest pr)
    {
        if (pr.OverallCiStatus is null && !pr.StatusChecks.Any())
            return new CiStatus { State = CiState.None };

        var overallState = pr.OverallCiStatus?.ToUpperInvariant();

        if (overallState == "FAILURE" || overallState == "ERROR")
        {
            var failingCheck = pr.StatusChecks
                .FirstOrDefault(c => c.Conclusion?.ToUpperInvariant() is "FAILURE" or "TIMED_OUT" or "STARTUP_FAILURE"
                                  || c.State?.ToUpperInvariant() is "FAILURE" or "ERROR");
            var allFailing = pr.StatusChecks
                .Where(c => (c.Conclusion ?? c.State ?? "").ToUpperInvariant() is "FAILURE" or "TIMED_OUT" or "STARTUP_FAILURE" or "ERROR")
                .Select(c => c.Name)
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList();
            return new CiStatus { State = CiState.Failing, FailingCheck = failingCheck?.Name, FailingChecks = allFailing };
        }

        if (overallState == "PENDING" || overallState == "EXPECTED" || overallState == "WAITING" || overallState == "IN_PROGRESS" || overallState == "QUEUED")
            return new CiStatus { State = CiState.Pending };

        if (overallState == "SUCCESS")
            return new CiStatus { State = CiState.Passing };

        // Infer from individual checks when rollup is missing
        var conclusions = pr.StatusChecks.Select(c => (c.Conclusion ?? c.State ?? "").ToUpperInvariant()).ToList();
        if (conclusions.Any(c => c is "FAILURE" or "TIMED_OUT" or "STARTUP_FAILURE" or "ERROR"))
        {
            var failingCheck = pr.StatusChecks.FirstOrDefault(c => (c.Conclusion ?? c.State ?? "").ToUpperInvariant() is "FAILURE" or "TIMED_OUT" or "ERROR");
            var allFailing = pr.StatusChecks
                .Where(c => (c.Conclusion ?? c.State ?? "").ToUpperInvariant() is "FAILURE" or "TIMED_OUT" or "STARTUP_FAILURE" or "ERROR")
                .Select(c => c.Name)
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList();
            return new CiStatus { State = CiState.Failing, FailingCheck = failingCheck?.Name, FailingChecks = allFailing };
        }
        if (conclusions.Any(c => c is "PENDING" or "IN_PROGRESS" or "QUEUED" or "WAITING" or "EXPECTED"))
            return new CiStatus { State = CiState.Pending };
        if (conclusions.Any(c => c is "SUCCESS"))
            return new CiStatus { State = CiState.Passing };

        return new CiStatus { State = CiState.None };
    }

    private static MergeStatus ComputeMerge(PullRequest pr)
    {
        var mergeable = pr.Mergeable?.ToUpperInvariant();
        var mergeState = pr.MergeStateStatus?.ToUpperInvariant();

        if (mergeable == "CONFLICTING")
            return new MergeStatus { State = MergeState.Conflict };

        if (mergeState == "BEHIND")
            return new MergeStatus { State = MergeState.BehindBase };

        if (mergeable == "MERGEABLE" || mergeState == "CLEAN" || mergeState == "HAS_HOOKS")
            return new MergeStatus { State = MergeState.Clean };

        if (mergeable == "UNKNOWN" || mergeState == "UNKNOWN" || mergeState is null)
            return new MergeStatus { State = MergeState.Unknown };

        return new MergeStatus { State = MergeState.Clean };
    }

    private static bool ComputeNeedsAction(CiStatus ci, ReviewStatus review, CommentStatus comment)
    {
        return ci.State == CiState.Failing
            || review.State == ReviewState.ChangesRequested
            || comment.OpenThreads > 0;
    }

    private static UrgencyLevel ComputeUrgency(CiStatus ci, ReviewStatus review, CommentStatus comment)
    {
        if (ci.State == CiState.Failing) return UrgencyLevel.CiFailing;
        if (review.State == ReviewState.ChangesRequested) return UrgencyLevel.ChangesRequested;
        if (comment.State == CommentState.New || comment.OpenThreads > 0) return UrgencyLevel.CommentsPending;
        if (review.State == ReviewState.Pending) return UrgencyLevel.AwaitingReview;
        if (review.State == ReviewState.Approved) return UrgencyLevel.Approved;
        return UrgencyLevel.Normal;
    }

    public static bool IsBot(string login)
    {
        if (BotLogins.Contains(login, StringComparer.OrdinalIgnoreCase)) return true;
        return BotSuffixes.Any(s => login.EndsWith(s, StringComparison.OrdinalIgnoreCase));
    }
}
