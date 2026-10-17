using PrDashboard.Models;

namespace PrDashboard.Notifications;

public static class ToastNotifier
{
    private static readonly HashSet<string> _notifiedKeys = [];

    public static void NotifyNewNeedsAction(IEnumerable<PullRequest> prs, string appName = "PrDashboard")
    {
        if (!OperatingSystem.IsWindows()) return;

        var newNeedsAction = prs
            .Where(p => p.Status?.NeedsMyAction == true)
            .Where(p => _notifiedKeys.Add($"{p.Repo}#{p.Number}"))
            .ToList();

        foreach (var pr in newNeedsAction)
            ShowToast(appName, $"PR #{pr.Number} precisa de ação", TruncateTitle(pr.Title), pr.Url);
    }

    private static void ShowToast(string appName, string title, string body, string? url)
    {
        try
        {
            // Use PowerShell to show a Windows toast notification
            var script = $"""
                [Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] | Out-Null
                [Windows.Data.Xml.Dom.XmlDocument, Windows.Data.Xml.Dom, ContentType = WindowsRuntime] | Out-Null
                $template = [Windows.UI.Notifications.ToastTemplateType]::ToastText02
                $xml = [Windows.UI.Notifications.ToastNotificationManager]::GetTemplateContent($template)
                $xml.SelectSingleNode('//text[@id="1"]').InnerText = '{EscapePs(title)}'
                $xml.SelectSingleNode('//text[@id="2"]').InnerText = '{EscapePs(body)}'
                $toast = [Windows.UI.Notifications.ToastNotification]::new($xml)
                [Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('{EscapePs(appName)}').Show($toast)
                """;

            var psi = new System.Diagnostics.ProcessStartInfo("powershell.exe",
                $"-NoProfile -NonInteractive -Command \"{script.Replace("\"", "\\\"")}\"")
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardError = true,
            };
            var proc = System.Diagnostics.Process.Start(psi);
            proc?.WaitForExit(3000);
        }
        catch { /* toast is optional, never crash */ }
    }

    private static string TruncateTitle(string title) =>
        title.Length > 80 ? title[..77] + "..." : title;

    private static string EscapePs(string s) =>
        s.Replace("'", "''").Replace("\n", " ").Replace("\r", "");
}
