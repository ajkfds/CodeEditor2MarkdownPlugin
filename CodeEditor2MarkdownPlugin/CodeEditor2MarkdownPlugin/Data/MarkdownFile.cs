using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginMarkdown.Data
{
    public class MarkdownFile : CodeEditor2.Data.TextFile
    {
        public static new MarkdownFile Create(string relativePath, CodeEditor2.Data.Project project)
        {
            //string id = GetID(relativePath, project);

            MarkdownFile fileItem = new MarkdownFile();
            fileItem.Project = project;
            fileItem.RelativePath = relativePath;
            if (relativePath.Contains('\\'))
            {
                fileItem.Name = relativePath.Substring(relativePath.LastIndexOf('\\') + 1);
            }
            else
            {
                fileItem.Name = relativePath;
            }

            return fileItem;
        }



        protected override CodeEditor2.NavigatePanel.NavigatePanelNode createNode()
        {
            return new CodeEditor2.NavigatePanel.TextFileNode(this);
        }

        public override CodeEditor2.CodeEditor.DocumentParser CreateDocumentParser(CodeEditor2.CodeEditor.DocumentParser.ParseModeEnum parseMode)
        {
            return new Parser.Parser(this, parseMode);
        }


    }
}
