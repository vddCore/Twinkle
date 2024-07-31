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

public class DisplayLinkedControlViewModel : ViewModelBase
{
    private readonly IInputModuleControlService _inputModuleControlService;
    private readonly ISettingsService _settingsService;
    private readonly IPluginSystem _pluginSystem;

    private readonly ILog _log;
    
    public Settings<SettingsModel> AppSettings => _settingsService.AppSettings;

    public LedDisplayModel LeftModule { get; private set; } = null!;
    public LedDisplayModel RightModule { get; private set; } = null!;

    public ObservableCollection<PluginModel> AvailablePlugins { get; set; } = [];

    public PluginModel? SelectedPluginModel { get; set; }
    
    public DualDisplayController? CurrentPlugin { get; set; }
    public object? CurrentPluginView => CurrentPlugin?.View; 

    public DisplayLinkedControlViewModel(
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

    public void RefreshAvailablePlugins()
    {
        
    }
    
    private void OnDevicesRescanned(DevicesRescannedMessage _)
        => RefreshDisplayCollection();

    private void RefreshDisplayCollection()
    {
        if (_inputModuleControlService.Displays.Count > 1)
        {
            var left = _inputModuleControlService.Displays.First(
                x => x.SerialNumber == AppSettings.Model.LeftDeviceSerialNumber
            );
            
            var right = _inputModuleControlService.Displays.First(
                x => x.SerialNumber == AppSettings.Model.RightDeviceSerialNumber
            );

            LeftModule = new LedDisplayModel(left, _inputModuleControlService.Displays.IndexOf(left));
            RightModule = new LedDisplayModel(right, _inputModuleControlService.Displays.IndexOf(right));
        }
    }
}