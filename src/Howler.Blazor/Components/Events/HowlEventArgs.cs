using System;

namespace Howler.Blazor.Components.Events
{
    public class HowlEventArgs : EventArgs
    {
        /// <summary>
        /// The ID of the sound.
        /// </summary>
        public int SoundId { get; set; }
    }
}