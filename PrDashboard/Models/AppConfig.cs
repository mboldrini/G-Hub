using System.Text.Json.Serialization;

namespace PrDashboard.Models;

public class AppConfig
{
    [JsonPropertyName("intervalSeconds")] public int IntervalSeconds { get; set; } = 300;
    [JsonPropertyName("outputDir")] public string OutputDir { get; set; } = "./out";
    [JsonPropertyName("historyDays")] public int HistoryDays { get; set; } = 30;
    [JsonPropertyName("hostname")] public string Hostname { get; set; } = "github.com";
    [JsonPropertyName("ignoredOrgs")] public List<string> IgnoredOrgs { get; set; } = [];
    [JsonPropertyName("ignoredRepos")] public List<string> IgnoredRepos { get; set; } = [];
    [JsonPropertyName("watchedRepos")] public List<string> WatchedRepos { get; set; } = [];
    [JsonPropertyName("hideDrafts")] public bool HideDrafts { get; set; } = false;
    [JsonPropertyName("webPort")] public int WebPort { get; set; } = 5555;
    [JsonPropertyName("visibleColumns")] public List<string> VisibleColumns { get; set; } =
        ["repo", "number", "title", "review", "ci", "comment", "merge", "age"];
    [JsonPropertyName("language")] public string Language { get; set; } = "pt-BR";
    [JsonPropertyName("theme")] public string Theme { get; set; } = "dark";
    [JsonPropertyName("collapsedRepos")] public List<string> CollapsedRepos { get; set; } = [];
    [JsonPropertyName("myPrsFilter")] public string MyPrsFilter { get; set; } = "";
    [JsonPropertyName("myPrsView")] public string MyPrsView { get; set; } = "repo";
    [JsonPropertyName("sidebarOpen")] public bool SidebarOpen { get; set; } = true;
    [JsonPropertyName("repoOrder")] public List<string> RepoOrder { get; set; } = [];
}
