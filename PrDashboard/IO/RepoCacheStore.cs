using System.Text.Json;
using System.Text.RegularExpressions;
using PrDashboard.Models;

namespace PrDashboard.IO;

public class RepoCacheStore(string cacheDir)
{
    private static readonly JsonSerializerOptions Opts = new() { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private string ReposDir => Path.Combine(cacheDir, "repos");
    private string MetaPath => Path.Combine(cacheDir, "meta.json");

    private static string ToSlug(string repo) => repo.Replace("/", "__");

    public RepoCacheEntry LoadRepo(string repo)
    {
        var path = Path.Combine(ReposDir, $"{ToSlug(repo)}.json");
        if (!File.Exists(path)) return new RepoCacheEntry { Repo = repo };
        try
        {
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<RepoCacheEntry>(json, Opts) ?? new RepoCacheEntry { Repo = repo };
        }
        catch { return new RepoCacheEntry { Repo = repo }; }
    }

    public void SaveRepo(RepoCacheEntry entry)
    {
        Directory.CreateDirectory(ReposDir);
        var path = Path.Combine(ReposDir, $"{ToSlug(entry.Repo)}.json");
        var json = JsonSerializer.Serialize(entry, Opts);
        StateStore.WriteAtomic(path, json);
    }

    public CacheMetadata LoadMeta()
    {
        if (!File.Exists(MetaPath)) return new CacheMetadata();
        try
        {
            var json = File.ReadAllText(MetaPath);
            return JsonSerializer.Deserialize<CacheMetadata>(json, Opts) ?? new CacheMetadata();
        }
        catch { return new CacheMetadata(); }
    }

    public void SaveMeta(CacheMetadata meta)
    {
        Directory.CreateDirectory(cacheDir);
        var json = JsonSerializer.Serialize(meta, Opts);
        StateStore.WriteAtomic(MetaPath, json);
    }

    public static List<string> ExtractJiraTickets(string body)
    {
        var matches = Regex.Matches(body, @"\b[A-Z]{2,10}-\d+\b");
        return matches.Select(m => m.Value).Distinct().ToList();
    }
}
