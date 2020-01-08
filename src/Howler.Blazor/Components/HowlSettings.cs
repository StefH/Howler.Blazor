namespace Howler.Blazor.Components
{
    public class HowlSettings
    {
        /// <summary>
        /// The sources to the track(s) to be loaded for the sound (URLs or base64 data URIs).
        /// These should be in order of preference, howler.js will automatically load the first one that is compatible with the current browser.
        /// If your files have no extensions, you will need to explicitly specify the extension using the format property.
        /// </summary>
        public string[] Sources { get; set; }

        /// <summary>
        /// Set to true to force HTML5 Audio.
        /// This should be used for large audio files so that you don't have to wait for the full file to be downloaded and decoded before playing.
        /// </summary>
        public bool Html5 { get; set; }
    }
}