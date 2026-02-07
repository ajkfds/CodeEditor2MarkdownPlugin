using Avalonia.Threading;
using CodeEditor2.CodeEditor;
using CodeEditor2.CodeEditor.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginMarkdown.Data
{
    public class MarkdownFile : CodeEditor2.Data.TextFile
    {
        public static new Task<MarkdownFile> CreateAsync(string relativePath, CodeEditor2.Data.Project project)
        {
            string name;
            if (relativePath.Contains(System.IO.Path.DirectorySeparatorChar))
            {
                name = relativePath.Substring(relativePath.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);
            }
            else
            {
                name = relativePath;
            }
            MarkdownFile fileItem = new MarkdownFile()
            {
                Project = project,
                RelativePath = relativePath,
                Name = name
            };

            return Task.FromResult(fileItem);
        }



        protected override CodeEditor2.NavigatePanel.NavigatePanelNode CreateNode()
        {
            return new NavigatePanel.MarkdownFileNode(this);
        }

        public override DocumentParser CreateDocumentParser(DocumentParser.ParseModeEnum parseMode, System.Threading.CancellationToken? token)
        {
            if(parseMode==DocumentParser.ParseModeEnum.EditParse) StartAsyncPreviewUpdate();
            return new Parser.Parser(this, parseMode,token);
        }

        public override async Task AcceptParsedDocumentAsync(ParsedDocument newParsedDocument)
        {
            await base.AcceptParsedDocumentAsync(newParsedDocument);
        }
        public override async Task SaveAsync()
        {
            await base.SaveAsync();
            StartAsyncPreviewUpdate();
        }

        private void StartAsyncPreviewUpdate()
        {
            Dispatcher.UIThread.InvokeAsync(
            new Action(async () =>
            {
                await ((NavigatePanel.MarkdownFileNode)NavigatePanelNode).UpdatePreView();
            })
            );
        }
    }
}
