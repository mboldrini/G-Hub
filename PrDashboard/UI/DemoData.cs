using PrDashboard.Models;

namespace PrDashboard.UI;

public static class DemoData
{
    public static (List<PullRequest> MyPrs, List<PullRequest> ReviewPrs, List<MergedPr> MergedPrs) Generate()
    {
        var now = DateTimeOffset.UtcNow;

        var myPrs = new List<PullRequest>
        {
            // ── acme/platform ──────────────────────────────────────────
            new()
            {
                Repo = "acme/platform", Number = 412, Title = "feat: distributed tracing with OpenTelemetry across all services",
                Url = "https://github.com/acme/platform/pull/412", IsDraft = false,
                Author = "demo-user",
                CreatedAt = now.AddDays(-4), UpdatedAt = now.AddHours(-1), HeadRef = "feat/otel-tracing", BaseRef = "main",
                ReviewDecision = "APPROVED", Mergeable = "MERGEABLE", MergeStateStatus = "CLEAN",
                Additions = 486, Deletions = 23, ChangedFiles = 12,
                OverallCiStatus = "SUCCESS",
                StatusChecks = [new() { Name = "ci/build-and-test", Conclusion = "SUCCESS" }, new() { Name = "ci/lint", Conclusion = "SUCCESS" }],
                LatestReviews = [new() { Author = "j.santos", State = "APPROVED", SubmittedAt = now.AddHours(-3) }],
                Status = new PrStatus
                {
                    Review = new() { State = ReviewState.Approved, Reviewer = "j.santos", Since = TimeSpan.FromHours(3) },
                    Comment = new() { State = CommentState.Quiet },
                    Ci = new() { State = CiState.Passing },
                    Merge = new() { State = MergeState.Clean },
                    NeedsMyAction = false, Urgency = UrgencyLevel.Approved
                }
            },
            new()
            {
                Repo = "acme/platform", Number = 408, Title = "feat: circuit breaker pattern for external API calls",
                Url = "https://github.com/acme/platform/pull/408", IsDraft = false,
                Author = "demo-user",
                CreatedAt = now.AddDays(-6), UpdatedAt = now.AddHours(-2), HeadRef = "feat/circuit-breaker", BaseRef = "main",
                ReviewDecision = "REVIEW_REQUIRED", Mergeable = "MERGEABLE", MergeStateStatus = "BLOCKED",
                Additions = 312, Deletions = 18, ChangedFiles = 7,
                OverallCiStatus = "SUCCESS",
                StatusChecks = [new() { Name = "ci/build-and-test", Conclusion = "SUCCESS" }],
                ReviewRequests = [new() { Login = "r.chen", IsTeam = false }, new() { Login = "platform-team", IsTeam = true }],
                Status = new PrStatus
                {
                    Review = new() { State = ReviewState.Pending, PendingReviewers = ["r.chen", "platform-team"] },
                    Comment = new() { State = CommentState.Quiet },
                    Ci = new() { State = CiState.Passing },
                    Merge = new() { State = MergeState.Clean },
                    NeedsMyAction = false, Urgency = UrgencyLevel.AwaitingReview
                }
            },

            // ── acme/backend ───────────────────────────────────────────
            new()
            {
                Repo = "acme/backend", Number = 731, Title = "fix: memory leak in WebSocket connection pool under high load",
                Url = "https://github.com/acme/backend/pull/731", IsDraft = false,
                Author = "demo-user",
                CreatedAt = now.AddDays(-2), UpdatedAt = now.AddMinutes(-45), HeadRef = "fix/ws-pool-leak", BaseRef = "main",
                ReviewDecision = "CHANGES_REQUESTED", Mergeable = "MERGEABLE", MergeStateStatus = "BLOCKED",
                Additions = 94, Deletions = 67, ChangedFiles = 4,
                OverallCiStatus = "SUCCESS",
                StatusChecks = [new() { Name = "ci/build-and-test", Conclusion = "SUCCESS" }],
                LatestReviews = [new() { Author = "m.oliveira", State = "CHANGES_REQUESTED", SubmittedAt = now.AddHours(-8) }],
                ReviewThreads =
                [
                    new() { IsResolved = false, IsOutdated = false, Author = "m.oliveira", Path = "src/WebSockets/Pool.cs" },
                    new() { IsResolved = false, IsOutdated = false, Author = "m.oliveira", Path = "src/WebSockets/Manager.cs" },
                ],
                Status = new PrStatus
                {
                    Review = new() { State = ReviewState.ChangesRequested, Reviewer = "m.oliveira", Since = TimeSpan.FromHours(8) },
                    Comment = new() { State = CommentState.HasComments, OpenThreads = 2 },
                    Ci = new() { State = CiState.Passing },
                    Merge = new() { State = MergeState.Clean },
                    NeedsMyAction = true, Urgency = UrgencyLevel.ChangesRequested
                }
            },
            new()
            {
                Repo = "acme/backend", Number = 726, Title = "perf: replace N+1 queries with batch loading in order service",
                Url = "https://github.com/acme/backend/pull/726", IsDraft = false,
                Author = "demo-user",
                CreatedAt = now.AddDays(-3), UpdatedAt = now.AddHours(-5), HeadRef = "perf/batch-orders", BaseRef = "main",
                ReviewDecision = "REVIEW_REQUIRED", Mergeable = "MERGEABLE", MergeStateStatus = "BLOCKED",
                Additions = 203, Deletions = 88, ChangedFiles = 6,
                OverallCiStatus = "FAILURE",
                StatusChecks =
                [
                    new() { Name = "ci/build-and-test", Conclusion = "FAILURE" },
                    new() { Name = "ci/lint", Conclusion = "SUCCESS" },
                    new() { Name = "ci/sonarcloud", Conclusion = "SUCCESS" },
                ],
                ReviewRequests = [new() { Login = "j.santos", IsTeam = false }],
                Status = new PrStatus
                {
                    Review = new() { State = ReviewState.Pending, PendingReviewers = ["j.santos"] },
                    Comment = new() { State = CommentState.Quiet },
                    Ci = new() { State = CiState.Failing, FailingCheck = "ci/build-and-test" },
                    Merge = new() { State = MergeState.Clean },
                    NeedsMyAction = true, Urgency = UrgencyLevel.CiFailing
                }
            },

            // ── acme/frontend ──────────────────────────────────────────
            new()
            {
                Repo = "acme/frontend", Number = 298, Title = "feat: real-time dashboard with WebSocket + React Query v5",
                Url = "https://github.com/acme/frontend/pull/298", IsDraft = false,
                Author = "demo-user",
                CreatedAt = now.AddDays(-5), UpdatedAt = now.AddHours(-4), HeadRef = "feat/realtime-dashboard", BaseRef = "main",
                ReviewDecision = "APPROVED", Mergeable = "MERGEABLE", MergeStateStatus = "CLEAN",
                Additions = 728, Deletions = 104, ChangedFiles = 18,
                OverallCiStatus = "SUCCESS",
                StatusChecks =
                [
                    new() { Name = "ci/build", Conclusion = "SUCCESS" },
                    new() { Name = "ci/vitest", Conclusion = "SUCCESS" },
                    new() { Name = "ci/playwright", Conclusion = "SUCCESS" },
                ],
                LatestReviews =
                [
                    new() { Author = "r.chen", State = "APPROVED", SubmittedAt = now.AddHours(-6) },
                    new() { Author = "a.lima", State = "APPROVED", SubmittedAt = now.AddHours(-2) },
                ],
                Status = new PrStatus
                {
                    Review = new() { State = ReviewState.Approved, Reviewer = "a.lima", Since = TimeSpan.FromHours(2) },
                    Comment = new() { State = CommentState.Quiet },
                    Ci = new() { State = CiState.Passing },
                    Merge = new() { State = MergeState.Clean },
                    NeedsMyAction = false, Urgency = UrgencyLevel.Approved
                }
            },
            new()
            {
                Repo = "acme/frontend", Number = 291, Title = "refactor: migrate auth flows to Zustand + replace Redux",
                Url = "https://github.com/acme/frontend/pull/291", IsDraft = false,
                Author = "demo-user",
                CreatedAt = now.AddDays(-8), UpdatedAt = now.AddHours(-12), HeadRef = "refactor/zustand-auth", BaseRef = "main",
                ReviewDecision = "REVIEW_REQUIRED", Mergeable = "BEHIND", MergeStateStatus = "BEHIND",
                Additions = 541, Deletions = 892, ChangedFiles = 31,
                OverallCiStatus = "SUCCESS",
                StatusChecks = [new() { Name = "ci/build", Conclusion = "SUCCESS" }, new() { Name = "ci/vitest", Conclusion = "SUCCESS" }],
                ReviewRequests = [new() { Login = "a.lima", IsTeam = false }],
                RecentComments = [new() { Author = "a.lima", CreatedAt = now.AddHours(-12), Body = "LGTM overall, just needs rebase" }],
                Status = new PrStatus
                {
                    Review = new() { State = ReviewState.Pending, PendingReviewers = ["a.lima"] },
                    Comment = new() { State = CommentState.New, LastAuthor = "a.lima", Since = TimeSpan.FromHours(12) },
                    Ci = new() { State = CiState.Passing },
                    Merge = new() { State = MergeState.BehindBase },
                    NeedsMyAction = true, Urgency = UrgencyLevel.CommentsPending
                }
            },

            // ── acme/infra ─────────────────────────────────────────────
            new()
            {
                Repo = "acme/infra", Number = 87, Title = "ci: add matrix strategy for multi-region smoke tests",
                Url = "https://github.com/acme/infra/pull/87", IsDraft = false,
                Author = "demo-user",
                CreatedAt = now.AddDays(-1), UpdatedAt = now.AddHours(-1), HeadRef = "ci/matrix-smoke", BaseRef = "main",
                ReviewDecision = "REVIEW_REQUIRED", Mergeable = "MERGEABLE", MergeStateStatus = "BLOCKED",
                Additions = 67, Deletions = 14, ChangedFiles = 3,
                OverallCiStatus = "SUCCESS",
                StatusChecks = [new() { Name = "ci/validate-yaml", Conclusion = "SUCCESS" }],
                ReviewRequests = [new() { Login = "devops-team", IsTeam = true }],
                Status = new PrStatus
                {
                    Review = new() { State = ReviewState.Pending, PendingReviewers = ["devops-team"] },
                    Comment = new() { State = CommentState.Quiet },
                    Ci = new() { State = CiState.Passing },
                    Merge = new() { State = MergeState.Clean },
                    NeedsMyAction = false, Urgency = UrgencyLevel.AwaitingReview
                }
            },
            new()
            {
                Repo = "acme/infra", Number = 84, Title = "chore: upgrade Terraform AWS provider to v5 + migrate state",
                Url = "https://github.com/acme/infra/pull/84", IsDraft = true,
                Author = "demo-user",
                CreatedAt = now.AddDays(-10), UpdatedAt = now.AddDays(-1), HeadRef = "chore/terraform-v5", BaseRef = "main",
                ReviewDecision = null, Mergeable = "MERGEABLE", MergeStateStatus = "DRAFT",
                Additions = 189, Deletions = 134, ChangedFiles = 9,
                Status = new PrStatus
                {
                    Review = new() { State = ReviewState.None },
                    Comment = new() { State = CommentState.Quiet },
                    Ci = new() { State = CiState.None },
                    Merge = new() { State = MergeState.Clean },
                    NeedsMyAction = false, Urgency = UrgencyLevel.Normal
                }
            },
        };

        var reviewPrs = new List<PullRequest>
        {
            new()
            {
                Repo = "acme/platform", Number = 415, Title = "feat: gRPC streaming for analytics ingestion pipeline",
                Url = "https://github.com/acme/platform/pull/415", IsDraft = false,
                Author = "r.chen", HeadRef = "feat/grpc-analytics",
                CreatedAt = now.AddDays(-2), UpdatedAt = now.AddHours(-4),
                ReviewDecision = "REVIEW_REQUIRED",
                Status = new PrStatus
                {
                    Review = new() { State = ReviewState.Pending },
                    Ci = new() { State = CiState.Passing },
                    Merge = new() { State = MergeState.Clean },
                    Urgency = UrgencyLevel.AwaitingReview
                }
            },
            new()
            {
                Repo = "acme/backend", Number = 735, Title = "fix: idempotency key collision on concurrent payment retries",
                Url = "https://github.com/acme/backend/pull/735", IsDraft = false,
                Author = "m.oliveira", HeadRef = "fix/payment-idempotency",
                CreatedAt = now.AddDays(-1), UpdatedAt = now.AddHours(-2),
                ReviewDecision = "REVIEW_REQUIRED",
                Status = new PrStatus
                {
                    Review = new() { State = ReviewState.Pending },
                    Ci = new() { State = CiState.Passing },
                    Merge = new() { State = MergeState.Clean },
                    Urgency = UrgencyLevel.AwaitingReview
                }
            },
            new()
            {
                Repo = "acme/frontend", Number = 302, Title = "feat: skeleton loaders and optimistic updates in cart",
                Url = "https://github.com/acme/frontend/pull/302", IsDraft = false,
                Author = "a.lima", HeadRef = "feat/cart-ux",
                CreatedAt = now.AddHours(-6), UpdatedAt = now.AddHours(-1),
                ReviewDecision = "REVIEW_REQUIRED",
                Status = new PrStatus
                {
                    Review = new() { State = ReviewState.Pending },
                    Ci = new() { State = CiState.Pending },
                    Merge = new() { State = MergeState.Clean },
                    Urgency = UrgencyLevel.AwaitingReview
                }
            },
        };

        var mergedPrs = new List<MergedPr>
        {
            new() { Repo = "acme/platform", Number = 403, Title = "feat: JWT refresh token rotation with Redis backing store", Url = "#", CreatedAt = now.AddDays(-8), MergedAt = now.AddDays(-5) },
            new() { Repo = "acme/backend",  Number = 718, Title = "perf: async bulk indexing for Elasticsearch sync worker",    Url = "#", CreatedAt = now.AddDays(-7), MergedAt = now.AddDays(-4) },
            new() { Repo = "acme/backend",  Number = 714, Title = "fix: deadlock in transaction rollback under load",           Url = "#", CreatedAt = now.AddDays(-6), MergedAt = now.AddDays(-3) },
            new() { Repo = "acme/frontend", Number = 284, Title = "feat: infinite scroll with virtualized list (react-window)", Url = "#", CreatedAt = now.AddDays(-5), MergedAt = now.AddDays(-2) },
            new() { Repo = "acme/infra",    Number = 81,  Title = "ci: cut deploy time 40% with layer caching in Buildkit",    Url = "#", CreatedAt = now.AddDays(-4), MergedAt = now.AddDays(-1) },
            new() { Repo = "acme/platform", Number = 399, Title = "refactor: extract rate limiter into shared middleware pkg",  Url = "#", CreatedAt = now.AddDays(-3), MergedAt = now.AddHours(-8) },
        };

        return (myPrs, reviewPrs, mergedPrs);
    }
}
