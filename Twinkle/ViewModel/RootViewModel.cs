namespace Twinkle.ViewModel;

using Glitonea.Mvvm;
using View.ContentPages;

public class RootViewModel : ViewModelBase
{
    public SingleInstanceViewModelBase CurrentContentPageViewModel { get; private set; } = ContentPage.DisplayControl;

    public void MainViewContextSelectionChanged(object? parameter)
    {
        if (CurrentContentPageViewModel != parameter
            && parameter is SingleInstanceViewModelBase vm)
        {
            CurrentContentPageViewModel = vm;
        }
    }
}