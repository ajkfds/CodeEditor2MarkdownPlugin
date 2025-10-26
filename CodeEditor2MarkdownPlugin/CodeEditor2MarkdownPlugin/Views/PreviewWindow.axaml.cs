using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CodeEditor2MarkdownPlugin;

public partial class PreviewWindow : Window
{
    public PreviewWindow()
    {
        InitializeComponent();
    }

    public PreviewControl PreviewControl { get => previewControl; }
}