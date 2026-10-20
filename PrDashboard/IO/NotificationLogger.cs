namespace PrDashboard.IO;

public class NotificationLogger(string outputDir)
{
    public static NotificationLogger? Instance { get; private set; }

    public static NotificationLogger Initialize(string outputDir)
    {
        Instance = new NotificationLogger(outputDir);
        return Instance;
    }

    public void Log(string type, string repo, int prNumber, string prTitle)
    {
        try
        {
            var dir = Path.Combine(WeeklyFolder.GetPath(outputDir, DateTime.Now), "notifications");
            Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, $"notifications-{DateTime.UtcNow:yyyyMMdd}.log");
            var entry = System.Text.Json.JsonSerializer.Serialize(new
            {
                timestamp = DateTime.UtcNow.ToString("O"),
                type,
                repo,
                prNumber,
                prTitle
            });
            File.AppendAllText(path, entry + Environment.NewLine);
        }
        catch { /* logging must never crash the app */ }
    }
}
