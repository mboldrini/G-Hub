using System.Text.RegularExpressions;

namespace PrDashboard.Tests;

public class AudioTests
{
    private static readonly string RepoRoot = Path.GetFullPath(
        Path.Combine(AppContext.BaseDirectory, "..", "..", "..", ".."));

    private static readonly string SourceDir = Path.Combine(RepoRoot, "PrDashboard");
    private static readonly string PublishDir = Path.Combine(RepoRoot, "publish");

    // ── Source-level checks ───────────────────────────────────────────────────

    [Fact]
    public void AppJs_has_no_synth_audio_code()
    {
        var js = File.ReadAllText(Path.Combine(SourceDir, "Web", "app.js"));
        Assert.DoesNotContain("_synthBeep", js);
        Assert.DoesNotContain("AudioContext", js);
        Assert.DoesNotContain("_tryPlayFile", js);
        Assert.DoesNotContain("OscillatorNode", js);
    }

    [Fact]
    public void AppJs_playAudio_uses_audio_route_prefix()
    {
        var js = File.ReadAllText(Path.Combine(SourceDir, "Web", "app.js"));
        // Calls must use /audio/ prefix, never bare /done.mp3 etc.
        var bare = Regex.IsMatch(js, @"playAudio\('/(?!audio/)[^']+\.mp3'");
        Assert.False(bare, "playAudio() called with bare /file.mp3 path — must use /audio/file.mp3");
    }

    [Fact]
    public void WebServer_has_no_SynthWav()
    {
        var cs = File.ReadAllText(Path.Combine(SourceDir, "Web", "WebServer.cs"));
        Assert.DoesNotContain("SynthWav", cs);
        Assert.DoesNotContain("audioFallback", cs);
    }

    [Theory]
    [InlineData("done.mp3")]
    [InlineData("error.mp3")]
    [InlineData("message.mp3")]
    public void Audio_file_exists_in_source(string filename)
    {
        var path = Path.Combine(SourceDir, filename);
        Assert.True(File.Exists(path), $"PrDashboard/{filename} not found — place your MP3 files there before building");
    }

    [Theory]
    [InlineData("done.mp3")]
    [InlineData("error.mp3")]
    [InlineData("message.mp3")]
    public void Audio_file_in_source_is_not_empty_stub(string filename)
    {
        var path = Path.Combine(SourceDir, filename);
        if (!File.Exists(path)) return;
        var size = new FileInfo(path).Length;
        Assert.True(size > 1024, $"PrDashboard/{filename} is too small ({size} bytes) — replace with a real audio file");
    }

    // ── Publish-output checks ─────────────────────────────────────────────────

    [Theory]
    [InlineData("done.mp3")]
    [InlineData("error.mp3")]
    [InlineData("message.mp3")]
    public void Audio_file_exists_in_publish(string filename)
    {
        var path = Path.Combine(PublishDir, filename);
        Assert.True(File.Exists(path), $"publish/{filename} not found — run build.bat first");
    }

    [Theory]
    [InlineData("done.mp3")]
    [InlineData("error.mp3")]
    [InlineData("message.mp3")]
    public void Audio_file_in_publish_has_valid_format(string filename)
    {
        var path = Path.Combine(PublishDir, filename);
        if (!File.Exists(path)) return;
        var header = new byte[4];
        using var fs = File.OpenRead(path);
        fs.Read(header, 0, 4);
        bool isWav  = header[0] == 0x52 && header[1] == 0x49 && header[2] == 0x46 && header[3] == 0x46; // RIFF
        bool isId3  = header[0] == 0x49 && header[1] == 0x44 && header[2] == 0x33;                      // ID3
        bool isSync = header[0] == 0xFF && (header[1] & 0xE0) == 0xE0;                                  // MP3 sync
        Assert.True(isWav || isId3 || isSync,
            $"publish/{filename} has unknown magic bytes 0x{header[0]:X2}{header[1]:X2}{header[2]:X2}{header[3]:X2}");
    }

    [Theory]
    [InlineData("done.mp3")]
    [InlineData("error.mp3")]
    [InlineData("message.mp3")]
    public void Audio_file_in_publish_is_not_empty_stub(string filename)
    {
        var path = Path.Combine(PublishDir, filename);
        if (!File.Exists(path)) return;
        var size = new FileInfo(path).Length;
        Assert.True(size > 1024, $"publish/{filename} is too small ({size} bytes) — likely a stub or truncated file");
    }
}
