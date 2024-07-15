namespace Twinkle.ViewModel.ContentPages;

using Glitonea.Mvvm;
using Twinkle.API.Configuration;
using Twinkle.Infrastructure.Services;
using Twinkle.Model;

public class SettingsViewModel : SingleInstanceViewModelBase
{
    private readonly ISettingsService _settingsService;

    public Settings<SettingsModel> AppSettings => _settingsService.AppSettings;

    public SettingsViewModel(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }
}