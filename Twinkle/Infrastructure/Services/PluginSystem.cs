namespace Twinkle.Infrastructure.Services;

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Twinkle.API;
using Twinkle.API.Extensibility;
using Twinkle.API.Logging;
using Twinkle.Model;

public class PluginSystem : IPluginSystem
{
    private readonly ILog _log;

    public ObservableCollection<PluginModel> Plugins { get; } = [];

    public ApiContext ApiContext => App.ApiContext;

    public PluginSystem(ILogService logService)
    {
        _log = logService.GetLog();
    }

    public void LoadPlugins()
    {
        if (!Directory.Exists(ApiContext.PluginDirectory.FullName))
        {
            _log.Error("Plugins directory not found, will not load plugins.");
            return;
        }

        var files = ApiContext.PluginDirectory.GetFiles("*.twext.dll");

        foreach (var file in files)
        {
            var asm = Assembly.LoadFrom(file.FullName);
            
            var displayControllers = asm.GetTypes().Where(t => t.IsAssignableTo(typeof(DisplayController)));
            
            foreach (var t in displayControllers)
            {
                var attr = t.GetCustomAttribute<DisplayControllerAttribute>();

                if (attr != null)
                {
                    Plugins.Add(new PluginModel(t, attr.ID, attr.Name));
                }
            }
        }
    }
}