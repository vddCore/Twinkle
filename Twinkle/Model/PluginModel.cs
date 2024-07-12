namespace Twinkle.Model;

using System;

public class PluginModel
{
    public Type PluginType { get; }
    public string ID { get; }
    public string Name { get; }

    public PluginModel(Type pluginType, string id, string name)
    {
        PluginType = pluginType;
        ID = id;
        Name = name;
    }
}