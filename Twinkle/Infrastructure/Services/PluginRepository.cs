namespace Twinkle.Infrastructure.Services;

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Twinkle.API;
using Twinkle.Model;

public class PluginRepository : IPluginRepository
{
    public ObservableCollection<PluginModel> Plugins { get; } = new();

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
                var attr = t.GetCustomAttribute<DriverPluginAttribute>();
                
                if (attr != null)
                {
                    Plugins.Add(new PluginModel(t, attr.ID, attr.Name));
                }
            }
        }
    }
}