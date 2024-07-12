namespace Twinkle.API;

using System.ComponentModel;
using Avalonia.Controls;
using Starlight.Framework;

public abstract class DriverPlugin : INotifyPropertyChanged
{
    internal protected LedDisplay Display { get; internal set; }
    
    public abstract object? View { get; }

    public event PropertyChangedEventHandler? PropertyChanged;

    internal DriverPlugin(LedDisplay display)
    {
        Display = display;
    }
        
    internal virtual void Activate()
    {
        try
        {
            OnActivated();
        }
        catch
        {
            // todo: don't ignore it in the future.
        }
    }

    internal virtual void Deactivate()
    {
        try
        {
            OnDeactivated();
        }
        catch
        {
            // todo: don't ignore it in the future
        }
    }

    protected virtual void OnActivated()
    {
    }

    protected virtual void OnDeactivated()
    {
    }
}

public abstract class DriverPlugin<T> : DriverPlugin where T: Control, new()
{
    public override object View => new T { DataContext = this };

    protected DriverPlugin(LedDisplay display) 
        : base(display)
    {
    }
}