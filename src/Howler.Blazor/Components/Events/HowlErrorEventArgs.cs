namespace Howler.Blazor.Components.Events
{
    public class HowlErrorEventArgs
    {
        /// <summary>
        /// The optional ID of the sound.
        /// </summary>
        public int? SoundId { get; set; }

        /// <summary>
        /// The error message/code.
        /// </summary>
        public string Error { get; set; }
    }
}