﻿namespace Twinkle.Model;

using System;
using System.ComponentModel;
using Starlight.Framework;
using Twinkle.API;

public class LedDisplayModel : INotifyPropertyChanged
{
    private DriverPlugin? _currentPlugin;
    private PluginModel? _currentPluginModel;

    public LedDisplay Display { get; }
    public int DeviceIndex { get; }
    
    public bool IsSelected { get; set; }

    public PluginModel? CurrentPluginModel
    {
        get => _currentPluginModel;
        set
        {
            if (value != null)
            {
                _currentPluginModel = value;
                
                CurrentPlugin = (DriverPlugin?)Activator.CreateInstance(
                    _currentPluginModel.PluginType,
                    new object[] { Display }
                );
            }
            else
            {
                CurrentPlugin = null;
            }
        }
    }

    public DriverPlugin? CurrentPlugin
    {
        get => _currentPlugin;
        set
        {
            if (value == _currentPlugin)
            {
                return;
            }
                
            if (_currentPlugin != null)
            {
                _currentPlugin.Deactivate();
            }

            _currentPlugin = value;

            if (_currentPlugin != null)
            {
                _currentPlugin.Activate();
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public LedDisplayModel(LedDisplay display, int deviceIndex)
    {
        Display = display;
        DeviceIndex = deviceIndex;
    }
}