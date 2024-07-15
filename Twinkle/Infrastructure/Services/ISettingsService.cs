namespace Twinkle.Infrastructure.Services;

using Glitonea.Mvvm;
using Twinkle.API.Configuration;
using Twinkle.Model;

public interface ISettingsService : IService
{
    Settings<SettingsModel> AppSettings { get; }
}