using PrDashboard.Logic;
using PrDashboard.Models;
using Spectre.Console;
using Spectre.Console.Rendering;
using System.Globalization;
using SpectreColor = Spectre.Console.Color;
using SpectrePanel = Spectre.Console.Panel;

namespace PrDashboard.UI;

public class PrRenderer(bool useEmoji, bool useLinks)
{
    // 2-char ASCII fallbacks — no square brackets to avoid Spectre markup issues
    private string E(string emoji, string ascii) => useEmoji ? emoji : ascii;

    private string DotRed    => E("🔴", "!!");
    private string DotYellow => E("🟡", "~~");
    private string DotGreen  => E("🟢", "OK");
    private string DotGrey   => E("◌",  "..");
    private string IconOk    => E("✅", "ok");
    private string IconFail  => E("❌", "xx");
    private string IconWait  => E("⏳", "..");
    private string IconNew   => E("🆕", "NW");
    private string IconMsg   => E("💬", "cc");
    private string IconWarn  => E("⚠️", "/\\");
    private string IconArrow => E("➜",  "->");
    private string IconEye   => E("👀", ">>");
    private string IconCheck => E("✔",  "v.");
    private string DotStat   => E("●",  "* ");
    private string DotAction => E("▲",  "^^");
    private string DotDraft  => E("◌",  "..");

    // ── Status classification ─────────────────────────────────────────────────

    private string StatusDot(PullRequest pr)
    {
        if (pr.IsDraft) return DotGrey;
        if (pr.Status?.NeedsMyAction == true) return DotRed;
        if (IsReadyToMerge(pr)) return DotGreen;
        return DotYellow;
    }

    private string StatusDotColor(PullRequest pr)
    {
        if (pr.IsDraft) return "grey";
        if (pr.Status?.NeedsMyAction == true) return "red";
        if (IsReadyToMerge(pr)) return "green";
        return "yellow";
    }

    public static bool IsReadyToMerge(PullRequest pr) =>
        pr.Status?.Review.State == ReviewState.Approved
        && pr.Status.Ci.State is CiState.Passing or CiState.None
        && pr.Status.Merge.State is MergeState.Clean or MergeState.Unknown;

    private static bool IsVerbose(PullRequest pr) => pr.Status?.NeedsMyAction == true;

    // ─── tree indent based on PR number length ───────────────────────────────
    // Line 1: "  {dot} #{num}  {title}" — dot=2cols, "#"=1, num=N digits, "  "=2
    //   total before title = 2+2+1+1+N+2 = 8+N cols
    private static string TreePad(int num) => new string(' ', 8 + num.ToString().Length);

    // ── Header panel ─────────────────────────────────────────────────────────

    public IRenderable BuildHeaderPanel(
        int total, int needsAction, int readyToMerge, int drafts,
        DateTimeOffset lastUpdate, int nextRefreshSec)
    {
        var updated = lastUpdate == DateTimeOffset.MinValue
            ? "—"
            : lastUpdate.ToLocalTime().ToString("HH:mm");
        var nextStr = nextRefreshSec > 0
            ? $"próx {nextRefreshSec / 60}:{nextRefreshSec % 60:D2}"
            : "atualizando...";

        const int barWidth = 34;
        var percent = total > 0 ? needsAction * 100 / total : 0;
        var filled  = total > 0 ? Math.Min(barWidth, needsAction * barWidth / total) : 0;
        var barFull = new string('█', filled);
        var barEmpty = new string('░', barWidth - filled);

        var content = new Rows([
            new Markup(
                $"[bold]{Markup.Escape(DotStat)}MEUS PRs[/]  " +
                $"[grey]atualizado {updated} · {Markup.Escape(nextStr)}[/]"),
            new Text(""),
            new Markup(
                $"[red]{Markup.Escape(DotAction)}[/] [bold]{needsAction}[/] precisam de ação   " +
                $"[green]{Markup.Escape(IconCheck)}[/] [bold]{readyToMerge}[/] prontos p/ merge   " +
                $"[grey]{Markup.Escape(DotDraft)} {drafts} draft   {Markup.Escape(DotStat)}{total} abertos[/]"),
            new Markup(
                $"[green]{Markup.Escape(barFull)}[/][grey]{Markup.Escape(barEmpty)}[/]  [grey]{percent}% precisa de ação[/]"),
        ]);

        return new SpectrePanel(content)
            .Border(BoxBorder.Rounded)
            .BorderStyle(new Style(SpectreColor.Grey));
    }

    // ── Repo section ──────────────────────────────────────────────────────────

    public IRenderable BuildRepoSection(
        string repo, List<PullRequest> prs,
        int selectedIndex, int navOffset, int width, DateTimeOffset now)
    {
        var needsAction = prs.Count(p => p.Status?.NeedsMyAction == true);
        var countLabel  = $"{prs.Count} PR{(prs.Count != 1 ? "s" : "")}";
        if (needsAction > 0)
            countLabel += $" · [red]{needsAction} precisam de ação[/]";

        var prRows = new List<IRenderable>();
        for (int i = 0; i < prs.Count; i++)
        {
            if (i > 0) prRows.Add(new Text(""));
            var pr = prs[i];
            prRows.AddRange(BuildPrEntry(pr, navOffset + i == selectedIndex, width, now));
        }
        prRows.Add(new Text(""));

        return new SpectrePanel(new Rows(prRows))
            .Header($"[bold]{Markup.Escape(repo)}[/]  [grey]{countLabel}[/]")
            .Border(BoxBorder.Heavy)
            .BorderStyle(new Style(SpectreColor.Grey));
    }

    // ── PR entry (verbose or compact) ────────────────────────────────────────

    public IEnumerable<IRenderable> BuildPrEntry(
        PullRequest pr, bool isSelected, int width, DateTimeOffset now)
    {
        var dot      = StatusDot(pr);
        var dotColor = StatusDotColor(pr);
        var sel      = isSelected ? "[bold yellow]> [/]" : "  ";
        var pad      = TreePad(pr.Number);

        var maxTitle = Math.Max(20, width - 16 - pr.Number.ToString().Length);
        var title    = pr.Title.Length > maxTitle ? pr.Title[..(maxTitle - 1)] + "…" : pr.Title;

        var draftBadge = pr.IsDraft ? "  [grey bold]DRAFT[/]" : "";

        string numRef = useLinks && !string.IsNullOrEmpty(pr.Url)
            ? $"[link={pr.Url}][bold]#{pr.Number}[/][/]"
            : $"[bold]#{pr.Number}[/]";

        yield return new Markup(
            $"{sel}[{dotColor}]{Markup.Escape(dot)}[/] {numRef}  {Markup.Escape(title)}{draftBadge}");

        if (IsVerbose(pr))
            foreach (var line in BuildVerboseLines(pr, pad, now))
                yield return line;
        else
            yield return BuildCompactLine(pr, pad, now);
    }

    // ── Verbose tree ─────────────────────────────────────────────────────────

    private IEnumerable<IRenderable> BuildVerboseLines(PullRequest pr, string pad, DateTimeOffset now)
    {
        if (pr.Status is null) yield break;

        var items = new List<(string Label, string Markup)>();

        // Review
        var rev = pr.Status.Review;
        if (rev.State != ReviewState.None)
        {
            var rc = rev.State switch
            {
                ReviewState.Approved =>
                    $"[green]{Markup.Escape(IconOk)} aprovado · {Markup.Escape(rev.Reviewer ?? "")}[/]" +
                    (rev.Since.HasValue ? $"[grey], {Markup.Escape(TimeFormatter.Duration(rev.Since.Value))}[/]" : ""),
                ReviewState.ChangesRequested =>
                    $"[red]{Markup.Escape(DotRed)} mudanças pedidas · {Markup.Escape(rev.Reviewer ?? "")}[/]" +
                    (rev.Since.HasValue ? $"[grey], {Markup.Escape(TimeFormatter.Duration(rev.Since.Value))}[/]" : ""),
                ReviewState.Pending when rev.PendingReviewers.Any() =>
                    $"[yellow]{Markup.Escape(IconWait)} aguardando {Markup.Escape(string.Join(", ", rev.PendingReviewers.Take(3)))}[/]",
                _ =>
                    $"[yellow]{Markup.Escape(IconWait)} aguardando review[/]",
            };
            items.Add(("review", rc));
        }

        // CI
        if (pr.Status.Ci.State != CiState.None)
        {
            var cc = pr.Status.Ci.State switch
            {
                CiState.Failing => $"[red]{Markup.Escape(IconFail)} {Markup.Escape(pr.Status.Ci.FailingCheck ?? "CI")}[/]",
                CiState.Pending => $"[yellow]{Markup.Escape(IconWait)} CI rodando[/]",
                CiState.Passing => $"[green]{Markup.Escape(IconOk)} CI ok[/]",
                _ => ""
            };
            if (!string.IsNullOrEmpty(cc)) items.Add(("CI", cc));
        }

        // Conversation
        var com = pr.Status.Comment;
        if (com.State == CommentState.New || com.OpenThreads > 0)
        {
            var cc = com.State == CommentState.New
                ? $"[yellow]{Markup.Escape(IconNew)} {Markup.Escape(com.LastAuthor ?? "")} comentou " +
                  $"{Markup.Escape(TimeFormatter.Ago(now - com.Since!.Value, now))}" +
                  (com.OpenThreads > 0 ? $" · {com.OpenThreads} threads abertas[/]" : "[/]")
                : $"[yellow]{Markup.Escape(IconMsg)} {com.OpenThreads} threads abertas[/]";
            items.Add(("conversa", cc));
        }

        // Merge
        var mc = pr.Status.Merge.State switch
        {
            MergeState.Conflict   => $"[red]{Markup.Escape(IconWarn)} conflito com a base[/]",
            MergeState.BehindBase => $"[yellow]{Markup.Escape(IconWarn)} atrás da base[/]",
            MergeState.Clean      => $"[green]{Markup.Escape(IconOk)} limpo[/][grey] · aberto {Markup.Escape(TimeFormatter.Ago(pr.CreatedAt, now))}[/]",
            _                     => $"[grey]aberto {Markup.Escape(TimeFormatter.Ago(pr.CreatedAt, now))}[/]",
        };
        items.Add(("merge", mc));

        for (int i = 0; i < items.Count; i++)
        {
            var (label, markup) = items[i];
            var branch  = i == items.Count - 1 ? "└" : "├";
            var padded  = label.PadRight(10);
            yield return new Markup($"[grey]{Markup.Escape(pad)}{branch} {Markup.Escape(padded)}[/]{markup}");
        }
    }

    // ── Compact single line ───────────────────────────────────────────────────

    private IRenderable BuildCompactLine(PullRequest pr, string pad, DateTimeOffset now)
    {
        if (pr.Status is null)
            return new Markup($"[grey]{Markup.Escape(pad)}└ aberto {Markup.Escape(TimeFormatter.Ago(pr.CreatedAt, now))}[/]");

        var parts = new List<string>();

        var rev = pr.Status.Review;
        var rs = rev.State switch
        {
            ReviewState.Approved when rev.Reviewer != null =>
                $"[green]{Markup.Escape(IconOk)} aprovado {Markup.Escape(rev.Reviewer)}[/]" +
                (rev.Since.HasValue ? $"[grey], {Markup.Escape(TimeFormatter.Duration(rev.Since.Value))}[/]" : ""),
            ReviewState.ChangesRequested when rev.Reviewer != null =>
                $"[red]{Markup.Escape(DotRed)} {Markup.Escape(rev.Reviewer)}[/]",
            ReviewState.Pending when rev.PendingReviewers.Any() =>
                $"[yellow]{Markup.Escape(IconWait)} aguardando {Markup.Escape(string.Join(", ", rev.PendingReviewers.Take(2)))}[/]",
            ReviewState.Pending =>
                $"[yellow]{Markup.Escape(IconWait)} aguardando review[/]",
            _ => ""
        };
        if (!string.IsNullOrEmpty(rs)) parts.Add(rs);

        var cs = pr.Status.Ci.State switch
        {
            CiState.Passing => $"[green]{Markup.Escape(IconOk)} CI[/]",
            CiState.Failing => $"[red]{Markup.Escape(IconFail)} CI[/]",
            CiState.Pending => $"[yellow]{Markup.Escape(IconWait)} CI rodando[/]",
            _ => ""
        };
        if (!string.IsNullOrEmpty(cs)) parts.Add(cs);

        var ms = pr.Status.Merge.State switch
        {
            MergeState.Conflict   => $"[red]{Markup.Escape(IconWarn)} conflito[/]",
            MergeState.BehindBase => $"[yellow]{Markup.Escape(IconWarn)} atrás da base[/]",
            _ => ""
        };
        if (!string.IsNullOrEmpty(ms)) parts.Add(ms);

        parts.Add($"[grey]{Markup.Escape(TimeFormatter.Ago(pr.CreatedAt, now))}[/]");

        if (IsReadyToMerge(pr))
            parts.Add($"[green bold]{Markup.Escape(IconArrow)} pronto p/ merge[/]");

        var sep = "[grey] · [/]";
        return new Markup($"[grey]{Markup.Escape(pad)}└[/] {string.Join(sep, parts)}");
    }

    // ── Reviews section ───────────────────────────────────────────────────────

    public IRenderable BuildReviewSection(
        List<PullRequest> reviewPrs, int selectedIndex, int navOffset, DateTimeOffset now)
    {
        if (!reviewPrs.Any())
            return new SpectrePanel(new Markup("[grey]  Nenhum review pendente.[/]"))
                .Header("[bold]REVIEWS PEDIDOS A MIM[/]")
                .Border(BoxBorder.Heavy)
                .BorderStyle(new Style(SpectreColor.Grey));

        var rows = new List<IRenderable>();
        for (int i = 0; i < reviewPrs.Count; i++)
        {
            var pr  = reviewPrs[i];
            var sel = (navOffset + i) == selectedIndex ? "[bold yellow]> [/]" : "  ";

            string numRef = useLinks && !string.IsNullOrEmpty(pr.Url)
                ? $"[link={pr.Url}][bold]#{pr.Number}[/][/]"
                : $"[bold]#{pr.Number}[/]";

            rows.Add(new Markup(
                $"{sel}[grey]{Markup.Escape(IconEye)} {Markup.Escape(pr.Repo)}[/]  " +
                $"{numRef}  {Markup.Escape(pr.Title)}"));

            // Detail line: author · age · CI · last comment
            var sb = new System.Text.StringBuilder();
            sb.Append("[grey]             ");
            if (!string.IsNullOrEmpty(pr.Author))
                sb.Append($"por {Markup.Escape(pr.Author)} · ");
            sb.Append($"aberto {Markup.Escape(TimeFormatter.Ago(pr.CreatedAt, now))}[/]");

            if (pr.Status?.Ci.State is CiState ciState && ciState != CiState.None)
            {
                var ciText = ciState switch
                {
                    CiState.Passing => $"[green]{Markup.Escape(IconOk)} CI ok[/]",
                    CiState.Failing => $"[red]{Markup.Escape(IconFail)} CI falhou[/]",
                    CiState.Pending => $"[yellow]{Markup.Escape(IconWait)} CI rodando[/]",
                    _ => ""
                };
                if (!string.IsNullOrEmpty(ciText))
                    sb.Append($"[grey] · [/]{ciText}");
            }

            var lastComment = pr.RecentComments.OrderByDescending(c => c.CreatedAt).FirstOrDefault();
            if (lastComment != null)
                sb.Append($"[grey] · msg {Markup.Escape(lastComment.Author)} {Markup.Escape(TimeFormatter.Ago(lastComment.CreatedAt, now))}[/]");

            rows.Add(new Markup(sb.ToString()));
        }

        return new SpectrePanel(new Rows(rows))
            .Header($"[bold]REVIEWS PEDIDOS A MIM[/]  [grey]{reviewPrs.Count}[/]")
            .Border(BoxBorder.Heavy)
            .BorderStyle(new Style(SpectreColor.Grey));
    }

    // ── Concluded section ─────────────────────────────────────────────────────

    public IRenderable? BuildConcludedSection(List<MergedPr> merged)
    {
        var thisWeek = merged.Where(m => m.MergedAt >= DateTimeOffset.UtcNow.AddDays(-7)).ToList();
        if (!thisWeek.Any()) return null;

        var ptBr = new CultureInfo("pt-BR");
        var rows = thisWeek.OrderByDescending(m => m.MergedAt).Take(10).Select(m =>
        {
            var dayStr   = m.MergedAt.ToLocalTime().ToString("ddd dd/MM", ptBr);
            var duration = TimeFormatter.Duration(m.MergedAt - m.CreatedAt);
            string numRef = useLinks && !string.IsNullOrEmpty(m.Url)
                ? $"[link={m.Url}]#{m.Number}[/]"
                : $"#{m.Number}";
            return (IRenderable)new Markup(
                $"[green]{Markup.Escape(IconCheck)}[/] [grey]{Markup.Escape(m.Repo)}[/]  " +
                $"{numRef}  {Markup.Escape(m.Title)}  " +
                $"[grey]{Markup.Escape(dayStr)} · levou {Markup.Escape(duration)}[/]");
        }).ToList();

        return new SpectrePanel(new Rows(rows))
            .Header($"[bold]CONCLUÍDOS ESTA SEMANA[/]  [grey]{thisWeek.Count} mergeados[/]")
            .Border(BoxBorder.Heavy)
            .BorderStyle(new Style(SpectreColor.Grey));
    }

    // ── Footer / status ───────────────────────────────────────────────────────

    public IRenderable RenderFooter() =>
        new Markup("  [grey]↑/↓[/] seleciona   [grey]Enter[/] abre no navegador   [grey]r[/] atualiza   [grey]q[/] sai");

    public IRenderable RenderError(string message) =>
        new Markup($"[red bold]Erro:[/] {Markup.Escape(message)}");

    public IRenderable RenderUpdating() =>
        new Markup($"[grey]{E("⟳", "~")} atualizando...[/]");
}
