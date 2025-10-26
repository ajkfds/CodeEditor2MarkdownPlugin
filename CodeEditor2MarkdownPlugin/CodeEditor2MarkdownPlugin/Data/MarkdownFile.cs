﻿using Avalonia.Threading;
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
        public static new MarkdownFile Create(string relativePath, CodeEditor2.Data.Project project)
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

            return fileItem;
        }



        protected override CodeEditor2.NavigatePanel.NavigatePanelNode CreateNode()
        {
            return new NavigatePanel.MarkdownFileNode(this);
        }

        public override DocumentParser CreateDocumentParser(DocumentParser.ParseModeEnum parseMode, System.Threading.CancellationToken? token)
        {
            return new Parser.Parser(this, parseMode,token);
        }

        public override void Save()
        {
            base.Save();
            Dispatcher.UIThread.Post(
            new Action(() =>
            {
                ((NavigatePanel.MarkdownFileNode)NavigatePanelNode).UpdatePreVirew();
            })
        );
        }
    }
}
