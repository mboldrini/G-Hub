using System.Text.Json;
using PrDashboard.Models;

namespace PrDashboard.Config;

public static class ConfigLoader
{
    private static readonly JsonSerializerOptions Opts = new() { PropertyNameCaseInsensitive = true, ReadCommentHandling = JsonCommentHandling.Skip };

    public static AppConfig Load(string path)
    {
        if (!File.Exists(path))
            return new AppConfig();

        var json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<AppConfig>(json, Opts) ?? new AppConfig();
    }
}
