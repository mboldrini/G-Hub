using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using PrDashboard.Models;

namespace PrDashboard.IO;

public class StateStore(string outputDir)
{
    private static readonly JsonSerializerOptions Opts = new() { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    private static readonly Mutex GlobalMutex = new(false, "Global\\PrDashboard_OutputMutex");

    private string StatePath => Path.Combine(outputDir, "prs-state.json");

    public PrsState Load()
    {
        if (!File.Exists(StatePath)) return new PrsState();
        try
        {
            var json = File.ReadAllText(StatePath);
            return JsonSerializer.Deserialize<PrsState>(json, Opts) ?? new PrsState();
        }
        catch { return new PrsState(); }
    }

    public void Save(PrsState state)
    {
        var json = JsonSerializer.Serialize(state, Opts);
        WriteAtomic(StatePath, json);
    }

    public void UpdateAndSave(
        List<PullRequest> myPrs,
        List<PullRequest> reviewPrs,
        List<MergedPr> merged,
        List<ClosedPr> closed)
    {
        bool acquired = false;
        try
        {
            acquired = GlobalMutex.WaitOne(TimeSpan.FromSeconds(30));
            if (!acquired) throw new TimeoutException("Timeout aguardando mutex de escrita.");

            var state = Load();
            var now = DateTimeOffset.UtcNow.ToString("o");

            foreach (var pr in myPrs.Concat(reviewPrs))
            {
                var key = $"{pr.Repo}#{pr.Number}";
                if (!state.FirstSeen.ContainsKey(key))
                    state.FirstSeen[key] = now;
                state.LastEvent[key] = pr.UpdatedAt.ToString("o");
            }

            // Merge into history (no duplicates by repo+number)
            foreach (var m in merged)
            {
                if (!state.MergedHistory.Any(h => h.Repo == m.Repo && h.Number == m.Number))
                {
                    var key = $"{m.Repo}#{m.Number}";
                    m.FirstSeenAt = state.FirstSeen.GetValueOrDefault(key);
                    state.MergedHistory.Add(m);
                }
            }

            foreach (var c in closed)
            {
                if (!state.ClosedHistory.Any(h => h.Repo == c.Repo && h.Number == c.Number))
                    state.ClosedHistory.Add(c);
            }

            Save(state);
        }
        finally
        {
            if (acquired) GlobalMutex.ReleaseMutex();
        }
    }

    public static void WriteAtomic(string path, string content)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        var tmp = path + ".tmp";
        File.WriteAllText(tmp, content, Encoding.UTF8);
        File.Move(tmp, path, overwrite: true);
    }

    public static string? HashContent(string path)
    {
        if (!File.Exists(path)) return null;
        var bytes = File.ReadAllBytes(path);
        return Convert.ToHexString(SHA256.HashData(bytes));
    }
}
