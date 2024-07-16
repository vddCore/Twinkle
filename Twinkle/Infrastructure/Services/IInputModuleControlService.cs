namespace Twinkle.Infrastructure.Services;

using System.Collections.ObjectModel;
using Glitonea.Mvvm;
using Starlight.Framework;

public interface IInputModuleControlService : IService
{
    ObservableCollection<LedDisplay> Displays { get; }
    void RescanDisplays();
}