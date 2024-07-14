namespace Twinkle.API.Logging.Sinks;

using System.Diagnostics;

public sealed class DebugSink : Sink
{
    public override void Write(LogMessage message)
        => Debug.WriteLine(message.ToString());
}