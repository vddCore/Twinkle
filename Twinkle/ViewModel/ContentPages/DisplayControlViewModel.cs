namespace Twinkle.ViewModel.ContentPages;

using System.ComponentModel;
using Glitonea.Mvvm;
using Twinkle.API.Configuration;
using Twinkle.Infrastructure.Services;
using Twinkle.Model;

public class DisplayControlViewModel : SingleInstanceViewModelBase
{
    private readonly ISettingsService _settingsService;

    public Settings<SettingsModel> AppSettings => _settingsService.AppSettings;

    public SingleInstanceViewModelBase CurrentDisplayModeViewModel { get; set; } = null!;

    public DisplayControlViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        _settingsService.AppSettings.Model.PropertyChanged += AppSettingsPropertyChanged;
            
        UpdateCurrentDisplayModeView();
    }

    private void AppSettingsPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(AppSettings.Model.LinkedDisplayMode))
        {
            UpdateCurrentDisplayModeView();
        }
    }

    private void UpdateCurrentDisplayModeView()
    {
        CurrentDisplayModeViewModel = AppSettings.Model.LinkedDisplayMode
            ? ViewModelResolver.Instance.ResolveSingle<DisplayLinkedControlViewModel>()
            : ViewModelResolver.Instance.ResolveSingle<DisplaySeparateControlViewModel>();
    }
}