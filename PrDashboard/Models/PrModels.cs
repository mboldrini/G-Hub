using System.Text.Json.Serialization;

namespace PrDashboard.Models;

public class PullRequest
{
    public string Repo { get; set; } = "";
    public int Number { get; set; }
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public bool IsDraft { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
    public string HeadRef { get; set; } = "";
    public string BaseRef { get; set; } = "";
    public string? ReviewDecision { get; set; }
    public List<Review> LatestReviews { get; set; } = [];
    public List<ReviewRequest> ReviewRequests { get; set; } = [];
    public Commit? LastCommit { get; set; }
    public string? Mergeable { get; set; }
    public string? MergeStateStatus { get; set; }
    public List<Comment> RecentComments { get; set; } = [];
    public List<Review> RecentReviews { get; set; } = [];
    public List<ReviewThread> ReviewThreads { get; set; } = [];
    public int Additions { get; set; }
    public int Deletions { get; set; }
    public int ChangedFiles { get; set; }
    public List<StatusCheck> StatusChecks { get; set; } = [];
    public string? OverallCiStatus { get; set; }
    public string? Author { get; set; }
    public string Body { get; set; } = "";

    // Computed by PrAnalyzer
    public PrStatus? Status { get; set; }
}

public class Review
{
    public string Author { get; set; } = "";
    public string State { get; set; } = "";
    public DateTimeOffset SubmittedAt { get; set; }
}

public class ReviewRequest
{
    public string Login { get; set; } = "";
    public bool IsTeam { get; set; }
}

public class Commit
{
    public string MessageHeadline { get; set; } = "";
    public DateTimeOffset CommittedDate { get; set; }
}

public class StatusCheck
{
    public string Name { get; set; } = "";
    public string? Conclusion { get; set; }
    public string? State { get; set; }
    public string? DetailsUrl { get; set; }
}

public class Comment
{
    public string Author { get; set; } = "";
    public DateTimeOffset CreatedAt { get; set; }
    public string Body { get; set; } = "";
}

public class ReviewThread
{
    public bool IsResolved { get; set; }
    public bool IsOutdated { get; set; }
    public string Author { get; set; } = "";
    public DateTimeOffset CreatedAt { get; set; }
    public string? Path { get; set; }
    public string? Url { get; set; }
}

public class PrStatus
{
    public ReviewStatus Review { get; set; } = new();
    public CommentStatus Comment { get; set; } = new();
    public CiStatus Ci { get; set; } = new();
    public MergeStatus Merge { get; set; } = new();
    public bool NeedsMyAction { get; set; }
    public UrgencyLevel Urgency { get; set; }
}

public class ReviewStatus
{
    public ReviewState State { get; set; }
    public string? Reviewer { get; set; }
    public TimeSpan? Since { get; set; }
    public List<string> PendingReviewers { get; set; } = [];
}

public class CommentStatus
{
    public CommentState State { get; set; }
    public string? LastAuthor { get; set; }
    public TimeSpan? Since { get; set; }
    public DateTimeOffset? LastCommentAt { get; set; }
    public int OpenThreads { get; set; }
}

public class CiStatus
{
    public CiState State { get; set; }
    public string? FailingCheck { get; set; }
}

public class MergeStatus
{
    public MergeState State { get; set; }
}

public enum ReviewState { Approved, ChangesRequested, Pending, None }
public enum CommentState { New, HasComments, Quiet }
public enum CiState { Passing, Failing, Pending, None }
public enum MergeState { Clean, Conflict, BehindBase, Unknown }
public enum UrgencyLevel { CiFailing, ChangesRequested, CommentsPending, AwaitingReview, Approved, Normal }

public class MergedPr
{
    public string Repo { get; set; } = "";
    public int Number { get; set; }
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public DateTimeOffset MergedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string? FirstSeenAt { get; set; }
}

public class ClosedPr
{
    public string Repo { get; set; } = "";
    public int Number { get; set; }
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public DateTimeOffset ClosedAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}

public class PrsState
{
    [JsonPropertyName("firstSeen")] public Dictionary<string, string> FirstSeen { get; set; } = [];
    [JsonPropertyName("lastEvent")] public Dictionary<string, string> LastEvent { get; set; } = [];
    [JsonPropertyName("mergedHistory")] public List<MergedPr> MergedHistory { get; set; } = [];
    [JsonPropertyName("closedHistory")] public List<ClosedPr> ClosedHistory { get; set; } = [];
}
