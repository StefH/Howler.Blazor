using System;
using Howler.Blazor.Components.Events;

namespace Howler.Blazor.Components;

public partial class Howl
{
    public event Action<HowlPlayEventArgs>? OnPlay;
    public event Action<HowlEventArgs>? OnStop;
    public event Action<HowlEventArgs>? OnEnd;
    public event Action<HowlErrorEventArgs>? OnLoadError;
    public event Action<HowlErrorEventArgs>? OnPlayError;
    public event Action<EventArgs>? OnLoad;
    public event Action<HowlEventArgs>? OnPause;
    public event Action<HowlEventArgs>? OnMute;
    public event Action<HowlEventArgs>? OnVolume;
    public event Action<HowlRateEventArgs>? OnRate;
    public event Action<HowlEventArgs>? OnSeek;
    public event Action<HowlEventArgs>? OnFade;
    public event Action<EventArgs>? OnUnlock;
}