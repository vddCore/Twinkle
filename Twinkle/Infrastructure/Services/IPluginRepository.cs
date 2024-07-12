namespace Twinkle.Infrastructure.Services;

using System.Collections.ObjectModel;
using Glitonea.Mvvm;
using Twinkle.Model;

public interface IPluginRepository : IService
{
    ObservableCollection<PluginModel> Plugins { get; }

    void LoadPlugins(string directory);
}