namespace PrDashboard.IO;

public static class WeeklyFolder
{
    private static readonly string[] MonthsPT =
        ["Jan", "Fev", "Mar", "Abr", "Mai", "Jun", "Jul", "Ago", "Set", "Out", "Nov", "Dez"];

    public static string GetPath(string baseDir, DateTime date)
    {
        int daysToMonday = date.DayOfWeek == DayOfWeek.Sunday ? -6 : -(int)date.DayOfWeek + 1;
        var monday = date.AddDays(daysToMonday).Date;
        var friday = monday.AddDays(4);
        var month = MonthsPT[monday.Month - 1];
        var weekFolder = $"{monday.Day:D2}_{friday.Day:D2}";
        return Path.Combine(baseDir, month, weekFolder);
    }
}
