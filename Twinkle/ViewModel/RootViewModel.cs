namespace Twinkle.ViewModel;

using Glitonea.Mvvm;
using Glitonea.Mvvm.Messaging;
using Twinkle.Infrastructure;
using Twinkle.Infrastructure.Services;
using Twinkle.View.ContentPages;

public class RootViewModel : ViewModelBase
{
    private IPluginRepository _pluginRepository;
    
    public SingleInstanceViewModelBase CurrentContentPageViewModel { get; private set; } = ContentPage.DisplayControl;

    public RootViewModel(IPluginRepository pluginRepository)
    {
        _pluginRepository = pluginRepository;
        _pluginRepository.LoadPlugins("plugins");
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