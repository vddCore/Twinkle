namespace Twinkle;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Glitonea.Mvvm;
using PropertyChanged;
using Twinkle.API;
using Twinkle.Infrastructure.Services;

[DoNotNotify]
public class App : Application
{
    public static ApiContext ApiContext { get; private set; } = null!;

    public override void Initialize()
    {
        ApiContext = new ApiContext();
        ServiceResolver.Instance.Resolve<ILogService>().Start();
        
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new View.Windows.MainWindow();
            desktop.ShutdownRequested += OnDesktopLifetimeShutdownRequested;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void OnDesktopLifetimeShutdownRequested(object? sender, ShutdownRequestedEventArgs e)
    {
        ServiceResolver.Instance.Resolve<ILogService>().Stop();
    }
}