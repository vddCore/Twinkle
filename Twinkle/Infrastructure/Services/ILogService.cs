namespace Twinkle.Infrastructure.Services;

using System;
using System.Reflection;
using Glitonea.Mvvm;
using Twinkle.API.Logging;

public interface ILogService : IService
{
    event EventHandler<LogMessage>? LogMessageReceived;

    string LogFilePath { get; }

    ILog GetLog();
    ILog GetLog(Assembly assembly);
    
    void Start();
    void Stop();
}