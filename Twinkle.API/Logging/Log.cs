namespace Twinkle.API.Logging;

using System;
using System.Text;

internal sealed class Log : ILog
{
    private readonly LogManager _logManager;
    private readonly string _assemblyName;

    public LogLevel Verbosity { get; set; } = LogLevel.Exception;
    public bool ForwardSuppressedMessagesToFile { get; set; } = true;

    internal Log(LogManager logManager, string assemblyName)
    {
        _logManager = logManager;
        _assemblyName = assemblyName;
    }

    public void Info(string msg)
    {
        _logManager.EnqueueWriteOperation(
            LogLevel.Information,
            _assemblyName,
            msg
        );
    }

    public void Warning(string msg)
    {
        _logManager.EnqueueWriteOperation(
            LogLevel.Warning,
            _assemblyName,
            msg
        );
    }

    public void Error(string msg)
    {
        _logManager.EnqueueWriteOperation(
            LogLevel.Error,
            _assemblyName,
            msg
        );
    }

    public void Debug(string msg)
    {
        _logManager.EnqueueWriteOperation(
            LogLevel.Debug,
            _assemblyName,
            msg
        );
    }

    public void Exception(Exception e)
    {
        if (Verbosity < LogLevel.Exception && !ForwardSuppressedMessagesToFile)
        {
            return;
        }

        var sb = new StringBuilder();
        sb.AppendLine("An exception has occurred.");

        var currentException = e;
        var indent = "  ";

        while (currentException != null)
        {
            var lines = currentException.ToString().Split('\n');
            for (var i = 0; i < lines.Length; i++)
            {
                sb.Append(indent);

                if (i < lines.Length - 1)
                {
                    sb.AppendLine(lines[i]);
                }
                else
                {
                    sb.Append(lines[i]);
                }
            }

            indent += "  ";
            currentException = currentException.InnerException;
        }

        _logManager.EnqueueWriteOperation(
            LogLevel.Exception,
            _assemblyName,
            sb.ToString()
        );
    }
}