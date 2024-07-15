namespace Twinkle.Model;

using System.ComponentModel;

public class SettingsModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool ShowDevelopmentTab { get; set; }
}
