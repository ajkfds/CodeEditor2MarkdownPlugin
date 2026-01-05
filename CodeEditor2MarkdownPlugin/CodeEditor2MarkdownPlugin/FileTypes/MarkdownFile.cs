using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginMarkdown.FileTypes
{
    public class MarkdownFile : CodeEditor2.FileTypes.FileType
    {
        public override string ID { get => "MarkdownFile"; }

        public override bool IsThisFileType(string relativeFilePath, CodeEditor2.Data.Project project)
        {
            if (
                relativeFilePath.ToLower().EndsWith(".md")
            )
            {
                return true;
            }
            return false;
        }

        public override CodeEditor2.Data.File CreateFile(string relativeFilePath, CodeEditor2.Data.Project project)
        {
            return Data.MarkdownFile.Create(relativeFilePath, project);
        }
        public override IImage GetIconImage()
        {
            return AjkAvaloniaLibs.Libs.Icons.GetSvgBitmap(
                    "CodeEditor2/Assets/Icons/markdown.svg",
                    Avalonia.Media.Color.FromArgb(100, 200, 200, 200)
                );
        }
    }

}
