using System.Text.Json.Serialization;

namespace PrDashboard.Models;

public class CachedPr
{
    [JsonPropertyName("number")] public int Number { get; set; }
    [JsonPropertyName("title")] public string Title { get; set; } = "";
    [JsonPropertyName("branch")] public string Branch { get; set; } = "";
    [JsonPropertyName("author")] public string Author { get; set; } = "";
    [JsonPropertyName("createdAt")] public DateTimeOffset CreatedAt { get; set; }
    [JsonPropertyName("updatedAt")] public DateTimeOffset UpdatedAt { get; set; }
    [JsonPropertyName("status")] public CachedPrStatus Status { get; set; } = new();
    [JsonPropertyName("description")] public string Description { get; set; } = "";
    [JsonPropertyName("jiraTickets")] public List<string> JiraTickets { get; set; } = [];
}

public class CachedPrStatus
{
    [JsonPropertyName("ci")] public string Ci { get; set; } = "none";
    [JsonPropertyName("reviewState")] public string ReviewState { get; set; } = "none";
    [JsonPropertyName("hasConflicts")] public bool HasConflicts { get; set; }
    [JsonPropertyName("isDraft")] public bool IsDraft { get; set; }
}

public class RepoCacheEntry
{
    [JsonPropertyName("repo")] public string Repo { get; set; } = "";
    [JsonPropertyName("updatedAt")] public DateTimeOffset UpdatedAt { get; set; }
    [JsonPropertyName("openPrs")] public List<CachedPr> OpenPrs { get; set; } = [];
}

public class CacheMetadata
{
    [JsonPropertyName("lastUpdated")] public Dictionary<string, DateTimeOffset> LastUpdated { get; set; } = [];
}
