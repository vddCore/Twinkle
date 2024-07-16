namespace Twinkle.ViewModel;

using System.Collections.ObjectModel;
using System.Linq;
using Glitonea.Mvvm;
using Twinkle.API.Configuration;
using Twinkle.API.Extensibility;
using Twinkle.API.Logging;
using Twinkle.Infrastructure;
using Twinkle.Infrastructure.Services;
using Twinkle.Model;

public class DisplaySeparateControlViewModel : SingleInstanceViewModelBase
{
    private readonly IInputModuleControlService _inputModuleControlService;
    private readonly ISettingsService _settingsService;
    private readonly IPluginSystem _pluginSystem;
    private readonly ILog _log;

    private LedDisplayModel? _selectedDisplay;

    public Settings<SettingsModel> AppSettings => _settingsService.AppSettings;

    public ObservableCollection<LedDisplayModel> Displays { get; } = new();

    public bool IsAnyDisplaySelected => _selectedDisplay != null;

    public double ControlActionListOpacity => IsAnyDisplaySelected ? 1 : 0;
    public double SelectionPromptOpacity => IsAnyDisplaySelected ? 0 : 0.8;

    public ObservableCollection<PluginModel> AvailablePlugins
    {
        get
        {
            return new(_pluginSystem.Plugins.Where(x => x.PluginType.IsAssignableTo(typeof(SingleDisplayController))));
        }
    }

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

            OnPropertyChanged(nameof(IsAnyDisplaySelected));
            OnPropertyChanged(nameof(ControlActionListOpacity));
            OnPropertyChanged(nameof(SelectionPromptOpacity));
            OnPropertyChanged(nameof(CurrentPluginView));
            OnPropertyChanged();
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


    public DisplaySeparateControlViewModel(
        IInputModuleControlService inputModuleControlService,
        ISettingsService settingsService,
        IPluginSystem pluginSystem,
        ILogService logService)
    {
        _inputModuleControlService = inputModuleControlService;
        _settingsService = settingsService;
        _pluginSystem = pluginSystem;
        _log = logService.GetLog();

        RefreshDisplayCollection();

        Subscribe<DevicesRescannedMessage>(OnDevicesRescanned);
    }

    ~DisplaySeparateControlViewModel()
    {
        Unsubscribe<DevicesRescannedMessage>();
    }

    public void InputModuleSelectionChanged(object? parameter)
    {
        if (parameter is not LedDisplayModel ldm)
            return;

        if (SelectedDisplay == ldm)
            return;

        SelectedDisplay = ldm;
    }

    private void OnDevicesRescanned(DevicesRescannedMessage _)
        => RefreshDisplayCollection();

    private void RefreshDisplayCollection()
    {
        SelectedDisplay = null;
        Displays.Clear();

        if (_inputModuleControlService.Displays.Count > 1)
        {
            var left = _inputModuleControlService.Displays.First(
                x => x.SerialNumber == AppSettings.Model.LeftDeviceSerialNumber
            );
            
            var right = _inputModuleControlService.Displays.First(
                x => x.SerialNumber == AppSettings.Model.RightDeviceSerialNumber
            );


            Displays.Add(new LedDisplayModel(left, _inputModuleControlService.Displays.IndexOf(left)));
            Displays.Add(new LedDisplayModel(right, _inputModuleControlService.Displays.IndexOf(right)));
        }
        else if (_inputModuleControlService.Displays.Count == 1)
        {
            Displays.Add(new LedDisplayModel(_inputModuleControlService.Displays[0], 0));
        }
    }
}