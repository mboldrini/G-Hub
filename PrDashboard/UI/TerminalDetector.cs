namespace PrDashboard.UI;

public static class TerminalDetector
{
    public static bool SupportsEmoji { get; } = DetectEmojiSupport();
    public static bool SupportsLinks { get; } = DetectLinkSupport();

    private static bool DetectEmojiSupport()
    {
        // Windows Terminal and modern terminals set WT_SESSION
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WT_SESSION"))) return true;
        // VS Code terminal
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("TERM_PROGRAM"))) return true;
        // CI environments typically support Unicode
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("CI"))) return true;
        // Old Windows console host: ConEmu, etc.
        if (OperatingSystem.IsWindows())
        {
            // Check if stdout is a real console (not redirected) and check codepage
            if (!Console.IsOutputRedirected)
            {
                try
                {
                    // Old console host doesn't support OSC sequences — rough heuristic
                    var wt = Environment.GetEnvironmentVariable("WT_SESSION");
                    return wt != null;
                }
                catch { return false; }
            }
        }
        return true;
    }

    private static bool DetectLinkSupport()
    {
        // OSC 8 hyperlinks: supported in Windows Terminal, modern terminals
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WT_SESSION"))) return true;
        if (Environment.GetEnvironmentVariable("TERM_PROGRAM") is "vscode" or "iTerm.app") return true;
        return false;
    }
}
