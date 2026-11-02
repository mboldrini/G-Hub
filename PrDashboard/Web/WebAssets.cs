namespace PrDashboard.Web;

public static class WebAssets
{
    private static string Load(string name)
    {
        var asm = typeof(WebAssets).Assembly;
        using var stream = asm.GetManifestResourceStream($"PrDashboard.Web.{name}")!;
        using var reader = new System.IO.StreamReader(stream);
        return reader.ReadToEnd();
    }

    public static string Html  => Load("index.html");
    public static string Css   => Load("app.css");
    public static string Js    => Load("app.js");
    public static string Icons => Load("icons.svg");
}
