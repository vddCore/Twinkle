namespace Twinkle.API.Extensibility;

[AttributeUsage(AttributeTargets.Class)]
public class DisplayControllerAttribute : Attribute
{
    public string ID { get; }
    public string Name { get; set; } = "I forgor :skull:";

    public DisplayControllerAttribute(string id)
    {
        ID = id;
    }
}