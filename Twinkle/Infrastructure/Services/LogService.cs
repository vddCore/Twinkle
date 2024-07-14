namespace Twinkle.Infrastructure.Services;

using System;
using System.Reflection;
using Twinkle.API.Logging;

public class LogService : ILogService
{
    public LogManager LogManager { get; }

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