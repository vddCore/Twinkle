namespace Twinkle.View.ContentPages;

using Glitonea.Mvvm;
using ViewModel.ContentPages;

public static class ContentPage
{
    public static DisplayControlViewModel DisplayControl { get; } =
        ViewModelResolver.Instance.ResolveSingle<DisplayControlViewModel>();

    public static SettingsViewModel Settings { get; } =
        ViewModelResolver.Instance.ResolveSingle<SettingsViewModel>();

    public static ScriptingViewModel Scripting { get; } =
        ViewModelResolver.Instance.ResolveSingle<ScriptingViewModel>();
}