using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeEditor2.CodeEditor.Parser;

namespace pluginMarkdown.Parser
{
    public class Parser : DocumentParser
    {
        [SetsRequiredMembers]
        public Parser(Data.MarkdownFile file, DocumentParser.ParseModeEnum parseMode, System.Threading.CancellationToken? token) : base(file, parseMode,token)
        {
            this.Document = new CodeEditor2.CodeEditor.CodeDocument(file); // use verilog codeDocument
            this.Document.CopyTextOnlyFrom(file.CodeDocument);
            this.ParseMode = parseMode;
            this.TextFile = file as CodeEditor2.Data.TextFile;

            ParsedDocument = new CodeEditor2.CodeEditor.ParsedDocument(file, file.CodeDocument.Version, parseMode);
        }

        public override void Parse()
        {
            for(int line = 1; line<Document.Lines; line++)
            {
                string lineText = Document.CreateString(Document.GetLineStartIndex(line), Document.GetLineLength(line));
                if (lineText.StartsWith("# "))
                {
                    colorLine(Style.Color.Header,line);
                }
                else if(lineText.StartsWith("## "))
                {
                    colorLine(Style.Color.Header, line);
                }
                else if(lineText.StartsWith("### "))
                {
                    colorLine(Style.Color.Header, line);
                }
            }
        }

        private void colorLine(Style.Color color,int line)
        {
            int start = Document.GetLineStartIndex(line);
            int end = start + Document.GetLineLength(line);
            for (int i = start; i < end; i++)
            {
                Document.TextColors.SetColorAt(i, (byte)color);
            }
        }
    }
}
