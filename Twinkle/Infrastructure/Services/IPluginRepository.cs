namespace Twinkle.Infrastructure.Services;

using System.Collections.ObjectModel;
using Glitonea.Mvvm;
using Starlight.Framework;
using Twinkle.API;

public interface IPluginRepository : IService
{
    DriverPlugin? CurrentlyActivePlugin { get; }
    ObservableCollection<DriverPlugin> Plugins { get; }

    void LoadPlugins(string directory);
    void ActivatePlugin(DriverPlugin plugin, LedDisplay display);
    void DeactivateCurrentPlugin();
}