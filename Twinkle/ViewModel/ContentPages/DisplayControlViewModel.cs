namespace Twinkle.ViewModel.ContentPages;

using System.Collections.ObjectModel;
using Glitonea.Mvvm;
using Twinkle.API;
using Twinkle.API.Extensibility;
using Twinkle.API.Logging;
using Twinkle.Infrastructure;
using Twinkle.Infrastructure.Services;
using Twinkle.Model;

public class DisplayControlViewModel : SingleInstanceViewModelBase
{
    private readonly IInputModuleControlService _inputModuleControlService;
    private readonly IPluginSystem _pluginSystem;
    private readonly ILog _log;

    private LedDisplayModel? _selectedDisplay;
    
    public ObservableCollection<LedDisplayModel> Displays { get; private set; } = new();

    public bool IsAnyDisplaySelected => _selectedDisplay != null;

    public double ControlActionListOpacity => IsAnyDisplaySelected ? 1 : 0;
    public double SelectionPromptOpacity => IsAnyDisplaySelected ? 0 : 0.8;

    public ObservableCollection<PluginModel> AvailablePlugins => _pluginSystem.Plugins;
    
    public LedDisplayModel? SelectedDisplay
    {
        get => _selectedDisplay;
        
        set
        {
            if (_selectedDisplay != null)
            {
                _selectedDisplay.IsSelected = false;
            }

            _selectedDisplay = value;

            if (_selectedDisplay != null)
            {
                _selectedDisplay.IsSelected = true;
            }
            
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsAnyDisplaySelected));
            OnPropertyChanged(nameof(ControlActionListOpacity));
            OnPropertyChanged(nameof(SelectionPromptOpacity));
            OnPropertyChanged(nameof(CurrentPluginView));
        }
    }

    public PluginModel? CurrentPluginModel
    {
        get => SelectedDisplay?.CurrentPluginModel;
        
        set
        {
            if (SelectedDisplay != null)
            {
                SelectedDisplay.CurrentPluginModel = value;
            }
            
            OnPropertyChanged();
            OnPropertyChanged(nameof(CurrentPluginView));
        }    
    }

    public SingleDisplayController? CurrentPlugin
    {
        get => SelectedDisplay?.CurrentPlugin;
        
        set
        {
            if (SelectedDisplay != null)
            {
                SelectedDisplay.CurrentPlugin = value;
            }

            OnPropertyChanged();
            OnPropertyChanged(nameof(CurrentPluginView));
        }
    }
    
    public object? CurrentPluginView => SelectedDisplay?.CurrentPlugin?.View;

    public DisplayControlViewModel(
        IInputModuleControlService inputModuleControlService,
        IPluginSystem pluginSystem,
        ILogService logService)
    {
        _inputModuleControlService = inputModuleControlService;
        _pluginSystem = pluginSystem;
        _log = logService.GetLog();
        
        RescanDisplays();
        Subscribe<DeviceRescanRequestedMessage>(_ => RescanDisplays());
    }

    public void InputModuleSelectionChanged(object? parameter)
    {
        if (parameter is not LedDisplayModel ldm)
            return;

        if (SelectedDisplay == ldm)
            return;

        SelectedDisplay = ldm;
    }
    
    public void RescanDisplays()
    {
        _log.Info("Rescanning devices...");
        
        SelectedDisplay = null;
        Displays.Clear();

        var displayModules = _inputModuleControlService.EnumerateDisplays();

        for (var i = 0; i < displayModules.Count; i++)
        {
            _log.Info($"  Found display {i}: {displayModules[i].SerialNumber}");
            Displays.Add(new LedDisplayModel(displayModules[i], i));
        }
    }
}