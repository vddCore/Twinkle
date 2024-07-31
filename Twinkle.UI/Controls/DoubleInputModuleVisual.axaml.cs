using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Twinkle.UI.Controls;

public partial class DoubleInputModuleVisual : UserControl
{
    public static readonly StyledProperty<object?> LeftModuleContentProperty 
        = AvaloniaProperty.Register<DoubleInputModuleVisual, object?>(nameof(LeftModuleContent));
    
    public static readonly StyledProperty<object?> RightModuleContentProperty 
        = AvaloniaProperty.Register<DoubleInputModuleVisual, object?>(nameof(RightModuleContent));

    public object? LeftModuleContent
    {
        get => GetValue(LeftModuleContentProperty);
        set => SetValue(LeftModuleContentProperty, value);
    }

    public object? RightModuleContent
    {
        get => GetValue(RightModuleContentProperty);
        set => SetValue(RightModuleContentProperty, value);
    }
    
    public DoubleInputModuleVisual()
    {
        InitializeComponent();
    }
}