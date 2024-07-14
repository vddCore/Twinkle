namespace Twinkle.API;

public class ApiContext
{
    public DirectoryInfo PluginDirectory { get; }
    public DirectoryInfo SettingsDirectory { get; }
    public DirectoryInfo LogDirectory { get; }

    public ApiContext(string? baseDirectory = null)
    {
        baseDirectory ??= AppContext.BaseDirectory;
        
        (PluginDirectory = new DirectoryInfo(
            Path.Combine(baseDirectory, "Plugins")
        )).Create();

        (SettingsDirectory = new DirectoryInfo(
            Path.Combine(baseDirectory, "Settings")
        )).Create();

        (LogDirectory = new DirectoryInfo(
            Path.Combine(baseDirectory, "Logs")
        )).Create();
    }
}