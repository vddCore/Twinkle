namespace Twinkle.Infrastructure.Services;

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Starlight.Framework;
using Twinkle.API;

public class PluginRepository : IPluginRepository
{
    public DriverPlugin? CurrentlyActivePlugin { get; private set; }

    public ObservableCollection<DriverPlugin> Plugins { get; } = new();

    public void LoadPlugins(string directory)
    {
        if (!Directory.Exists(directory))
        {
            return;
        }
        
        var files = Directory.GetFiles(directory, "*.twext.dll");

        foreach (var file in files)
        {
            var asm = Assembly.LoadFrom(file);
            var types = asm.GetTypes().Where(t => t.IsAssignableTo(typeof(DriverPlugin)));

            foreach (var t in types)
            {
                var instance = Activator.CreateInstance(t) as DriverPlugin;

                if (instance != null)
                {
                    Plugins.Add(instance);
                }
            }
        }
    }

    public void ActivatePlugin(DriverPlugin plugin, LedDisplay display)
    {
        if (CurrentlyActivePlugin != null)
        {
            DeactivateCurrentPlugin();
        }

        CurrentlyActivePlugin = plugin;
        CurrentlyActivePlugin.Activate(display);
    }

    public void DeactivateCurrentPlugin()
    {
        CurrentlyActivePlugin?.Deactivate();
        CurrentlyActivePlugin = null;
    }
}