namespace Twinkle.Infrastructure.Services;

using System.Collections.ObjectModel;
using Starlight.Framework;

public class InputModuleControlService : IInputModuleControlService
{
    public ObservableCollection<LedDisplay> Displays { get; } = [];

    public void RescanDisplays()
    {
        Displays.Clear();

        foreach (var display in LedDisplay.Enumerate())
        {
            Displays.Add(display);
        }
    }
}