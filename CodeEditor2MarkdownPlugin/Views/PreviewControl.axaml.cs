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
        var uri = new Uri("avares://CodeEditor2MarkdownPlugin/Assets/html/preview.html");

        // アセットから HTML 文字列を取得
        string htmlText;
        using (var stream = AssetLoader.Open(uri))
        using (var reader = new StreamReader(stream))
        {
            htmlText = await reader.ReadToEndAsync();
        }

        // 文字列を渡して完了を待機
        bool success = await NavigateToStringAndWaitAsync(htmlText);
        if (success)
        {
            await Task.Delay(100);
            _loaded = true;
        }
    }

    public async Task<bool> NavigateToStringAndWaitAsync(string htmlContent)
    {
        var tcs = new TaskCompletionSource<bool>();

        void OnNavigationCompleted(object? sender, WebViewNavigationCompletedEventArgs e)
        {
            tcs.TrySetResult(e.IsSuccess);
        }

        browser.NavigationCompleted += OnNavigationCompleted;

        try
        {
            // 方法1: NavigateToString メソッドを使用（ライブラリのバージョンにある場合）
            browser.NavigateToString(htmlContent);

            // ※もし NavigateToString メソッドが存在しない・動作しない場合は
            //   下記のように HTML プロパティへ代入してください：
            // browser.HTML = htmlContent;

            return await tcs.Task;
        }
        finally
        {
            // 成功・失敗に関わらず確実にイベントハンドラーを解除（メモリリーク防止）
            browser.NavigationCompleted -= OnNavigationCompleted;
        }
    }


    public async Task LoadFile(MarkdownFile mdFile)
    {
        if (!_loaded) await PreViewSetup();
//        await browser.WaitForLoadAsync();

        if (mdFile.CodeDocument == null) return;
        string markdown = mdFile.CodeDocument.CreateString();
        string escaped = markdown
            .Replace("\\", "\\\\")
            .Replace("'", "\\'")
            .Replace("\r", "")
            .Replace("\n", "\\n");
        try
        {
            await browser.InvokeScript($"loadMarkdownContent('{escaped}');");
        }
        catch (Exception)
        {
            return;
        }
    }


}
