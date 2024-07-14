namespace Twinkle.API.Logging;

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Twinkle.API.Logging.Sinks;

public sealed class LogManager
{
    private CancellationTokenSource? _cancellationTokenSource;

    private readonly List<Sink> _activeSinks = new();

    private readonly Dictionary<string, Log> _logCache;
    private readonly Queue<LogMessage> _writeQueue;

    public bool IsRunning => _cancellationTokenSource?.IsCancellationRequested ?? false; 

    public IReadOnlyList<Sink> ActiveSinks => _activeSinks;

    public LogManager()
    {
        _logCache = new Dictionary<string, Log>();
        _writeQueue = new Queue<LogMessage>();
    }

    public void Start()
    {
        if (IsRunning) return;

        _cancellationTokenSource = new CancellationTokenSource();
        
        #pragma warning disable CS4014
        {
            WriterBackgroundTask();
        }
        #pragma warning restore CS4014
    }

    public void Stop()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }

    internal Log GetForCurrentAssembly()
        => GetForAssembly(Assembly.GetCallingAssembly());

    internal Log GetForAssembly(Assembly assembly)
    {
        var instanceName = assembly.GetName().Name!;

        if (!_logCache.TryGetValue(instanceName, out var log))
        {
            log = new Log(this, instanceName);
            _logCache.Add(instanceName, log);
        }

        return log;
    }
        
    internal void EnqueueWriteOperation(
        LogLevel level,
        string sourceName,
        string text,
        bool suppressConsoleOutput = false,
        bool suppressFileOutput = false)
    {
        lock (_writeQueue)
        {
            _writeQueue.Enqueue(
                new LogMessage(
                    DateTime.Now,
                    level,
                    sourceName,
                    text, 
                    suppressConsoleOutput, 
                    suppressFileOutput
                )
            );
        }
    }
    
    public LogManager LogToFile(string logDirectory, int maxRecents = 5)
    {
        AddSink(new FileSink(logDirectory, $"{DateTime.Now:dd-MM-yyyy_HH_mm_ss}.log", maxRecents));
        return this;
    }

    public LogManager LogToCallback(Action<LogMessage> callback)
    {
        AddSink(new CallbackSink(callback));
        return this;
    }

    public LogManager LogToDebugStream()
    {
        AddSink(new DebugSink());
        return this;
    }

    public void AddSink(Sink sink)
    {
        if (!_activeSinks.Contains(sink))
        {
            _activeSinks.Add(sink);
        }
    }
        
    private async Task WriterBackgroundTask()
    {
        while (IsRunning)
        {
            lock (_writeQueue)
            {
                while (_writeQueue.Count > 0)
                {
                    var msg = _writeQueue.Dequeue();

                    foreach (var sink in _activeSinks)
                    {
                        sink.Write(msg);
                    }
                }
            }

            await Task.Delay(10);
        }
    }
}