namespace Twinkle.Model;

using Starlight.Framework;

public class LedDisplayModel
{
    public LedDisplay Display { get; }
    public int DeviceIndex { get; }
    public bool IsSelected { get; set; }

    public LedDisplayModel(LedDisplay display, int deviceIndex)
    {
        Display = display;
        DeviceIndex = deviceIndex;
    }
}