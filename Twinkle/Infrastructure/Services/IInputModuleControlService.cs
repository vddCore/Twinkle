namespace Twinkle.Infrastructure.Services;

using System.Collections.Generic;
using Glitonea.Mvvm;
using Starlight.Framework;

public interface IInputModuleControlService : IService
{
    List<LedDisplay> EnumerateDisplays();
}