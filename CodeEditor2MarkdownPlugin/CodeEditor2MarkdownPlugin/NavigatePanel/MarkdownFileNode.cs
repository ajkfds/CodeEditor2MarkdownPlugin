using Avalonia.Controls;
using Avalonia.Threading;
using CodeEditor2;
using HarfBuzzSharp;
using pluginMarkdown.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pluginMarkdown.NavigatePanel
{
    public class MarkdownFileNode : CodeEditor2.NavigatePanel.TextFileNode
    {
        public MarkdownFileNode(Data.MarkdownFile file):base(file)
        {

        }

        public CodeEditor2.Data.TextFile TextFile
        {
            get
            {
                return Item as CodeEditor2.Data.TextFile;
            }
        }

        public override async void OnSelected()
        {
            base.OnSelected(); // update context menu

            if (TextFile == null)
            {
//                if (NodeSelected != null) NodeSelected();
                Update();
                return;
            }


            CodeEditor2.Controller.CodeEditor.SetTextFile(TextFile, true);
//            if (NodeSelected != null) NodeSelected();

            UpdateVisual();

            if (TextFile.ParseValid && !TextFile.ReparseRequested)
            {
                // skip parse
            }
            else
            {
                if (TextFile == null) return;
//                await Tool.ParseHierarchy.ParseAsync(TextFile, Tool.ParseHierarchy.ParseMode.SearchReparseReqestedTree);
            }
            if( !CodeEditor2MarkdownPlugin.Global.PreviewWindow.IsVisible)
            {
                CodeEditor2MarkdownPlugin.Global.PreviewWindow.Show();
            }
            if(TextFile is MarkdownFile markdownFile)
            {
                await CodeEditor2MarkdownPlugin.Global.PreviewControl.LoadFile(markdownFile);
            }
        }

        public async Task UpdatePreVirew()
        {
            if (TextFile is MarkdownFile markdownFile)
            {
                await CodeEditor2MarkdownPlugin.Global.PreviewControl.LoadFile(markdownFile);
            }
        }

        //public override string Text
        //{
        //    get => FileItem.Name;
        //}

        //        private static ajkControls.Primitive.IconImage icon = new ajkControls.Primitive.IconImage(Properties.Resources.text);
        //public override void DrawNode(Graphics graphics, int x, int y, Font font, Color color, Color backgroundColor, Color selectedColor, int lineHeight, bool selected)
        //{
        //    graphics.DrawImage(icon.GetImage(lineHeight, ajkControls.Primitive.IconImage.ColorStyle.White), new Point(x, y));
        //    Color bgColor = backgroundColor;
        //    if (selected) bgColor = selectedColor;
        //    System.Windows.Forms.TextRenderer.DrawText(
        //        graphics,
        //        Text,
        //        font,
        //        new Point(x + lineHeight + (lineHeight >> 2), y),
        //        color,
        //        bgColor,
        //        System.Windows.Forms.TextFormatFlags.NoPadding
        //        );
        //}

        //public override void OnSelected()
        //{
        //    CodeEditor2.Controller.CodeEditor.SetTextFile(TextFile);
        //}

        public override void UpdateVisual()
        {
            if (Dispatcher.UIThread.CheckAccess())
            {
                _updateVisual();
            }
            else
            {
                Dispatcher.UIThread.Post(() =>
                {
                    _updateVisual();
                });
            }
        }

        public void _updateVisual()
        {
            string text = "null";
            CodeEditor2.Data.TextFile? textFile = TextFile;
            if (textFile != null) text = textFile.Name;
            Text = text;

            if (textFile == null)
            {
                Image = AjkAvaloniaLibs.Libs.Icons.GetSvgBitmap(
                    "CodeEditor2/Assets/Icons/questionDocument.svg",
                    Avalonia.Media.Color.FromArgb(100, 200, 200, 200),
                    "CodeEditor2/Assets/Icons/questionDocument.svg",
                    Avalonia.Media.Color.FromArgb(255, 255, 255, 200)
                    );
                Nodes.Clear();
                return;
            }

            if (textFile.CodeDocument != null && textFile.CodeDocument.IsDirty)
            {
                Image = AjkAvaloniaLibs.Libs.Icons.GetSvgBitmap(
                    "CodeEditor2/Assets/Icons/markdown.svg",
                    Avalonia.Media.Color.FromArgb(100, 200, 200, 200),
                    "CodeEditor2/Assets/Icons/shine.svg",
                    Avalonia.Media.Color.FromArgb(255, 255, 255, 200)
                    );
            }
            else
            {
                Image = AjkAvaloniaLibs.Libs.Icons.GetSvgBitmap(
                    "CodeEditor2/Assets/Icons/markdown.svg",
                    Avalonia.Media.Color.FromArgb(100, 200, 200, 200)
                    );
            }
            Controller.NavigatePanel.UpdateVisual();
        }
    }
}
