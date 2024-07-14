namespace Twinkle.API.Extensibility;

using Avalonia.Controls;
using Starlight.Framework;
using Starlight.Framework.Graphics;
using Twinkle.API.Logging;

public abstract class SingleDisplayController : DisplayController
{
    internal LedDisplay Display { get; }
    
    internal protected SingleDisplayRenderer Renderer { get; }
    
    internal protected SingleDisplayController(LedDisplay display, ILog log)
        : base(log)
    {
        Display = display;
        Renderer = new SingleDisplayRenderer(display);
    }
}

public abstract class SingleDisplayController<T> : SingleDisplayController where T : Control, new()
{
    public override object View => new T { DataContext = this };
    
    internal protected SingleDisplayController(LedDisplay display, ILog log)
        : base(display, log)
    {
    }
}