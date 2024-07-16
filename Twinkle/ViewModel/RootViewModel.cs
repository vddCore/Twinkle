namespace Twinkle.ViewModel;

using Glitonea.Mvvm;
using Twinkle.API.Configuration;
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

    public RootViewModel(
        ISettingsService settingsService,
        IPluginSystem pluginSystem,
        IInputModuleControlService inputModuleControlService)
    {
        _settingsService = settingsService;
        _pluginSystem = pluginSystem;
        _inputModuleControlService = inputModuleControlService;
        
        RequestDeviceRescan();
        _pluginSystem.LoadPlugins();
    }

    public void MainViewContextSelectionChanged(object? parameter)
    {
        if (CurrentContentPageViewModel != parameter && parameter is SingleInstanceViewModelBase vm)
        {
            CurrentContentPageViewModel = vm;
        }
    }

    public void RequestDeviceRescan()
        => _inputModuleControlService.RescanDisplays();
}