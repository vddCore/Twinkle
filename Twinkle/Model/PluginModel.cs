namespace Twinkle.Model;

using System;
using Twinkle.API.Extensibility;

public class PluginModel
{
    public Type PluginType { get; }
    public string ID { get; }
    public string Name { get; }
    
    public bool IsDualDisplay => PluginType.IsAssignableTo(typeof(DualDisplayController));

    public PluginModel(Type pluginType, string id, string name)
    {
        PluginType = pluginType;
        ID = id;
        Name = name;
    }
}