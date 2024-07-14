namespace Twinkle.API.Logging;

public interface ILog
{
    LogLevel Verbosity { get; set; }
    bool ForwardSuppressedMessagesToFile { get; set; }
        
    void Info(string msg);
    void Warning(string msg);
    void Error(string msg);
    void Exception(Exception e);
    void Debug(string msg);
}