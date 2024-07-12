namespace Twinkle.API;

[AttributeUsage(AttributeTargets.Class)]
public class DriverPluginAttribute : Attribute
{
    public string ID { get; }
    public string Name { get; set; } = "I forgor :skull:";

    public DriverPluginAttribute(string id)
    {
        ID = id;
    }
}