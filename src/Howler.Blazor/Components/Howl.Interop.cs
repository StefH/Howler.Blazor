using Howler.Blazor.Components.Events;
using Microsoft.JSInterop;
using System;

namespace Howler.Blazor.Components
{
    public partial class Howl
    {
        [JSInvokable]
        public void OnPlayCallback(int soundId, int durationInSeconds)
        {
            TotalTime = TimeSpan.FromSeconds(durationInSeconds);

            OnPlay?.Invoke(new HowlPlayEventArgs { SoundId = soundId, TotalTime = TotalTime });
        }

        [JSInvokable]
        public void OnStopCallback(int soundId)
        {
            TotalTime = TimeSpan.Zero;

            OnStop?.Invoke(new HowlEventArgs { SoundId = soundId });
        }

        [JSInvokable]
        public void OnPauseCallback(int soundId)
        {
            TotalTime = TimeSpan.Zero;

            OnPause?.Invoke(new HowlEventArgs { SoundId = soundId });
        }

        [JSInvokable]
        public void OnEndCallback(int soundId)
        {
            OnEnd?.Invoke(new HowlEventArgs { SoundId = soundId });
        }

        [JSInvokable]
        public void OnLoadCallback()
        {
            OnLoad?.Invoke(new EventArgs());
        }

        [JSInvokable]
        public void OnLoadErrorCallback(int? soundId, string error)
        {
            OnLoadError?.Invoke(new HowlErrorEventArgs { SoundId = soundId, Error = error });
        }

        [JSInvokable]
        public void OnPlayErrorCallback(int? soundId, string error)
        {
            OnPlayError?.Invoke(new HowlErrorEventArgs { SoundId = soundId, Error = error });
        }
    }
}