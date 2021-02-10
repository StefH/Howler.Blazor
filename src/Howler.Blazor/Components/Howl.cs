using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Stef.Validation;

namespace Howler.Blazor.Components
{
    public partial class Howl : IHowl
    {
        private readonly IJSRuntime _runtime;
        private readonly DotNetObjectReference<Howl> _dotNetObjectReference;

        public TimeSpan TotalTime { get; private set; } = TimeSpan.Zero;

        public Howl(IJSRuntime runtime)
        {
            _runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));

            _runtime = runtime;
            _dotNetObjectReference = DotNetObjectReference.Create(this);
        }

        public ValueTask<int> Play(params Uri[] locations)
        {
            Guard.HasNoNulls(locations, nameof(locations));

            return Play(locations.Select(l => l.ToString()).ToArray());
        }

        public ValueTask<int> Play(params string[] sources)
        {
            Guard.HasNoNulls(sources, nameof(sources));

            var options = new HowlOptions
            {
                Sources = sources
            };

            return Play(options);
        }

        public ValueTask<int> Play(byte[] audio, string mimetype)
        {
            Guard.NotNull(audio, nameof(audio));
            Guard.NotNullOrEmpty(mimetype, nameof(mimetype));

            // http://www.iandevlin.com/blog/2012/09/html5/html5-media-and-data-uri/
            var audioAsBase64 = Convert.ToBase64String(audio);
            string html5AudioUrl = $"data:{mimetype};base64,{audioAsBase64}";

            var options = new HowlOptions
            {
                Sources = new[] { html5AudioUrl }
            };

            return _runtime.InvokeAsync<int>("howl.play", _dotNetObjectReference, options);
        }

        public ValueTask<int> Play(HowlOptions options)
        {
            Guard.HasNoNulls(options.Sources, nameof(options.Sources));
            Guard.Condition(options.Sources, sources => sources.Length > 0, nameof(options.Sources));
            // Guard.Condition(options.Formats, formats => formats == null || formats.Length == options.Sources.Length, nameof(options.Formats));

            return _runtime.InvokeAsync<int>("howl.play", _dotNetObjectReference, options);
        }

        public ValueTask Stop()
        {
            return _runtime.InvokeVoidAsync("howl.stop");
        }

        public ValueTask Pause(int? soundId)
        {
            return _runtime.InvokeVoidAsync("howl.pause", soundId);
        }

        public ValueTask Seek(TimeSpan position)
        {
            return _runtime.InvokeVoidAsync("howl.seek", position.TotalSeconds);
        }

        public ValueTask Rate(double rate)
        {
            return _runtime.InvokeVoidAsync("howl.rate", rate);
        }

        public ValueTask Load()
        {
            return _runtime.InvokeVoidAsync("howl.load");
        }

        public ValueTask Unload()
        {
            return _runtime.InvokeVoidAsync("howl.unload");
        }

        public ValueTask<bool> IsPlaying()
        {
            return _runtime.InvokeAsync<bool>("howl.getIsPlaying", _dotNetObjectReference);
        }

        public async ValueTask<double> GetRate()
        {
            return await _runtime.InvokeAsync<double>("howl.getRate");
        }

        public async ValueTask<TimeSpan> GetCurrentTime()
        {
            var timeInSeconds = await _runtime.InvokeAsync<double?>("howl.getCurrentTime");
            return ConvertToTimeSpan(timeInSeconds);
        }

        public async ValueTask<TimeSpan> GetTotalTime()
        {
            var timeInSeconds = await _runtime.InvokeAsync<double?>("howl.getTotalTime");
            return ConvertToTimeSpan(timeInSeconds);
        }

        private static TimeSpan ConvertToTimeSpan(double? value)
        {
            return value is null ? TimeSpan.Zero : TimeSpan.FromSeconds(value.Value);
        }
    }
}