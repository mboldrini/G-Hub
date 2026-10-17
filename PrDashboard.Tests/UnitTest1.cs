using System.Text.Json;
using PrDashboard.Logic;
using PrDashboard.Models;

namespace PrDashboard.Tests;

public class PrAnalyzerTests
{
    private static readonly DateTimeOffset Now = new(2026, 9, 10, 12, 0, 0, TimeSpan.Zero);
    private const string MyLogin = "mylogin";

    private static PullRequest LoadFixture(string name)
    {
        var path = Path.Combine("fixtures", "synthetic", name);
        var json = File.ReadAllText(path);
        var opts = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<PullRequest>(json, opts)!;
    }

    [Fact]
    public void Approved_PR_has_Approved_review_state()
    {
        var pr = LoadFixture("pr_approved.json");
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(ReviewState.Approved, status.Review.State);
        Assert.Equal("reviewer1", status.Review.Reviewer);
    }

    [Fact]
    public void Approved_PR_CI_passing_is_not_needs_action()
    {
        var pr = LoadFixture("pr_approved.json");
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.False(status.NeedsMyAction);
        Assert.Equal(UrgencyLevel.Approved, status.Urgency);
    }

    [Fact]
    public void CI_failing_sets_CiFailing_urgency_and_needs_action()
    {
        var pr = LoadFixture("pr_ci_failing.json");
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(CiState.Failing, status.Ci.State);
        Assert.Equal("unit-tests", status.Ci.FailingCheck);
        Assert.True(status.NeedsMyAction);
        Assert.Equal(UrgencyLevel.CiFailing, status.Urgency);
    }

    [Fact]
    public void Changes_requested_sets_correct_state_and_needs_action()
    {
        var pr = LoadFixture("pr_changes_requested.json");
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(ReviewState.ChangesRequested, status.Review.State);
        Assert.Equal("reviewer2", status.Review.Reviewer);
        Assert.True(status.NeedsMyAction);
        Assert.Equal(UrgencyLevel.ChangesRequested, status.Urgency);
    }

    [Fact]
    public void Open_thread_contributes_to_needs_action()
    {
        var pr = LoadFixture("pr_changes_requested.json");
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(1, status.Comment.OpenThreads);
        Assert.True(status.NeedsMyAction);
    }

    [Fact]
    public void New_comment_after_my_commit_is_marked_New()
    {
        var pr = LoadFixture("pr_new_comment.json");
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(CommentState.New, status.Comment.State);
        Assert.Equal("reviewer3", status.Comment.LastAuthor);
    }

    [Fact]
    public void Bot_comments_are_ignored()
    {
        var pr = LoadFixture("pr_bot_comments_ignored.json");
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(CommentState.Quiet, status.Comment.State);
    }

    [Fact]
    public void My_own_comments_are_ignored()
    {
        var pr = new PullRequest
        {
            Repo = "org/repo", Number = 99, Title = "X",
            CreatedAt = Now.AddDays(-5),
            UpdatedAt = Now.AddDays(-1),
            LastCommit = new Commit { CommittedDate = Now.AddDays(-3) },
            RecentComments =
            [
                new Comment { Author = MyLogin, CreatedAt = Now.AddDays(-1), Body = "My comment" }
            ]
        };
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(CommentState.Quiet, status.Comment.State);
    }

    [Fact]
    public void IsBot_detects_bot_suffixes()
    {
        Assert.True(PrAnalyzer.IsBot("dependabot[bot]"));
        Assert.True(PrAnalyzer.IsBot("renovate[bot]"));
        Assert.True(PrAnalyzer.IsBot("dependabot"));
        Assert.True(PrAnalyzer.IsBot("codecov-commenter"));
        Assert.True(PrAnalyzer.IsBot("my-bot"));
        Assert.False(PrAnalyzer.IsBot("reviewer1"));
        Assert.False(PrAnalyzer.IsBot("mylogin"));
    }

    [Fact]
    public void ReviewDecision_null_approved_when_approved_and_no_changes_requested()
    {
        var pr = new PullRequest
        {
            ReviewDecision = null,
            LatestReviews =
            [
                new Review { Author = "rev1", State = "APPROVED", SubmittedAt = Now.AddHours(-2) }
            ]
        };
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(ReviewState.Approved, status.Review.State);
    }

    [Fact]
    public void ReviewDecision_null_not_approved_when_changes_requested_present()
    {
        var pr = new PullRequest
        {
            ReviewDecision = null,
            LatestReviews =
            [
                new Review { Author = "rev1", State = "APPROVED", SubmittedAt = Now.AddHours(-2) },
                new Review { Author = "rev2", State = "CHANGES_REQUESTED", SubmittedAt = Now.AddHours(-1) }
            ]
        };
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(ReviewState.ChangesRequested, status.Review.State);
    }

    [Fact]
    public void Conflict_merge_state_detected()
    {
        var pr = new PullRequest { Mergeable = "CONFLICTING" };
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(MergeState.Conflict, status.Merge.State);
    }

    [Fact]
    public void Behind_base_detected()
    {
        var pr = new PullRequest { Mergeable = "MERGEABLE", MergeStateStatus = "BEHIND" };
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(MergeState.BehindBase, status.Merge.State);
    }

    [Fact]
    public void CI_urgency_beats_changes_requested()
    {
        var pr = new PullRequest
        {
            OverallCiStatus = "FAILURE",
            ReviewDecision = "CHANGES_REQUESTED",
            StatusChecks = [new StatusCheck { Name = "build", Conclusion = "FAILURE" }],
            LatestReviews = [new Review { Author = "rev1", State = "CHANGES_REQUESTED", SubmittedAt = Now.AddHours(-1) }]
        };
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(UrgencyLevel.CiFailing, status.Urgency);
    }

    [Fact]
    public void Outdated_threads_not_counted_as_open()
    {
        var pr = new PullRequest
        {
            ReviewThreads =
            [
                new ReviewThread { IsResolved = false, IsOutdated = true },
                new ReviewThread { IsResolved = true, IsOutdated = false },
            ]
        };
        var status = PrAnalyzer.Analyze(pr, MyLogin, Now);
        Assert.Equal(0, status.Comment.OpenThreads);
        Assert.False(status.NeedsMyAction);
    }
}

public class TimeFormatterTests
{
    [Fact]
    public void Under_60_seconds_returns_agora_mesmo()
    {
        var now = DateTimeOffset.UtcNow;
        Assert.Equal("agora mesmo", TimeFormatter.Ago(now.AddSeconds(-30), now));
    }

    [Fact]
    public void Minutes_formatted_correctly()
    {
        var now = DateTimeOffset.UtcNow;
        Assert.Equal("há 45min", TimeFormatter.Ago(now.AddMinutes(-45), now));
    }

    [Fact]
    public void Hours_formatted_correctly()
    {
        var now = DateTimeOffset.UtcNow;
        Assert.Equal("há 3h", TimeFormatter.Ago(now.AddHours(-3), now));
    }

    [Fact]
    public void Days_formatted_correctly()
    {
        var now = DateTimeOffset.UtcNow;
        Assert.Equal("há 5d", TimeFormatter.Ago(now.AddDays(-5), now));
    }
}
