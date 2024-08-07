﻿namespace Twinkle.API.Extensibility;

using Twinkle.API.Logging;

public abstract class DisplayController
{
    internal protected ApiContext ApiContext { get; }
    internal protected ILog Log { get; }
    
    public virtual object? View { get; protected set; }

    internal DisplayController(
        ApiContext apiContext,
        ILog log)
    {
        ApiContext = apiContext;
        Log = log;
    }
    
    protected virtual void OnActivated()
    {
    }

    protected virtual void OnDeactivated()
    {
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
}