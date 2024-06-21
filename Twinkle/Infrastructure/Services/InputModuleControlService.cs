namespace Twinkle.Infrastructure.Services;

using System.Collections.Generic;
using System.Linq;
using Starlight.Framework;

public class InputModuleControlService : IInputModuleControlService
{
    public List<LedDisplay> EnumerateDisplays()
        => LedDisplay.Enumerate().ToList();
}