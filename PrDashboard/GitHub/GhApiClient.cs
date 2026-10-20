using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using PrDashboard.Models;

namespace PrDashboard.GitHub;

public class GhApiClient(string hostname)
{
    private static readonly JsonSerializerOptions JsonOpts = new() { PropertyNameCaseInsensitive = true };

    public async Task<List<PullRequest>> FetchMyOpenPrsAsync(string login, AppConfig config, CancellationToken ct = default)
    {
        var query = $"is:pr is:open author:{login} archived:false";
        return await FetchPaginatedAsync(GraphQLQueries.MyOpenPrs, query, ParsePr, config, ct);
    }

    public async Task<List<PullRequest>> FetchReviewRequestedPrsAsync(string login, AppConfig config, CancellationToken ct = default)
    {
        var query = $"is:pr is:open review-requested:{login} archived:false";
        return await FetchPaginatedAsync(GraphQLQueries.ReviewRequestedPrs, query, ParsePr, config, ct);
    }

    public async Task<List<PullRequest>> FetchWatchedRepoPrsAsync(string repo, string login, AppConfig config, CancellationToken ct = default)
    {
        var query = $"repo:{repo} is:pr is:open author:{login} archived:false";
        return await FetchPaginatedAsync(GraphQLQueries.MyOpenPrs, query, ParsePr, config, ct);
    }

    public async Task<List<MergedPr>> FetchMergedPrsAsync(string login, int days, CancellationToken ct = default)
    {
        var since = DateTimeOffset.UtcNow.AddDays(-days).ToString("yyyy-MM-dd");
        var query = $"is:pr is:merged author:{login} merged:>={since}";
        return await FetchPaginatedAsync(GraphQLQueries.MergedPrs, query, ParseMergedPr, null, ct);
    }

    public async Task<List<ClosedPr>> FetchClosedUnmergedPrsAsync(string login, int days, CancellationToken ct = default)
    {
        var since = DateTimeOffset.UtcNow.AddDays(-days).ToString("yyyy-MM-dd");
        var query = $"is:pr is:closed is:unmerged author:{login} closed:>={since}";
        return await FetchPaginatedAsync(GraphQLQueries.ClosedUnmergedPrs, query, ParseClosedPr, null, ct);
    }

    private async Task<List<T>> FetchPaginatedAsync<T>(
        string gqlQuery,
        string searchQuery,
        Func<JsonNode, T?> parser,
        AppConfig? config,
        CancellationToken ct)
    {
        var results = new List<T>();
        string? cursor = null;

        while (true)
        {
            var variables = BuildVariables(searchQuery, cursor);
            var payload = JsonSerializer.Serialize(new { query = gqlQuery, variables });

            var json = await RunWithRetryAsync(payload, ct);
            var root = JsonNode.Parse(json) ?? throw new InvalidOperationException("Null response from GraphQL");

            if (root["errors"] is JsonArray errors && errors.Count > 0)
                throw new GhException(-1, errors.ToJsonString());

            var search = root["data"]?["search"] ?? throw new InvalidOperationException("Missing data.search in response");
            var nodes = search["nodes"]?.AsArray() ?? [];

            foreach (var node in nodes)
            {
                if (node is null) continue;
                var item = parser(node);
                if (item is null) continue;

                if (config is not null && item is PullRequest pr)
                {
                    if (config.IgnoredRepos.Contains(pr.Repo, StringComparer.OrdinalIgnoreCase)) continue;
                    var org = pr.Repo.Split('/').FirstOrDefault() ?? "";
                    if (config.IgnoredOrgs.Contains(org, StringComparer.OrdinalIgnoreCase)) continue;
                    if (config.HideDrafts && pr.IsDraft) continue;
                }

                results.Add(item);
            }

            var pageInfo = search["pageInfo"];
            if (pageInfo?["hasNextPage"]?.GetValue<bool>() != true) break;
            cursor = pageInfo["endCursor"]?.GetValue<string>();
        }

        return results;
    }

    private string BuildVariables(string searchQuery, string? cursor)
    {
        var sb = new StringBuilder();
        sb.Append($"{{\"query\":\"{EscapeJson(searchQuery)}\"");
        if (cursor != null)
            sb.Append($",\"cursor\":\"{EscapeJson(cursor)}\"");
        else
            sb.Append(",\"cursor\":null");
        sb.Append('}');
        return sb.ToString();
    }

    private async Task<string> RunWithRetryAsync(string payload, CancellationToken ct)
    {
        int[] backoffSeconds = [5, 15, 30];

        for (int attempt = 0; ; attempt++)
        {
            try
            {
                var escapedPayload = payload.Replace("\"", "\\\"").Replace("\r", "").Replace("\n", " ");
                var args = $"api graphql --input -";

                var psi = new System.Diagnostics.ProcessStartInfo("gh")
                {
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8,
                };

                if (hostname != "github.com")
                    psi.ArgumentList.Add($"--hostname");

                psi.Arguments = hostname != "github.com"
                    ? $"--hostname {hostname} api graphql --input -"
                    : "api graphql --input -";

                using var proc = System.Diagnostics.Process.Start(psi) ?? throw new InvalidOperationException("gh start failed");

                await proc.StandardInput.WriteAsync(payload);
                proc.StandardInput.Close();

                var outTask = proc.StandardOutput.ReadToEndAsync(ct);
                var errTask = proc.StandardError.ReadToEndAsync(ct);
                await proc.WaitForExitAsync(ct);

                var output = await outTask;
                var error = await errTask;

                if (proc.ExitCode != 0)
                    throw new GhException(proc.ExitCode, error.Trim());

                return output;
            }
            catch (GhException ex) when (ex.IsRateLimit && attempt < backoffSeconds.Length)
            {
                var wait = backoffSeconds[attempt];
                Console.Error.WriteLine($"Rate limit atingido. Aguardando {wait}s...");
                await Task.Delay(TimeSpan.FromSeconds(wait), ct);
            }
            catch (GhException ex) when (ex.IsNetwork && attempt < backoffSeconds.Length)
            {
                var wait = backoffSeconds[attempt];
                Console.Error.WriteLine($"Erro de rede. Tentando novamente em {wait}s...");
                await Task.Delay(TimeSpan.FromSeconds(wait), ct);
            }
            catch (GhException ex) when (ex.IsSso)
            {
                throw new GhException(ex.ExitCode,
                    $"Erro SSO/SAML: autentique-se na organização.\nExecute: gh auth login\n\nDetalhe: {ex.Message}");
            }
        }
    }

    private static PullRequest? ParsePr(JsonNode node)
    {
        try
        {
            var pr = new PullRequest
            {
                Repo = node["repository"]?["nameWithOwner"]?.GetValue<string>() ?? "",
                Number = node["number"]?.GetValue<int>() ?? 0,
                Title = node["title"]?.GetValue<string>() ?? "",
                Body = node["body"]?.GetValue<string>() ?? "",
                Url = node["url"]?.GetValue<string>() ?? "",
                Author = node["author"]?["login"]?.GetValue<string>(),
                IsDraft = node["isDraft"]?.GetValue<bool>() ?? false,
                CreatedAt = ParseDate(node["createdAt"]),
                UpdatedAt = ParseDate(node["updatedAt"]),
                HeadRef = node["headRefName"]?.GetValue<string>() ?? "",
                BaseRef = node["baseRefName"]?.GetValue<string>() ?? "",
                ReviewDecision = node["reviewDecision"]?.GetValue<string>(),
                Mergeable = node["mergeable"]?.GetValue<string>(),
                MergeStateStatus = node["mergeStateStatus"]?.GetValue<string>(),
                Additions = node["additions"]?.GetValue<int>() ?? 0,
                Deletions = node["deletions"]?.GetValue<int>() ?? 0,
                ChangedFiles = node["changedFiles"]?.GetValue<int>() ?? 0,
            };

            pr.LatestReviews = ParseReviews(node["latestReviews"]?["nodes"]);
            pr.ReviewRequests = ParseReviewRequests(node["reviewRequests"]?["nodes"]);
            pr.Assignees = ParseLogins(node["assignees"]?["nodes"]);
            pr.RecentComments = ParseComments(node["comments"]?["nodes"]);
            pr.RecentReviews = ParseReviews(node["reviews"]?["nodes"]);
            pr.ReviewThreads = ParseThreads(node["reviewThreads"]?["nodes"]);

            var lastCommitNode = node["commits"]?["nodes"]?.AsArray().LastOrDefault()?["commit"];
            if (lastCommitNode != null)
            {
                pr.LastCommit = new Commit
                {
                    MessageHeadline = lastCommitNode["messageHeadline"]?.GetValue<string>() ?? "",
                    CommittedDate = ParseDate(lastCommitNode["committedDate"]),
                };

                var rollup = lastCommitNode["statusCheckRollup"];
                if (rollup != null)
                {
                    pr.OverallCiStatus = rollup["state"]?.GetValue<string>();
                    pr.StatusChecks = ParseChecks(rollup["contexts"]?["nodes"]);
                }
            }

            return pr;
        }
        catch { return null; }
    }

    private static MergedPr? ParseMergedPr(JsonNode node)
    {
        try
        {
            return new MergedPr
            {
                Repo = node["repository"]?["nameWithOwner"]?.GetValue<string>() ?? "",
                Number = node["number"]?.GetValue<int>() ?? 0,
                Title = node["title"]?.GetValue<string>() ?? "",
                Url = node["url"]?.GetValue<string>() ?? "",
                CreatedAt = ParseDate(node["createdAt"]),
                MergedAt = ParseDate(node["mergedAt"]),
            };
        }
        catch { return null; }
    }

    private static ClosedPr? ParseClosedPr(JsonNode node)
    {
        try
        {
            return new ClosedPr
            {
                Repo = node["repository"]?["nameWithOwner"]?.GetValue<string>() ?? "",
                Number = node["number"]?.GetValue<int>() ?? 0,
                Title = node["title"]?.GetValue<string>() ?? "",
                Url = node["url"]?.GetValue<string>() ?? "",
                CreatedAt = ParseDate(node["createdAt"]),
                ClosedAt = ParseDate(node["closedAt"]),
            };
        }
        catch { return null; }
    }

    private static List<string> ParseLogins(JsonNode? nodes)
    {
        var list = new List<string>();
        if (nodes is not JsonArray arr) return list;
        foreach (var n in arr)
        {
            if (n is null) continue;
            var login = n["login"]?.GetValue<string>();
            if (!string.IsNullOrWhiteSpace(login)) list.Add(login);
        }
        return list;
    }

    private static List<Review> ParseReviews(JsonNode? nodes)
    {
        var list = new List<Review>();
        if (nodes is not JsonArray arr) return list;
        foreach (var n in arr)
        {
            if (n is null) continue;
            list.Add(new Review
            {
                Author = n["author"]?["login"]?.GetValue<string>() ?? "",
                State = n["state"]?.GetValue<string>() ?? "",
                SubmittedAt = ParseDate(n["submittedAt"]),
            });
        }
        return list;
    }

    private static List<ReviewRequest> ParseReviewRequests(JsonNode? nodes)
    {
        var list = new List<ReviewRequest>();
        if (nodes is not JsonArray arr) return list;
        foreach (var n in arr)
        {
            if (n is null) continue;
            var reviewer = n["requestedReviewer"];
            if (reviewer is null) continue;
            var login = reviewer["login"]?.GetValue<string>();
            var teamName = reviewer["name"]?.GetValue<string>();
            list.Add(new ReviewRequest
            {
                Login = login ?? teamName ?? "",
                IsTeam = login is null,
            });
        }
        return list;
    }

    private static List<Comment> ParseComments(JsonNode? nodes)
    {
        var list = new List<Comment>();
        if (nodes is not JsonArray arr) return list;
        foreach (var n in arr)
        {
            if (n is null) continue;
            list.Add(new Comment
            {
                Author = n["author"]?["login"]?.GetValue<string>() ?? "",
                CreatedAt = ParseDate(n["createdAt"]),
                Body = n["body"]?.GetValue<string>() ?? "",
            });
        }
        return list;
    }

    private static List<ReviewThread> ParseThreads(JsonNode? nodes)
    {
        var list = new List<ReviewThread>();
        if (nodes is not JsonArray arr) return list;
        foreach (var n in arr)
        {
            if (n is null) continue;
            var firstComment = n["comments"]?["nodes"]?.AsArray().FirstOrDefault();
            list.Add(new ReviewThread
            {
                IsResolved = n["isResolved"]?.GetValue<bool>() ?? false,
                IsOutdated = n["isOutdated"]?.GetValue<bool>() ?? false,
                Author = firstComment?["author"]?["login"]?.GetValue<string>() ?? "",
                CreatedAt = firstComment != null ? ParseDate(firstComment["createdAt"]) : default,
                Path = firstComment?["path"]?.GetValue<string>(),
                Url = firstComment?["url"]?.GetValue<string>(),
            });
        }
        return list;
    }

    private static List<StatusCheck> ParseChecks(JsonNode? nodes)
    {
        var list = new List<StatusCheck>();
        if (nodes is not JsonArray arr) return list;
        foreach (var n in arr)
        {
            if (n is null) continue;
            var name = n["name"]?.GetValue<string>() ?? n["context"]?.GetValue<string>() ?? "";
            list.Add(new StatusCheck
            {
                Name = name,
                Conclusion = n["conclusion"]?.GetValue<string>(),
                State = n["state"]?.GetValue<string>(),
                DetailsUrl = n["detailsUrl"]?.GetValue<string>() ?? n["targetUrl"]?.GetValue<string>(),
            });
        }
        return list;
    }

    private static DateTimeOffset ParseDate(JsonNode? node)
    {
        if (node is null) return default;
        var s = node.GetValue<string>();
        return DateTimeOffset.TryParse(s, out var dt) ? dt : default;
    }

    private static string EscapeJson(string s) =>
        s.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\n", "\\n").Replace("\r", "\\r");
}
