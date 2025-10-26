using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Platform;
using pluginMarkdown.Data;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CodeEditor2MarkdownPlugin;

public partial class PreviewControl : UserControl
{
    public PreviewControl()
    {
        InitializeComponent();
    }

    volatile bool _loaded = false;

    public async Task PreViewSetup()
    {
        string htmlText;
        var uri = new Uri("avares://CodeEditor2MarkdownPlugin/Assets/html/preview.html");

        using (var stream = AssetLoader.Open(uri))
        using (var reader = new StreamReader(stream))
        {
            htmlText = reader.ReadToEnd();
        }
        var tempPath = Path.Combine(Path.GetTempPath(), "temp.html");
        File.WriteAllText(tempPath, htmlText);

        await browser.LoadLocalHtml(tempPath);
        await browser.WaitForLoadAsync();
        await Task.Delay(100);
        _loaded = true;
    }

    public async Task LoadFile(MarkdownFile mdFile)
    {
        if (!_loaded) await PreViewSetup();
        await browser.WaitForLoadAsync();

        if (mdFile.CodeDocument == null) return;
        string markdown = mdFile.CodeDocument.CreateString();
        string escaped = markdown
            .Replace("\\", "\\\\")
            .Replace("'", "\\'")
            .Replace("\r", "")
            .Replace("\n", "\\n");
        await browser.ExecuteScriptAsync($"loadMarkdownContent('{escaped}');");
    }

}