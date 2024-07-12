namespace Twinkle.UI.Controls;

using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

public partial class GlyphButton : UserControl
{
    public static readonly StyledProperty<char> GlyphProperty 
        = AvaloniaProperty.Register<GlyphButton, char>(nameof(Glyph));
    
    public static readonly StyledProperty<double> GlyphSizeProperty 
        = AvaloniaProperty.Register<GlyphButton, double>(nameof(GlyphSize), 24);
    
    public static readonly StyledProperty<IBrush?> GlyphBrushProperty 
        = AvaloniaProperty.Register<GlyphButton, IBrush?>(nameof(GlyphBrush), Brushes.White);
    
    public static readonly StyledProperty<string> HeaderTextProperty
        = AvaloniaProperty.Register<GlyphButton, string>(nameof(HeaderText));
    
    public static readonly StyledProperty<double> HeaderTextSizeProperty 
        = AvaloniaProperty.Register<GlyphButton, double>(nameof(HeaderTextSize), 16);
    
    public static readonly StyledProperty<IBrush?> HeaderTextBrushProperty 
        = AvaloniaProperty.Register<GlyphButton, IBrush?>(nameof(HeaderTextBrush), Brushes.White);

    public static readonly StyledProperty<string> SubTextProperty 
        = AvaloniaProperty.Register<GlyphButton, string>(nameof(SubText));

    public static readonly StyledProperty<double> SubTextSizeProperty 
        = AvaloniaProperty.Register<GlyphButton, double>(nameof(SubTextSize), 12);
    
    public static readonly StyledProperty<IBrush?> SubTextBrushProperty 
        = AvaloniaProperty.Register<GlyphButton, IBrush?>(nameof(SubTextBrush), Brushes.White);
    
    public static readonly StyledProperty<ICommand> CommandProperty
        = AvaloniaProperty.Register<GlyphButton, ICommand>(nameof(Command));
    
    public static readonly StyledProperty<object?> CommandParameterProperty
        = AvaloniaProperty.Register<GlyphButton, object?>(nameof(CommandParameter));
    
    public char Glyph
    {
        get => GetValue(GlyphProperty);
        set => SetValue(GlyphProperty, value);
    }
    
    public double GlyphSize
    {
        get => GetValue(GlyphSizeProperty);
        set => SetValue(GlyphSizeProperty, value);
    }

    public IBrush? GlyphBrush
    {
        get => GetValue(GlyphBrushProperty);
        set => SetValue(GlyphBrushProperty, value);
    }
    
    public string HeaderText
    {
        get => GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }
    
    public double HeaderTextSize
    {
        get => GetValue(HeaderTextSizeProperty);
        set => SetValue(HeaderTextSizeProperty, value);
    }
    
    public IBrush? HeaderTextBrush
    {
        get => GetValue(HeaderTextBrushProperty);
        set => SetValue(HeaderTextBrushProperty, value);
    }
    
    public string SubText
    {
        get => GetValue(SubTextProperty);
        set => SetValue(SubTextProperty, value);
    }
    
    public double SubTextSize
    {
        get => GetValue(SubTextSizeProperty);
        set => SetValue(SubTextSizeProperty, value);
    }

    public IBrush? SubTextBrush
    {
        get => GetValue(SubTextBrushProperty);
        set => SetValue(SubTextBrushProperty, value);
    }
    
    public ICommand Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
    
    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    public GlyphButton()
    {
        InitializeComponent();
    }
}