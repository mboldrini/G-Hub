namespace PrDashboard.IO;

public class AppLogger(string outputDir) : IDisposable
{
    public static AppLogger? Instance { get; private set; }

    public static AppLogger Initialize(string outputDir)
    {
        Instance?.Dispose();
        Instance = new AppLogger(outputDir);
        return Instance;
    }

    private StreamWriter? _writer;
    private readonly object _lock = new();
    private readonly string _logDir = Path.Combine(outputDir, "logs");

    public void Log(string level, string message)
    {
        lock (_lock)
        {
            try
            {
                EnsureWriter();
                var line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
                _writer?.WriteLine(line);
                _writer?.Flush();
            }
            catch { /* logging must never crash the app */ }
        }
    }

    public void Info(string msg) => Log("INFO", msg);
    public void Warn(string msg) => Log("WARN", msg);
    public void Error(string msg) => Log("ERROR", msg);
    public void Error(Exception ex) => Log("ERROR", $"{ex.GetType().Name}: {ex.Message}");

    private void EnsureWriter()
    {
        if (_writer is not null) return;
        Directory.CreateDirectory(_logDir);
        var path = Path.Combine(_logDir, $"prdashboard-{DateTime.UtcNow:yyyyMMdd}.log");
        RotateIfNeeded(path);
        _writer = new StreamWriter(path, append: true);
    }

    private static void RotateIfNeeded(string path)
    {
        if (!File.Exists(path)) return;
        var info = new FileInfo(path);
        if (info.Length > 5 * 1024 * 1024)
        {
            var rotated = path.Replace(".log", $"-{DateTime.UtcNow:HHmmss}.bak");
            File.Move(path, rotated);
        }
    }

    public void Dispose()
    {
        lock (_lock)
        {
            _writer?.Dispose();
            _writer = null;
        }
        if (ReferenceEquals(Instance, this))
            Instance = null;
    }
}
