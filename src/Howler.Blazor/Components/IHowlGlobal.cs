using System;
using System.Threading.Tasks;

namespace Howler.Blazor.Components
{
    /// <summary>
    /// See https://github.com/goldfire/howler.js
    /// </summary>
    public interface IHowlGlobal
    {
        /// <summary>
        /// Mute or unmute all sounds.
        /// </summary>
        /// <param name="muted">True to mute and false to unmute</param>
        ValueTask Mute(bool muted);

        ValueTask<string[]> GetCodecs();

        ValueTask<bool> IsCodecSupported(string? extension);
    }
}