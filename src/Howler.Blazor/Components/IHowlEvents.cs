using Howler.Blazor.Components.Events;
using System;

namespace Howler.Blazor.Components;

public interface IHowlEvents
{
    /// <summary>
    /// Fires when the sound begins playing.
    /// </summary>
    event Action<HowlPlayEventArgs>? OnPlay;

    /// <summary>
    /// Fires when the sound has been stopped.
    /// </summary>
    event Action<HowlEventArgs>? OnStop;

    /// <summary>
    /// Fires when the sound finishes playing (if it is looping, it'll fire at the end of each loop).
    /// </summary>
    event Action<HowlEventArgs>? OnEnd;

    /// <summary>
    /// Fires when the sound is unable to load.
    /// </summary>
    event Action<HowlErrorEventArgs>? OnLoadError;

    /// <summary>
    /// Fires when the sound is unable to play.
    /// </summary>
    event Action<HowlErrorEventArgs>? OnPlayError;

    /// <summary>
    /// Fires when the sound is loaded.
    /// </summary>
    event Action<EventArgs>? OnLoad;

    /// <summary>
    /// Fires when the sound has been paused.
    /// </summary>
    event Action<HowlEventArgs>? OnPause;

    /// <summary>
    /// Fires when the sound has been muted/unmuted.
    /// </summary>
    event Action<HowlEventArgs>? OnMute;

    /// <summary>
    /// Fires when the sound's volume has changed.
    /// </summary>
    event Action<HowlEventArgs>? OnVolume;

    /// <summary>
    /// Fires when the sound's playback rate has changed.
    /// </summary>
    event Action<HowlRateEventArgs>? OnRate;

    /// <summary>
    /// Fires when the sound has been seeked.
    /// </summary>
    event Action<HowlEventArgs>? OnSeek;

    /// <summary>
    /// Fires when the current sound finishes fading in/out.
    /// </summary>
    event Action<HowlEventArgs>? OnFade;

    /// <summary>
    /// Fires when audio has been automatically unlocked through a touch/click event.
    /// </summary>
    event Action<EventArgs>? OnUnlock;
}