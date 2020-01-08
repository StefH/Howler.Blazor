namespace Howler.Blazor.Components.Events
{
    public class HowlErrorEventArgs : HowlEventArgs
    {
        /// <summary>
        /// The error message/code.
        /// </summary>
        public string Error { get; set; }
    }
}