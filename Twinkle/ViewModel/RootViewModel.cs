namespace Twinkle.ViewModel;

using System.Linq;
using Glitonea.Mvvm;
using Twinkle.API.Configuration;
using Twinkle.Infrastructure;
using Twinkle.Infrastructure.Services;
using Twinkle.Model;
using Twinkle.View.ContentPages;

public class RootViewModel : ViewModelBase
{
    private readonly ISettingsService _settingsService;
    private readonly IPluginSystem _pluginSystem;
    private readonly IInputModuleControlService _inputModuleControlService;

    public SingleInstanceViewModelBase CurrentContentPageViewModel { get; private set; } = ContentPage.DisplayControl;
    public Settings<SettingsModel> AppSettings => _settingsService.AppSettings;

    public bool IsMultiDisplaySetup => _inputModuleControlService.Displays.Count > 1;

    public RootViewModel(
        ISettingsService settingsService,
        IPluginSystem pluginSystem,
        IInputModuleControlService inputModuleControlService)
    {
        _settingsService = settingsService;
        _pluginSystem = pluginSystem;
        _inputModuleControlService = inputModuleControlService;
        
        RescanDisplays();
        _pluginSystem.LoadPlugins();
    }

    public void MainViewContextSelectionChanged(object? parameter)
    {
        if (CurrentContentPageViewModel != parameter && parameter is SingleInstanceViewModelBase vm)
        {
            CurrentContentPageViewModel = vm;
        }
    }

    public void RearrangeDisplays()
    {
        (AppSettings.Model.LeftDeviceSerialNumber, AppSettings.Model.RightDeviceSerialNumber) 
            =  (AppSettings.Model.RightDeviceSerialNumber, AppSettings.Model.LeftDeviceSerialNumber);
        
        RescanDisplays();
    }

    public void RescanDisplays()
    {
        _inputModuleControlService.RescanDisplays();

        if (_inputModuleControlService.Displays.Any())
        {
            if (_inputModuleControlService.Displays.FirstOrDefault(x => x.SerialNumber == AppSettings.Model.LeftDeviceSerialNumber) == null)
            {
                AppSettings.Model.LeftDeviceSerialNumber = _inputModuleControlService.Displays[0].SerialNumber;
            }

            if (_inputModuleControlService.Displays.Count > 1)
            {
                if (_inputModuleControlService.Displays.FirstOrDefault(x => x.SerialNumber == AppSettings.Model.RightDeviceSerialNumber) == null
                    || AppSettings.Model.LeftDeviceSerialNumber == AppSettings.Model.RightDeviceSerialNumber)
                {
                    AppSettings.Model.RightDeviceSerialNumber = _inputModuleControlService.Displays[1].SerialNumber;
                }
            }
        }

        OnPropertyChanged(nameof(IsMultiDisplaySetup));

        if (!IsMultiDisplaySetup && AppSettings.Model.LinkedDisplayMode)
        {
            AppSettings.Model.LinkedDisplayMode = false;
        }
        
        new DevicesRescannedMessage().Broadcast();
    }
}