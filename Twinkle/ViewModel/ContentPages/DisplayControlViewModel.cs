namespace Twinkle.ViewModel.ContentPages;

using System.Collections.ObjectModel;
using Glitonea.Mvvm;
using Infrastructure.Services;
using Model;

public class DisplayControlViewModel : SingleInstanceViewModelBase
{
    private readonly IInputModuleControlService _inputModuleControlService;

    public ObservableCollection<LedDisplayModel> Displays { get; private set; } = new();

    public DisplayControlViewModel(IInputModuleControlService inputModuleControlService)
    {
        _inputModuleControlService = inputModuleControlService;
        RefreshDisplayCollection();
    }

    public void InputModuleSelectionChanged(object? parameter)
    {
        if (parameter is not LedDisplayModel)
            return;
        
        
    }

    private void RefreshDisplayCollection()
    {
        Displays.Clear();

        var displayModules = _inputModuleControlService.EnumerateDisplays();

        for (var i = 0; i < displayModules.Count; i++)
        {
            Displays.Add(new LedDisplayModel(displayModules[i], i));
        }
    }
}