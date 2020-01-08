using System;

namespace Howler.Blazor.Components.Events
{
    public class HowlPlayEventArgs : HowlEventArgs
    {
        public TimeSpan TotalTime { get; set; }
    }
}