namespace Twinkle.Model;

using System.ComponentModel;

public class SettingsModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public bool ShowDevelopmentTab { get; set; }
    public bool LinkedDisplayMode { get; set; }

    public string? LeftDeviceSerialNumber { get; set; }
    public string? RightDeviceSerialNumber { get; set; }
}