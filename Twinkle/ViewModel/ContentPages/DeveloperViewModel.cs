﻿namespace Twinkle.ViewModel.ContentPages;

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Glitonea.Mvvm;
using Twinkle.API.Logging;
using Twinkle.Infrastructure.Services;

public class DeveloperViewModel : SingleInstanceViewModelBase
{
    private readonly ILogService _logService;
    private readonly ILog _log;
    
    public ObservableCollection<LogMessage> LogMessages { get; } = new();
    public int BufferLength { get; set; } = 5000;

    public DeveloperViewModel(ILogService logService)
    {
        _logService = logService;
        _logService.LogMessageReceived += OnLogMessageReceived;

        _log = _logService.GetLog();

        for (var i = 0; i < 120; i++)
        {
            _log.Info($"Hello, world! {i}");
        }
    }

    public void OpenLogFile()
        => Process.Start("notepad", _logService.LogFilePath);

    public void ClearLog()
    {
        LogMessages.Clear();
    }

    private void OnLogMessageReceived(object? sender, LogMessage e)
    {
        LogMessages.Add(e);
        
        if (LogMessages.Count >= BufferLength)
        {
            LogMessages.RemoveAt(0);
        }
    }
}