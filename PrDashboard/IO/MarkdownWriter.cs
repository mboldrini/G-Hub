using PrDashboard.Logic;
using PrDashboard.Models;

namespace PrDashboard.IO;

public static class MarkdownWriter
{
    public static string BuildMeusPrs(
        List<PullRequest> myPrs,
        List<MergedPr> merged,
        List<ClosedPr> closed,
        int historyDays,
        DateTimeOffset now)
    {
        var needsAction = myPrs.Count(p => p.Status?.NeedsMyAction == true);
        var sb = new System.Text.StringBuilder();

        sb.AppendLine("---");
        sb.AppendLine($"atualizado: {now.ToLocalTime():yyyy-MM-dd HH:mm:ss}");
        sb.AppendLine($"abertos: {myPrs.Count}");
        sb.AppendLine($"precisam_acao: {needsAction}");
        sb.AppendLine("---");
        sb.AppendLine();

        sb.AppendLine("## Em aberto");
        sb.AppendLine();
        sb.AppendLine("| Repo | # | Título | Último commit | Review | CI | Comentário | Merge | Idade |");
        sb.AppendLine("|------|---|--------|--------------|--------|-----|------------|-------|-------|");

        foreach (var pr in myPrs.OrderBy(p => p.Repo).ThenBy(p => p.Number))
        {
            var title = EscapeMd(pr.Title);
            var link = $"[#{pr.Number}]({pr.Url})";
            var commit = pr.LastCommit != null ? TimeFormatter.Ago(pr.LastCommit.CommittedDate, now) : "—";
            var review = FormatReview(pr.Status?.Review);
            var ci = FormatCi(pr.Status?.Ci);
            var comment = FormatComment(pr.Status?.Comment);
            var merge = FormatMerge(pr.Status?.Merge);
            var age = TimeFormatter.Ago(pr.CreatedAt, now);
            sb.AppendLine($"| {EscapeMd(pr.Repo)} | {link} | {title} | {commit} | {review} | {ci} | {comment} | {merge} | {age} |");
        }

        sb.AppendLine();
        sb.AppendLine($"## Concluídos (últimos {historyDays} dias)");
        sb.AppendLine();

        var recentMerged = merged
            .Where(m => m.MergedAt >= now.AddDays(-historyDays))
            .OrderByDescending(m => m.MergedAt)
            .ToList();

        if (recentMerged.Any())
        {
            sb.AppendLine("| Repo | # | Título | Mergeado em | Levou |");
            sb.AppendLine("|------|---|--------|-------------|-------|");
            foreach (var m in recentMerged)
            {
                var link = $"[#{m.Number}]({m.Url})";
                var mergedAt = m.MergedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
                var duration = m.MergedAt - m.CreatedAt;
                sb.AppendLine($"| {EscapeMd(m.Repo)} | {link} | {EscapeMd(m.Title)} | {mergedAt} | {TimeFormatter.Duration(duration)} |");
            }
        }
        else
        {
            sb.AppendLine("_Nenhum._");
        }

        sb.AppendLine();
        sb.AppendLine("## Fechados sem merge");
        sb.AppendLine();

        var recentClosed = closed
            .Where(c => c.ClosedAt >= now.AddDays(-historyDays))
            .OrderByDescending(c => c.ClosedAt)
            .ToList();

        if (recentClosed.Any())
        {
            sb.AppendLine("| Repo | # | Título | Fechado em |");
            sb.AppendLine("|------|---|--------|------------|");
            foreach (var c in recentClosed)
            {
                var link = $"[#{c.Number}]({c.Url})";
                var closedAt = c.ClosedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
                sb.AppendLine($"| {EscapeMd(c.Repo)} | {link} | {EscapeMd(c.Title)} | {closedAt} |");
            }
        }
        else
        {
            sb.AppendLine("_Nenhum._");
        }

        return sb.ToString();
    }

    public static string BuildReviews(List<PullRequest> reviewPrs, DateTimeOffset now)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"# Reviews pedidos a mim — {now.ToLocalTime():yyyy-MM-dd}");
        sb.AppendLine();
        sb.AppendLine($"_Gerado em {now.ToLocalTime():HH:mm:ss}_");
        sb.AppendLine();

        if (!reviewPrs.Any())
        {
            sb.AppendLine("_Nenhum._");
            return sb.ToString();
        }

        foreach (var group in reviewPrs.GroupBy(p => p.Repo).OrderBy(g => g.Key))
        {
            sb.AppendLine($"### {group.Key}");
            foreach (var pr in group.OrderBy(p => p.Number))
            {
                var ci = pr.Status?.Ci.State switch
                {
                    CiState.Passing => "✅ CI",
                    CiState.Failing => $"❌ {pr.Status.Ci.FailingCheck ?? "CI"}",
                    CiState.Pending => "⏳ CI",
                    _ => ""
                };
                var age = TimeFormatter.Ago(pr.CreatedAt, now);
                var badges = string.Join(" · ", new[] { ci }.Where(s => s != ""));
                var draft = pr.IsDraft ? " _(rascunho)_" : "";
                sb.AppendLine($"- **[#{pr.Number}]({pr.Url})** {EscapeMd(pr.Title)}{draft}");
                sb.AppendLine($"  _{EscapeMd(pr.Author ?? "")}_ · {(badges != "" ? badges + " · " : "")}{age}");
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    public static string BuildHistorico(List<MergedPr> allMerged, List<ClosedPr> allClosed, DateTimeOffset now)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"# Histórico de PRs");
        sb.AppendLine();
        sb.AppendLine($"_Gerado em {now.ToLocalTime():yyyy-MM-dd HH:mm:ss}_");
        sb.AppendLine();

        sb.AppendLine("## Mergeados");
        sb.AppendLine();
        if (allMerged.Any())
        {
            sb.AppendLine("| Repo | # | Título | Mergeado em | Levou |");
            sb.AppendLine("|------|---|--------|-------------|-------|");
            foreach (var m in allMerged.OrderByDescending(m => m.MergedAt))
            {
                var link = $"[#{m.Number}]({m.Url})";
                var mergedAt = m.MergedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
                var duration = m.MergedAt - m.CreatedAt;
                sb.AppendLine($"| {EscapeMd(m.Repo)} | {link} | {EscapeMd(m.Title)} | {mergedAt} | {TimeFormatter.Duration(duration)} |");
            }
        }
        else
        {
            sb.AppendLine("_Nenhum._");
        }

        sb.AppendLine();
        sb.AppendLine("## Fechados sem merge");
        sb.AppendLine();
        if (allClosed.Any())
        {
            sb.AppendLine("| Repo | # | Título | Fechado em |");
            sb.AppendLine("|------|---|--------|------------|");
            foreach (var c in allClosed.OrderByDescending(c => c.ClosedAt))
            {
                var link = $"[#{c.Number}]({c.Url})";
                var closedAt = c.ClosedAt.ToLocalTime().ToString("yyyy-MM-dd HH:mm");
                sb.AppendLine($"| {EscapeMd(c.Repo)} | {link} | {EscapeMd(c.Title)} | {closedAt} |");
            }
        }
        else
        {
            sb.AppendLine("_Nenhum._");
        }

        return sb.ToString();
    }

    public static string BuildStandup(
        List<PullRequest> myPrs,
        List<MergedPr> mergedPrs,
        DateTimeOffset now)
    {
        var sb = new System.Text.StringBuilder();
        sb.AppendLine($"# Standup — {now.ToLocalTime():yyyy-MM-dd}");
        sb.AppendLine();
        sb.AppendLine($"_Gerado em {now.ToLocalTime():HH:mm:ss}_");
        sb.AppendLine();

        // My PRs
        sb.AppendLine("## Meus PRs Abertos");
        sb.AppendLine();
        if (!myPrs.Any())
        {
            sb.AppendLine("_Nenhum._");
        }
        else
        {
            foreach (var group in myPrs.GroupBy(p => p.Repo).OrderBy(g => g.Key))
            {
                sb.AppendLine($"### {group.Key}");
                foreach (var pr in group.OrderBy(p => p.Number))
                {
                    var review = pr.Status?.Review.State switch
                    {
                        ReviewState.Approved => $"✅ {pr.Status.Review.Reviewer}",
                        ReviewState.ChangesRequested => $"🔴 mudanças pedidas",
                        ReviewState.Pending => "⏳ aguardando review",
                        _ => ""
                    };
                    var ci = pr.Status?.Ci.State switch
                    {
                        CiState.Passing => "✅ CI",
                        CiState.Failing => $"❌ {pr.Status.Ci.FailingCheck ?? "CI"}",
                        CiState.Pending => "⏳ CI",
                        _ => ""
                    };
                    var comment = pr.Status?.Comment.State switch
                    {
                        CommentState.New => $"🆕 {pr.Status.Comment.LastAuthor}",
                        CommentState.HasComments when pr.Status.Comment.OpenThreads > 0
                            => $"💬 {pr.Status.Comment.OpenThreads} threads",
                        _ => ""
                    };
                    var merge = pr.Status?.Merge.State switch
                    {
                        MergeState.Conflict => "⚡ conflito",
                        MergeState.BehindBase => "⬇️ desatualizado",
                        _ => ""
                    };
                    var draft = pr.IsDraft ? " _(rascunho)_" : "";
                    var age = TimeFormatter.Ago(pr.CreatedAt, now);
                    var badges = string.Join(" · ", new[] { review, ci, comment, merge }.Where(s => s != ""));
                    var branch = pr.HeadRef != "" ? $" `{pr.HeadRef}`" : "";
                    sb.AppendLine($"- **[#{pr.Number}]({pr.Url})** {EscapeMd(pr.Title)}{draft}{branch}");
                    sb.AppendLine($"  {(badges != "" ? badges + " · " : "")}{age}");

                    if (!string.IsNullOrWhiteSpace(pr.Body))
                    {
                        var desc = pr.Body.Replace("\r\n", " ").Replace("\n", " ").Trim();
                        sb.AppendLine($"  > {desc}");
                    }

                    foreach (var c in pr.RecentComments.Where(c => c.Author != pr.Author && !string.IsNullOrEmpty(c.Body)))
                    {
                        var body = c.Body.Replace("\r\n", " ").Replace("\n", " ").Trim();
                        sb.AppendLine($"  - 💬 **{EscapeMd(c.Author)}** ({TimeFormatter.Ago(c.CreatedAt, now)}): {body}");
                    }
                }
                sb.AppendLine();
            }
        }

        // Merged
        sb.AppendLine("## Mergeados Recentemente");
        sb.AppendLine();
        if (!mergedPrs.Any())
        {
            sb.AppendLine("_Nenhum._");
        }
        else
        {
            foreach (var m in mergedPrs.OrderByDescending(m => m.MergedAt))
            {
                var duration = TimeFormatter.Duration(m.MergedAt - m.CreatedAt);
                sb.AppendLine($"- **[#{m.Number}]({m.Url})** {EscapeMd(m.Title)}");
                sb.AppendLine($"  {EscapeMd(m.Repo)} · {TimeFormatter.Ago(m.MergedAt, now)} · levou {duration}");
            }
        }

        return sb.ToString();
    }

    private static string FormatReview(ReviewStatus? r)
    {
        if (r is null) return "—";
        return r.State switch
        {
            ReviewState.Approved => $"✅ {r.Reviewer}",
            ReviewState.ChangesRequested => $"🔴 {r.Reviewer}",
            ReviewState.Pending => "⏳ aguardando",
            _ => "—",
        };
    }

    private static string FormatCi(CiStatus? ci)
    {
        if (ci is null) return "—";
        return ci.State switch
        {
            CiState.Passing => "✅",
            CiState.Failing => $"❌ {ci.FailingCheck}",
            CiState.Pending => "⏳",
            _ => "—",
        };
    }

    private static string FormatComment(CommentStatus? c)
    {
        if (c is null) return "—";
        return c.State switch
        {
            CommentState.New => $"🆕 {c.LastAuthor}",
            CommentState.HasComments when c.OpenThreads > 0 => $"💬 {c.OpenThreads} threads",
            _ => "—",
        };
    }

    private static string FormatMerge(MergeStatus? m)
    {
        if (m is null) return "—";
        return m.State switch
        {
            MergeState.Conflict => "⚠️ conflito",
            MergeState.BehindBase => "⬇️ atrás",
            _ => "✅",
        };
    }

    private static string EscapeMd(string s) =>
        s.Replace("|", "\\|").Replace("\n", " ").Replace("\r", "");
}
