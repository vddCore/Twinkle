namespace Twinkle.API.Logging;

public abstract class Sink
{
    public bool IsActive { get; set; } = true;

    public abstract void Write(LogMessage message);
}