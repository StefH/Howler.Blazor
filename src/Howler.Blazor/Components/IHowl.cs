using System;
using System.Threading.Tasks;

namespace Howler.Blazor.Components;

/// <summary>
/// See https://github.com/goldfire/howler.js
/// </summary>
public interface IHowl : IHowlEvents
{
    #region Properties
    TimeSpan TotalTime { get; }
    #endregion

    #region  Methods
    ValueTask<int> Play(params Uri[] locations);

    ValueTask<int> Play(params string[] sources);

    ValueTask<int> Play(byte[] audio, string mimeType);

    ValueTask<int> Play(HowlOptions options);

    ValueTask Play(int soundId);

    ValueTask Stop(int soundId);

    ValueTask Pause(int soundId);

    ValueTask Seek(int soundId, TimeSpan position);

    ValueTask Rate(int soundId, double rate);

    /// <summary>
    /// This is called by default, but if you set preload to false, you must call load before you can play any sounds.
    /// </summary>
    ValueTask Load(int soundId);

    /// <summary>
    /// Unload and destroy a Howl object. This will immediately stop all sounds attached to this sound and remove it from the cache.
    /// </summary>
    ValueTask Unload(int soundId);

    ValueTask<double> GetRate(int soundId);

    ValueTask<TimeSpan> GetCurrentTime(int soundId);

    ValueTask<TimeSpan> GetTotalTime(int soundId);

    ValueTask<bool> IsPlaying(int soundId);

    ValueTask<double> Volume(int soundId, double volume);
    #endregion
}