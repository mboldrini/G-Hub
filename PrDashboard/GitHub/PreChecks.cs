namespace PrDashboard.GitHub;

public static class PreChecks
{
    public static async Task<string> RunAllAsync(string hostname, CancellationToken ct = default)
    {
        EnsureGhInPath();
        await EnsureAuthenticatedAsync(hostname, ct);
        return await GetLoginAsync(hostname, ct);
    }

    private static void EnsureGhInPath()
    {
        var paths = (Environment.GetEnvironmentVariable("PATH") ?? "").Split(Path.PathSeparator);
        bool found = paths.Any(dir =>
            File.Exists(Path.Combine(dir, "gh.exe")) || File.Exists(Path.Combine(dir, "gh")));

        if (!found)
            throw new PreCheckException(
                "gh não encontrado no PATH.\nInstale o GitHub CLI: https://cli.github.com");
    }

    private static async Task EnsureAuthenticatedAsync(string hostname, CancellationToken ct)
    {
        var (_, error, code) = await GhRunner.RunAsync("auth status", hostname, ct);
        if (code != 0)
            throw new PreCheckException(
                $"gh auth status falhou. Execute: gh auth login\n\nDetalhe: {error.Trim()}");

        if (error.Contains("GH_TOKEN") || error.Contains("GITHUB_TOKEN"))
            throw new PreCheckException(
                "Variável de ambiente GH_TOKEN ou GITHUB_TOKEN detectada.\n" +
                "Ela sobrescreve o login do gh CLI. Remova-a ou ajuste conforme necessário.");
    }

    private static async Task<string> GetLoginAsync(string hostname, CancellationToken ct)
    {
        var hostnameFlag = hostname != "github.com" ? $"--hostname {hostname} " : "";
        var json = await GhRunner.RunJsonAsync($"{hostnameFlag}api user --jq .login", null, ct);
        var login = json.Trim().Trim('"');
        if (string.IsNullOrWhiteSpace(login))
            throw new PreCheckException("Não foi possível obter o login do GitHub via 'gh api user'.");
        return login;
    }
}

public class PreCheckException(string message) : Exception(message);
