namespace Twinkle.Infrastructure.Services;

using System.Collections.ObjectModel;
using Glitonea.Mvvm;
using Twinkle.API;
using Twinkle.Model;

public interface IPluginSystem : IService
{
    ApiContext ApiContext { get; }
    
    ObservableCollection<PluginModel> Plugins { get; }

    void LoadPlugins();
}