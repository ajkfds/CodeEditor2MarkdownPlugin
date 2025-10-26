using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeEditor2MarkdownPlugin
{
    public static class Global
    {
        public static CodeEditor2MarkdownPlugin.PreviewWindow PreviewWindow = new CodeEditor2MarkdownPlugin.PreviewWindow();
        public static PreviewControl PreviewControl { get => PreviewWindow.PreviewControl; }
    }
}
