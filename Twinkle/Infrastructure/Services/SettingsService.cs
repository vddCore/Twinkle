namespace Twinkle.Infrastructure.Services;

using Twinkle.API.Configuration;
using Twinkle.Model;

public class SettingsService : ISettingsService
{
    public Settings<SettingsModel> AppSettings { get; } = App.ApiContext.GetSettings<SettingsModel>()!;
}