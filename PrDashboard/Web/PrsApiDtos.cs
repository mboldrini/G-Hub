using PrDashboard.Logic;
using PrDashboard.Models;

namespace PrDashboard.Web;

public class PrsApiResponse
{
    public string Login { get; set; } = "";
    public string UpdatedAt { get; set; } = "";
    public int IntervalSeconds { get; set; }
    public bool IsLoading { get; set; }
    public string? Error { get; set; }
    public string Language { get; set; } = "pt-BR";
    public string Theme { get; set; } = "dark";
    public List<string> CollapsedRepos { get; set; } = [];
    public string MyPrsFilter { get; set; } = "";
    public string MyPrsView { get; set; } = "repo";
    public bool SidebarOpen { get; set; } = true;
    public List<string> RepoOrder { get; set; } = [];
    public bool NotifCommentVisual { get; set; } = true;
    public bool NotifCommentAudio { get; set; } = true;
    public bool NotifCiVisual { get; set; } = true;
    public bool NotifCiAudio { get; set; } = true;
    public bool NotifMergedVisual { get; set; } = true;
    public bool NotifMergedAudio { get; set; } = true;
    public bool NotifApprovedVisual { get; set; } = true;
    public bool NotifApprovedAudio { get; set; } = true;
    public bool NotifReadyVisual { get; set; } = true;
    public bool NotifReadyAudio { get; set; } = true;
    public bool HideBotReviews { get; set; } = false;
    public List<PrDto> MyPrs { get; set; } = [];
    public List<ReviewPrDto> ReviewPrs { get; set; } = [];
    public List<MergedPrDto> MergedPrs { get; set; } = [];
}

public class PrDto
{
    public string Repo { get; set; } = "";
    public int Number { get; set; }
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public bool IsDraft { get; set; }
    public string Author { get; set; } = "";
    public string AgeAgo { get; set; } = "";
    public string Urgency { get; set; } = "";
    public bool NeedsAction { get; set; }
    public string ReviewState { get; set; } = "";
    public string? ReviewedBy { get; set; }
    public string? ReviewedAgo { get; set; }
    public List<string> PendingReviewers { get; set; } = [];
    public string CiState { get; set; } = "";
    public string? FailingCheck { get; set; }
    public List<string> FailingChecks { get; set; } = [];
    public string CreatedAtIso { get; set; } = "";
    public string CommentState { get; set; } = "";
    public string? LastCommentBy { get; set; }
    public string? LastCommentAgo { get; set; }
    public string? LastCommentAt { get; set; }
    public int OpenThreads { get; set; }
    public string MergeState { get; set; } = "";
    public string HeadRef { get; set; } = "";
    public List<string> Assignees { get; set; } = [];
    public string Body { get; set; } = "";
    public string? LastCommentBody { get; set; }
    public List<PrCommentDto> Comments { get; set; } = [];
}

public class ReviewPrDto
{
    public string Repo { get; set; } = "";
    public int Number { get; set; }
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public string Author { get; set; } = "";
    public string AgeAgo { get; set; } = "";
    public bool IsDraft { get; set; }
    public string HeadRef { get; set; } = "";
    public string Urgency { get; set; } = "";
    public string CiState { get; set; } = "";
    public string? FailingCheck { get; set; }
    public List<string> FailingChecks { get; set; } = [];
    public string CreatedAtIso { get; set; } = "";
    public string CommentState { get; set; } = "";
    public string? LastCommentBy { get; set; }
    public string? LastCommentAgo { get; set; }
    public int OpenThreads { get; set; }
    public string MergeState { get; set; } = "";
    public string Body { get; set; } = "";
    public string? LastCommentBody { get; set; }
    public List<PrCommentDto> Comments { get; set; } = [];
}

public class PrCommentDto
{
    public string Author { get; set; } = "";
    public string Ago { get; set; } = "";
    public string Body { get; set; } = "";
}

public class MergedPrDto
{
    public string Repo { get; set; } = "";
    public int Number { get; set; }
    public string Title { get; set; } = "";
    public string Url { get; set; } = "";
    public string MergedAgo { get; set; } = "";
    public string Duration { get; set; } = "";
}

public static class PrsApiDtos
{
    public static PrDto MapMyPr(PullRequest pr, DateTimeOffset now, string lang = "pt-BR")
    {
        var s = pr.Status;
        var suffix = TimeFormatter.Suffix(lang);
        return new PrDto
        {
            Repo = pr.Repo,
            Number = pr.Number,
            Title = pr.Title,
            Url = pr.Url,
            IsDraft = pr.IsDraft,
            Author = pr.Author ?? "",
            AgeAgo = TimeFormatter.Ago(pr.CreatedAt, now, lang),
            Urgency = s?.Urgency.ToString() ?? "Normal",
            NeedsAction = s?.NeedsMyAction ?? false,
            ReviewState = s?.Review.State.ToString() ?? "None",
            ReviewedBy = s?.Review.Reviewer,
            ReviewedAgo = s?.Review.Since is { } rs ? $"{TimeFormatter.Duration(rs, lang)} {suffix}" : null,
            PendingReviewers = s?.Review.PendingReviewers ?? [],
            CiState = s?.Ci.State.ToString() ?? "None",
            FailingCheck = s?.Ci.FailingCheck,
            FailingChecks = s?.Ci.FailingChecks ?? [],
            CreatedAtIso = pr.CreatedAt.ToString("O"),
            CommentState = s?.Comment.State.ToString() ?? "Quiet",
            LastCommentBy = s?.Comment.LastAuthor,
            LastCommentAgo = s?.Comment.Since is { } cs ? $"{TimeFormatter.Duration(cs, lang)} {suffix}" : null,
            LastCommentAt = s?.Comment.LastCommentAt?.ToString("O"),
            OpenThreads = s?.Comment.OpenThreads ?? 0,
            MergeState = s?.Merge.State.ToString() ?? "Unknown",
            HeadRef = pr.HeadRef,
            Assignees = pr.Assignees,
            Body = pr.Body,
            LastCommentBody = pr.RecentComments.OrderByDescending(c => c.CreatedAt).FirstOrDefault()?.Body,
            Comments = pr.RecentComments
                .Select(c => (At: c.CreatedAt, Dto: new PrCommentDto { Author = c.Author, Ago = TimeFormatter.Ago(c.CreatedAt, now, lang), Body = c.Body }))
                .Concat(pr.RecentReviews
                    .Where(r => !PrAnalyzer.IsBot(r.Author) &&
                               (!string.IsNullOrWhiteSpace(r.Body) || r.State is "APPROVED" or "CHANGES_REQUESTED" or "DISMISSED"))
                    .Select(r => (At: r.SubmittedAt, Dto: new PrCommentDto { Author = r.Author, Ago = TimeFormatter.Ago(r.SubmittedAt, now, lang), Body = !string.IsNullOrWhiteSpace(r.Body) ? r.Body : ReviewStateLabel(r.State, lang) })))
                .OrderByDescending(x => x.At)
                .Select(x => x.Dto)
                .ToList(),
        };
    }

    public static ReviewPrDto MapReviewPr(PullRequest pr, DateTimeOffset now, string lang = "pt-BR")
    {
        var s = pr.Status;
        var suffix = TimeFormatter.Suffix(lang);
        return new ReviewPrDto
        {
            Repo = pr.Repo,
            Number = pr.Number,
            Title = pr.Title,
            Url = pr.Url,
            Author = pr.Author ?? "",
            AgeAgo = TimeFormatter.Ago(pr.CreatedAt, now, lang),
            IsDraft = pr.IsDraft,
            HeadRef = pr.HeadRef,
            Urgency = s?.Urgency.ToString() ?? "Normal",
            CiState = s?.Ci.State.ToString() ?? "None",
            FailingCheck = s?.Ci.FailingCheck,
            FailingChecks = s?.Ci.FailingChecks ?? [],
            CreatedAtIso = pr.CreatedAt.ToString("O"),
            CommentState = s?.Comment.State.ToString() ?? "Quiet",
            LastCommentBy = s?.Comment.LastAuthor,
            LastCommentAgo = s?.Comment.Since is { } cs ? $"{TimeFormatter.Duration(cs, lang)} {suffix}" : null,
            OpenThreads = s?.Comment.OpenThreads ?? 0,
            MergeState = s?.Merge.State.ToString() ?? "Unknown",
            Body = pr.Body,
            LastCommentBody = pr.RecentComments.OrderByDescending(c => c.CreatedAt).FirstOrDefault()?.Body,
            Comments = pr.RecentComments
                .Select(c => (At: c.CreatedAt, Dto: new PrCommentDto { Author = c.Author, Ago = TimeFormatter.Ago(c.CreatedAt, now, lang), Body = c.Body }))
                .Concat(pr.RecentReviews
                    .Where(r => !PrAnalyzer.IsBot(r.Author) &&
                               (!string.IsNullOrWhiteSpace(r.Body) || r.State is "APPROVED" or "CHANGES_REQUESTED" or "DISMISSED"))
                    .Select(r => (At: r.SubmittedAt, Dto: new PrCommentDto { Author = r.Author, Ago = TimeFormatter.Ago(r.SubmittedAt, now, lang), Body = !string.IsNullOrWhiteSpace(r.Body) ? r.Body : ReviewStateLabel(r.State, lang) })))
                .OrderByDescending(x => x.At)
                .Select(x => x.Dto)
                .ToList(),
        };
    }

    private static string ReviewStateLabel(string state, string lang) => (state, lang) switch
    {
        ("APPROVED", "pt-BR") => "*aprovado*",
        ("APPROVED", _) => "*approved*",
        ("CHANGES_REQUESTED", "pt-BR") => "*solicitou mudanças*",
        ("CHANGES_REQUESTED", _) => "*requested changes*",
        ("DISMISSED", "pt-BR") => "*review descartado*",
        ("DISMISSED", _) => "*review dismissed*",
        _ => ""
    };

    public static MergedPrDto MapMergedPr(MergedPr pr, DateTimeOffset now, string lang = "pt-BR") => new()
    {
        Repo = pr.Repo,
        Number = pr.Number,
        Title = pr.Title,
        Url = pr.Url,
        MergedAgo = TimeFormatter.Ago(pr.MergedAt, now, lang),
        Duration = TimeFormatter.Duration(pr.MergedAt - pr.CreatedAt, lang),
    };
}
