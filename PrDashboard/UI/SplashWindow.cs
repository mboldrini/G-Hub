using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using PrDashboard.GitHub;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PrDashboard.UI;

public class SplashWindow : Form
{
    private readonly WebView2 _wv = new();
    private readonly Func<Task> _startup;
    private readonly CancellationTokenSource _cts;
    private readonly NotifyIcon _notify;
    private int _port = 5555;
    private bool _toTray = true;
    private bool _startupFired;

    [DllImport("user32.dll")] static extern bool ReleaseCapture();
    [DllImport("user32.dll")] static extern nint SendMessage(nint hWnd, int msg, int wParam, int lParam);
    [DllImport("dwmapi.dll")] static extern int DwmSetWindowAttribute(nint hwnd, int attr, ref int attrValue, int attrSize);

    public SplashWindow(Func<Task> startup, CancellationTokenSource cts)
    {
        _startup = startup;
        _cts = cts;

        FormBorderStyle = FormBorderStyle.None;
        Size = new Size(524, 580);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Color.FromArgb(22, 27, 34);
        Text = "PrDashboard";

        try
        {
            var ico = Path.Combine(AppContext.BaseDirectory, "github.ico");
            if (File.Exists(ico)) Icon = new Icon(ico);
        }
        catch { }

        _wv.Dock = DockStyle.Fill;
        Controls.Add(_wv);

        _notify = new NotifyIcon
        {
            Icon = Icon ?? SystemIcons.Application,
            Text = "PrDashboard — GitHub PR Monitor",
            Visible = false,
        };
        _notify.DoubleClick += (_, _) => RestoreFromTray();

        var trayMenu = new ContextMenuStrip();
        trayMenu.Items.Add("Open", null, (_, _) => RestoreFromTray());
        trayMenu.Items.Add(new ToolStripSeparator());
        trayMenu.Items.Add("Stop & Close", null, (_, _) => { _cts.Cancel(); Close(); });
        _notify.ContextMenuStrip = trayMenu;
    }

    protected override CreateParams CreateParams
    {
        get { var cp = base.CreateParams; cp.ClassStyle |= 0x20000; return cp; }
    }

    protected override async void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        try { int p = 2; DwmSetWindowAttribute(Handle, 33, ref p, sizeof(int)); } catch { }

        try
        {
            await _wv.EnsureCoreWebView2Async();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "O WebView2 Runtime não foi encontrado.\n" +
                "Instale o Microsoft Edge WebView2 Runtime em:\n" +
                "https://developer.microsoft.com/en-us/microsoft-edge/webview2/\n\n" +
                ex.Message,
                "PrDashboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Exit();
            return;
        }

        _wv.CoreWebView2.WebMessageReceived += OnMessage;
        _wv.CoreWebView2.NavigateToString(SplashHtml.Get());

        // jsReady é enviado pelo <head> antes do Tailwind bloquear — startup começa cedo.
        // NavigationCompleted dispara depois (após Tailwind carregar). FireStartup é idempotente.
        _wv.CoreWebView2.NavigationCompleted += (_, _) => FireStartup();
    }

    private void FireStartup()
    {
        if (_startupFired) return;
        _startupFired = true;
        _ = _startup();
    }

    private void OnMessage(object? s, CoreWebView2WebMessageReceivedEventArgs e)
    {
        var msg = e.TryGetWebMessageAsString() ?? "";
        if (msg.StartsWith("tray:")) { _toTray = msg[5..] == "1"; return; }

        switch (msg)
        {
            case "jsReady":
                FireStartup(); // belt-and-suspenders: já foi disparado no NavigationCompleted
                break;
            case "startDrag":
                ReleaseCapture();
                SendMessage(Handle, 0x0112, 0xF012, 0);
                break;
            case "minimize":
                if (_toTray) HideToTray();
                else WindowState = FormWindowState.Minimized;
                break;
            case "close":
                _cts.Cancel();
                Close();
                break;
            case "stop":
                _cts.Cancel();
                ExecScript("typeof setStopped==='function'&&setStopped()");
                break;
            case "launch":
                OpenUrl($"http://localhost:{_port}");
                break;
        }
    }

    private void HideToTray()
    {
        _notify.Visible = true;
        Hide();
    }

    private void RestoreFromTray()
    {
        Show();
        WindowState = FormWindowState.Normal;
        Activate();
        _notify.Visible = false;
    }

    private static void OpenUrl(string url)
    {
        try { Process.Start(new ProcessStartInfo(url) { UseShellExecute = true }); }
        catch { }
    }

    // ── API chamada pelo Program.cs ──────────────────────────────────────────

    public void UpdateStatus(string text, int percent)
        => ExecScript($"typeof updateProgress==='function'&&updateProgress({percent},'{EscJs(text)}')");

    public void SetReady(string login, int port)
    {
        _port = port;
        ExecScript($"typeof updatePort==='function'&&updatePort({port})");
        ExecScript($"typeof setReady==='function'&&setReady('{EscJs(login)}')");
    }

    public void ShowError(string message)
        => ExecScript($"typeof setError==='function'&&setError('{EscJs(message)}')");

    public void ShowError(PreCheckException ex)
    {
        if (ex.FixCommand != null)
            ExecScript($"typeof setError==='function'&&setError('{EscJs(ex.Message)}','{EscJs(ex.FixCommand)}')");
        else
            ShowError(ex.Message);
    }

    // ── Internals ────────────────────────────────────────────────────────────

    private void ExecScript(string js)
    {
        if (!IsHandleCreated || IsDisposed) return;
        try { Invoke(() => _wv.CoreWebView2?.ExecuteScriptAsync(js)); }
        catch { }
    }

    private static string EscJs(string s)
        => s.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\n", "\\n").Replace("\r", "");

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _notify.Dispose();
        base.OnFormClosed(e);
    }
}
