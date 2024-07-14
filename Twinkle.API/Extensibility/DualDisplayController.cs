namespace Twinkle.API.Extensibility;

using Avalonia.Controls;
using Starlight.Framework;
using Starlight.Framework.Graphics;
using Twinkle.API.Logging;

public abstract class DualDisplayController : DisplayController
{
    internal LedDisplay LeftDisplay { get; }
    internal LedDisplay RightDisplay { get; }
    
    internal protected DualDisplayRenderer Renderer { get; }

    internal protected DualDisplayController(LedDisplay leftDisplay, LedDisplay rightDisplay, ILog log) 
        : base(log)
    {
        LeftDisplay = leftDisplay;
        RightDisplay = rightDisplay;

        Renderer = new DualDisplayRenderer();
        Renderer.Arrange(LeftDisplay, RightDisplay);
    }
}

public abstract class DualDisplayController<T> : DualDisplayController where T : Control, new()
{
    public override object View => new T { DataContext = this };
    
    internal protected DualDisplayController(LedDisplay leftDisplay, LedDisplay rightDisplay, ILog log)
        : base(leftDisplay, rightDisplay, log)
    {
    }
}