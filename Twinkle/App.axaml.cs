namespace Twinkle;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PropertyChanged;
using Twinkle.API;

[DoNotNotify]
public class App : Application
{
    public static ApiContext ApiContext { get; private set; } = null!;

    public override void Initialize()
    {
        ApiContext = new ApiContext();
        
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new View.Windows.MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}