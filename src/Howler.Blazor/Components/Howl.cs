using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Stef.Validation;

namespace Howler.Blazor.Components;

public partial class Howl : IHowl, IDisposable
{
    private readonly IJSRuntime _runtime;
    private readonly DotNetObjectReference<Howl> _dotNetObjectReference;
    private bool _isDisposed;

    public TimeSpan TotalTime { get; private set; } = TimeSpan.Zero;

    public Howl(IJSRuntime runtime)
    {
        _runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
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
        Guard.HasNoNulls(options.Sources);
        Guard.Condition(options.Sources, sources => sources.Length > 0);

        return _runtime.InvokeAsync<int>("howl.play", _dotNetObjectReference, options);
    }

    public ValueTask Play(int soundId)
    {
        return _runtime.InvokeVoidAsync("howl.playSound", soundId);
    }

    public ValueTask Stop(int soundId)
    {
        return _runtime.InvokeVoidAsync("howl.stop", soundId);
    }

    public ValueTask Pause(int soundId)
    {
        return _runtime.InvokeVoidAsync("howl.pause", soundId);
    }

    public ValueTask Seek(int soundId, TimeSpan position)
    {
        return _runtime.InvokeVoidAsync("howl.seek", soundId, position.TotalSeconds);
    }

    public ValueTask Rate(int soundId, double rate)
    {
        return _runtime.InvokeVoidAsync("howl.rate", soundId, rate);
    }

    public ValueTask Load(int soundId)
    {
        return _runtime.InvokeVoidAsync("howl.load", soundId);
    }

    public ValueTask Unload(int soundId)
    {
        return _runtime.InvokeVoidAsync("howl.unload", soundId);
    }

    public ValueTask<bool> IsPlaying(int soundId)
    {
        return _runtime.InvokeAsync<bool>("howl.getIsPlaying", soundId);
    }

    public async ValueTask<double> GetRate(int soundId)
    {
        return await _runtime.InvokeAsync<double>("howl.getRate", soundId);
    }

    public async ValueTask<TimeSpan> GetCurrentTime(int soundId)
    {
        var timeInSeconds = await _runtime.InvokeAsync<double?>("howl.getCurrentTime", soundId);
        return ConvertToTimeSpan(timeInSeconds);
    }

    public async ValueTask<TimeSpan> GetTotalTime(int soundId)
    {
        var timeInSeconds = await _runtime.InvokeAsync<double?>("howl.getTotalTime", soundId);
        return ConvertToTimeSpan(timeInSeconds);
    }

    public ValueTask<double> Volume(int soundId, double volume)
    {
        return _runtime.InvokeAsync<double>("howl.volume", soundId, volume);
    }

    private static TimeSpan ConvertToTimeSpan(double? value)
    {
        return value is null ? TimeSpan.Zero : TimeSpan.FromSeconds(value.Value);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            if (disposing)
            {
                _runtime.InvokeVoidAsync("howl.destroy");

                _dotNetObjectReference.Dispose();
            }

            _isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}