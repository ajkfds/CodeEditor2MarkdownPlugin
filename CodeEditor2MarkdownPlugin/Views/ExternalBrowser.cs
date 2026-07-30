using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEditor2MarkdownPlugin.Views
{
    internal class ExternalBrowser
    {
    }
    /*
    private async Task testStealth()
    {
        using var playwright = await Playwright.CreateAsync();

        // Edge (Chromium) 繧定ｵｷ蜍・
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Channel = "msedge",
            Headless = false,
            Args = new[]
            {
                "--disable-blink-features=AutomationControlled",
                "--no-first-run",
                "--no-default-browser-check"
            }
        });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 " +
                        "(KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36 Edg/120.0.0.0",
            Locale = "ja-JP",
            TimezoneId = "Asia/Tokyo",
            ViewportSize = new ViewportSize { Width = 1366, Height = 768 }
        });

        var page = await context.NewPageAsync();

        //// 譛蠕後∪縺ｧ蠕・▽ or 繧ｹ繧ｯ繝ｪ繝ｼ繝ｳ繧ｷ繝ｧ繝・ヨ繧貞叙繧・
        //await page.WaitForTimeoutAsync(3000);
        //await page.ScreenshotAsync(new PageScreenshotOptions { Path = "sannysoft.png", FullPage = true });
        await page.GotoAsync("https://www.google.com/");
        await Task.Delay(rand.Next(100, 200));

        await page.FillAsync("#APjFqb", "Playwright");
        await page.Keyboard.PressAsync("Enter");

        await Task.Delay(rand.Next(100, 200));
    }     
     */
}
