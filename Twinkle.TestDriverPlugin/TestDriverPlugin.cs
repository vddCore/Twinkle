namespace Twinkle.TestDriverPlugin;

using System.ComponentModel;
using Starlight.Framework;
using Starlight.Framework.Graphics;
using Twinkle.API;

[DriverPlugin("TestDriverPlugin", Name = "Test Driver")]
public class TestDriverPlugin : DriverPlugin<TestDriverPluginView>
{
    public SingleDisplayRenderer Renderer { get; set; }
    public int SliderValue { get; set; }

    public TestDriverPlugin(LedDisplay display)
        : base(display)
    {
        PropertyChanged += DriverPropertyChanged;
        Renderer = new SingleDisplayRenderer(display);
    }

    protected override void OnDeactivated()
    {
        Renderer = null!;
    }

    private void DriverPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SliderValue))
        {
            Renderer.Clear();

            for (var y = 0; y < SliderValue; y++)
            {
                Renderer.DrawLine(
                    0,
                    Display.Height - y - 1,
                    Display.Width - 1,
                    Display.Height - y - 1,
                    255
                );
            }

            Renderer.PushFramebuffer();
        }
    }
}