namespace Twinkle.ViewModel.ContentPages;

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Glitonea.Mvvm;
using Twinkle.API.Logging;
using Twinkle.Infrastructure.Services;

public class DeveloperViewModel : SingleInstanceViewModelBase
{
    private readonly ILogService _logService;
    
    public ObservableCollection<LogMessage> LogMessages { get; } = new();
    public int BufferLength { get; set; } = 5000;

    public DeveloperViewModel(ILogService logService)
    {
        _logService = logService;
        _logService.LogMessageReceived += OnLogMessageReceived;
    }

    public void OpenLogFile()
        => Process.Start("notepad", _logService.LogFilePath);

    public void ClearLog()
        => LogMessages.Clear();

    public void OpenPluginDirectory()
        => Process.Start("explorer", App.ApiContext.PluginDirectory.FullName);

    public void OpenSettingsDirectory()
        => Process.Start("explorer", App.ApiContext.SettingsDirectory.FullName);

    public void OpenLogDirectory()
        => Process.Start("explorer", App.ApiContext.LogDirectory.FullName);

    public void OpenApplicationDirectory()
        => Process.Start("explorer", AppContext.BaseDirectory);
    
    private void OnLogMessageReceived(object? sender, LogMessage e)
    {
        LogMessages.Add(e);
        
        if (LogMessages.Count >= BufferLength)
        {
            LogMessages.RemoveAt(0);
        }
    }
}