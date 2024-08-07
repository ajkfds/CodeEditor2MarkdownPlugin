using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeEditor2.CodeEditor.Parser;

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
            if (relativePath.Contains(System.IO.Path.DirectorySeparatorChar))
            {
                fileItem.Name = relativePath.Substring(relativePath.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
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

        public override DocumentParser CreateDocumentParser(DocumentParser.ParseModeEnum parseMode)
        {
            return new Parser.Parser(this, parseMode);
        }


    }
}
