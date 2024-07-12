namespace Twinkle.Model;

using System.ComponentModel;
using Starlight.Framework;
using Twinkle.API;

public class LedDisplayModel : INotifyPropertyChanged
{
    private DriverPlugin? _currentPlugin;
    
    public LedDisplay Display { get; }
    public int DeviceIndex { get; }
    
    public bool IsSelected { get; set; }

    public DriverPlugin? CurrentPlugin
    {
        get => _currentPlugin;
        set
        {
            if (value == _currentPlugin)
            {
                return;
            }
                
            if (_currentPlugin != null)
            {
                _currentPlugin.Deactivate();
            }

            _currentPlugin = value;

            if (_currentPlugin != null)
            {
                _currentPlugin.Activate(Display);
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public LedDisplayModel(LedDisplay display, int deviceIndex)
    {
        Display = display;
        DeviceIndex = deviceIndex;
    }
}