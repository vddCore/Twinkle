namespace Twinkle.API.Logging.Sinks;

using System;

public sealed class CallbackSink : Sink
{
    private readonly Action<LogMessage> _callback;

    public CallbackSink(Action<LogMessage> callback)
    {
        _callback = callback;
    }
    
    public override void Write(LogMessage message)
        => _callback(message);
}