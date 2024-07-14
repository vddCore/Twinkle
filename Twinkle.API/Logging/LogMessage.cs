namespace Twinkle.API.Logging;

using System;
using System.Text;

public sealed class LogMessage
{
    public DateTime Timestamp { get; }
    public LogLevel Level { get; }
    public string Source { get; }
    public string Body { get; }
    public bool SuppressCallbackOutput { get; }
    public bool SuppressFileOutput { get; }

    public LogMessage(
        DateTime timestamp,
        LogLevel level,
        string source,
        string body,
        bool suppressCallbackOutput,
        bool suppressFileOutput
    )
    {
        Timestamp = timestamp;
        Level = level;
        Source = source;
        Body = body;
        SuppressCallbackOutput = suppressCallbackOutput;
        SuppressFileOutput = suppressFileOutput;
    }

    private string GetLogLevelVisual(LogLevel logLevel) => logLevel switch
    {
        LogLevel.Information => "[i] ",
        LogLevel.Warning => "[*] ",
        LogLevel.Error => "[E] ",
        LogLevel.Exception => "[X] ",
        LogLevel.Debug => "[D] ",
        _ => "[?] "
    };

    private string FormatMessage()
    {
        var sb = new StringBuilder();

        sb.Append(GetLogLevelVisual(Level));
        sb.Append("[");
        sb.Append(Timestamp.ToString("dd-MM-yyyy HH:mm:ss"));
        sb.Append("] ");
        sb.Append(Source);
        sb.Append(" :: ");
        sb.Append(Body);

        return sb.ToString();
    }

    public override string ToString()
        => FormatMessage();
}