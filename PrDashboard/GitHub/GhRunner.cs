using System.Diagnostics;
using System.Text;

namespace PrDashboard.GitHub;

public static class GhRunner
{
    public static async Task<(string Output, string Error, int ExitCode)> RunAsync(
        string args, string? hostname = null, CancellationToken ct = default)
    {
        var finalArgs = hostname != null && hostname != "github.com"
            ? $"--hostname {hostname} {args}"
            : args;

        var psi = new ProcessStartInfo("gh", finalArgs)
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8,
        };

        using var proc = Process.Start(psi) ?? throw new InvalidOperationException("Failed to start gh");

        var outTask = proc.StandardOutput.ReadToEndAsync(ct);
        var errTask = proc.StandardError.ReadToEndAsync(ct);
        await proc.WaitForExitAsync(ct);

        return (await outTask, await errTask, proc.ExitCode);
    }

    public static async Task<string> RunJsonAsync(string args, string? hostname = null, CancellationToken ct = default)
    {
        var (output, error, code) = await RunAsync(args, hostname, ct);
        if (code != 0)
            throw new GhException(code, error.Trim());
        return output;
    }
}

public class GhException : Exception
{
    public int ExitCode { get; }

    public GhException(int exitCode, string errorMessage) : base(errorMessage)
        => ExitCode = exitCode;

    public bool IsRateLimit => Message.Contains("rate limit", StringComparison.OrdinalIgnoreCase)
                             || Message.Contains("API rate limit", StringComparison.OrdinalIgnoreCase);
    public bool IsSso => Message.Contains("SSO", StringComparison.OrdinalIgnoreCase)
                       || Message.Contains("SAML", StringComparison.OrdinalIgnoreCase);
    public bool IsNetwork => Message.Contains("connect", StringComparison.OrdinalIgnoreCase)
                           || Message.Contains("timeout", StringComparison.OrdinalIgnoreCase)
                           || Message.Contains("network", StringComparison.OrdinalIgnoreCase);
}
