using System;
using System.Threading.Tasks;

namespace Howler.Blazor.Components
{
    /// <summary>
    /// See https://github.com/goldfire/howler.js
    /// </summary>
    public interface IHowl : IHowlEvents
    {
        #region Properties
        TimeSpan TotalTime { get; }
        #endregion

        #region  Methods
        ValueTask<bool> IsPlaying();

        ValueTask<int> Play(params Uri[] locations);

        ValueTask<int> Play(params string[] sources);

        ValueTask<int> Play(byte[] audio, string mimeType);

        ValueTask<int> Play(HowlOptions options);

        ValueTask Stop();

        ValueTask Pause(int? soundId = null);

        ValueTask Seek(TimeSpan position);

        /// <summary>
        /// This is called by default, but if you set preload to false, you must call load before you can play any sounds.
        /// </summary>
        ValueTask Load();

        /// <summary>
        /// Unload and destroy a Howl object. This will immediately stop all sounds attached to this sound and remove it from the cache.
        /// </summary>
        ValueTask Unload();

        ValueTask<TimeSpan> GetCurrentTime();

        ValueTask<TimeSpan> GetTotalTime();
        #endregion
    }
}