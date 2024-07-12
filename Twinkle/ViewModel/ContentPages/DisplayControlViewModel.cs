namespace Twinkle.ViewModel.ContentPages;

using System.Collections.ObjectModel;
using Glitonea.Mvvm;
using Twinkle.API;
using Twinkle.Infrastructure;
using Twinkle.Infrastructure.Services;
using Twinkle.Model;

public class DisplayControlViewModel : SingleInstanceViewModelBase
{
    private readonly IInputModuleControlService _inputModuleControlService;
    private readonly IPluginRepository _pluginRepository;

    private LedDisplayModel? _selectedDisplay;
    
    public ObservableCollection<LedDisplayModel> Displays { get; private set; } = new();

    public bool IsAnyDisplaySelected => _selectedDisplay != null;

    public double ControlActionListOpacity => IsAnyDisplaySelected ? 1 : 0;
    public double SelectionPromptOpacity => IsAnyDisplaySelected ? 0 : 0.8;

    public ObservableCollection<PluginModel> AvailablePlugins => _pluginRepository.Plugins;
    
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

    public DriverPlugin? CurrentPlugin
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
        IPluginRepository pluginRepository)
    {
        _inputModuleControlService = inputModuleControlService;
        _pluginRepository = pluginRepository;
        
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
        SelectedDisplay = null;
        Displays.Clear();

        var displayModules = _inputModuleControlService.EnumerateDisplays();

        for (var i = 0; i < displayModules.Count; i++)
        {
            Displays.Add(new LedDisplayModel(displayModules[i], i));
        }
    }
}