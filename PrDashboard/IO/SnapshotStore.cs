using System.Text.Json;
using PrDashboard.Models;

namespace PrDashboard.IO;

public class SnapshotStore(string outputDir)
{
    private static readonly JsonSerializerOptions Opts = new() { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    public bool ExistsMerged(string repo, int number, string monthFolder) =>
        File.Exists(MergedPath(repo, number, monthFolder));

    public void SaveMerged(PrSnapshot snapshot)
    {
        var month = snapshot.MergedAt.ToLocalTime().ToString("yyyy-MM");
        Write(MergedPath(snapshot.Repo, snapshot.Number, month), snapshot);
    }

    public void SaveOpen(PrSnapshot snapshot) =>
        Write(OpenPath(snapshot.Repo, snapshot.Number), snapshot);

    private void Write(string path, PrSnapshot snapshot)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path)!);
        StateStore.WriteAtomic(path, JsonSerializer.Serialize(snapshot, Opts));
    }

    private string MergedPath(string repo, int number, string monthFolder) =>
        Path.Combine(outputDir, "prs", "merged", monthFolder, $"{Slug(repo)}--{number}.json");

    private string OpenPath(string repo, int number) =>
        Path.Combine(outputDir, "prs", "open", $"{Slug(repo)}--{number}.json");

    private static string Slug(string repo) => repo.Replace("/", "__");
}
