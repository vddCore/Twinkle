namespace Twinkle.API;

using System.ComponentModel;
using System.Reflection;
using Twinkle.API.Configuration;

public class ApiContext
{
    public DirectoryInfo PluginDirectory { get; }
    public DirectoryInfo SettingsDirectory { get; }
    public DirectoryInfo LogDirectory { get; }

    internal ApiContext(string? baseDirectory = null)
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

    public Settings<T>? GetSettings<T>(string? suffix = null)
        where T: INotifyPropertyChanged, new()
    {
        var fileName = Assembly.GetCallingAssembly().GetName().Name;
        
        if (suffix != null)
        {
            fileName += "." + suffix;
        }

        fileName += ".json";
        
        return new Settings<T>(Path.Combine(SettingsDirectory.FullName, fileName));
    }
}