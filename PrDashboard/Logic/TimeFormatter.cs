namespace PrDashboard.Logic;

public static class TimeFormatter
{
    public static string Ago(DateTimeOffset utcDate, DateTimeOffset? now = null, string lang = "pt-BR")
    {
        var diff = (now ?? DateTimeOffset.UtcNow).ToLocalTime() - utcDate.ToLocalTime();
        bool en = lang == "en-US";

        if (diff.TotalSeconds < 60)  return en ? "just now"                          : "agora mesmo";
        if (diff.TotalMinutes < 60)  return en ? $"{(int)diff.TotalMinutes}min ago"  : $"há {(int)diff.TotalMinutes}min";
        if (diff.TotalHours < 24)    return en ? $"{(int)diff.TotalHours}h ago"      : $"há {(int)diff.TotalHours}h";
        if (diff.TotalDays < 7)      return en ? $"{(int)diff.TotalDays}d ago"       : $"há {(int)diff.TotalDays}d";
        if (diff.TotalDays < 30)     return en ? $"{(int)(diff.TotalDays/7)}w ago"   : $"há {(int)(diff.TotalDays/7)}sem";
        if (diff.TotalDays < 365)    return en ? $"{(int)(diff.TotalDays/30)}mo ago" : $"há {(int)(diff.TotalDays/30)}mes";
        return en ? $"{(int)(diff.TotalDays/365)}yr ago" : $"há {(int)(diff.TotalDays/365)}ano";
    }

    public static string Duration(TimeSpan span, string lang = "pt-BR")
    {
        bool en = lang == "en-US";
        if (span.TotalSeconds < 60) return $"{(int)span.TotalSeconds}s";
        if (span.TotalMinutes < 60) return $"{(int)span.TotalMinutes}min";
        if (span.TotalHours < 24)   return $"{(int)span.TotalHours}h";
        if (span.TotalDays < 7)     return $"{(int)span.TotalDays}d";
        if (span.TotalDays < 30)    return en ? $"{(int)(span.TotalDays/7)}w"  : $"{(int)(span.TotalDays/7)}sem";
        return en ? $"{(int)(span.TotalDays/30)}mo" : $"{(int)(span.TotalDays/30)}mes";
    }

    public static string Suffix(string lang) => lang == "en-US" ? "ago" : "atrás";
}
