namespace Twinkle.Infrastructure.Services;

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Twinkle.API.Logging;
using Twinkle.API.Logging.Sinks;

public class LogService : ILogService
{
    public LogManager LogManager { get; }

    public string LogFilePath
    {
        get
        {
            var fileSink = LogManager.ActiveSinks.FirstOrDefault(x => x is FileSink) as FileSink;

            if (fileSink == null)
                return string.Empty;
            
            return Path.Combine(fileSink.Directory.FullName, fileSink.FileName);
        }
    }

    public event EventHandler<LogMessage>? LogMessageReceived;

    public LogService()
    {
        LogManager = new LogManager()
            .LogToDebugStream()
            .LogToCallback((m) => LogMessageReceived?.Invoke(this, m))
            .LogToFile(App.ApiContext.LogDirectory.FullName);
    }

    public ILog GetLog() => LogManager.GetForCurrentAssembly();
    public ILog GetLog(Assembly assembly) => LogManager.GetForAssembly(assembly);
    
    public void Start() => LogManager.Start();
    public void Stop() => LogManager.Stop();
}