namespace Twinkle.ViewModel;

using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Twinkle.API.Configuration;
using Twinkle.Infrastructure;
using Twinkle.Infrastructure.Services;
using Twinkle.Model;
using Twinkle.View.ContentPages;

public class RootViewModel : ViewModelBase
{
    private ISettingsService _settingsService;
    private IPluginSystem _pluginSystem;
    
    public SingleInstanceViewModelBase CurrentContentPageViewModel { get; private set; } = ContentPage.DisplayControl;
    public Settings<SettingsModel> AppSettings => _settingsService.AppSettings;

    public RootViewModel(
        ISettingsService settingsService,
        IPluginSystem pluginSystem)
    {
        _settingsService = settingsService;
        
        _pluginSystem = pluginSystem;
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
        => Message.Broadcast<DeviceRescanRequestedMessage>();
}