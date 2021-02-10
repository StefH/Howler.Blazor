using System;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Howler.Blazor.Components
{
    public class HowlGlobal : IHowlGlobal
    {
        public const double MaxRate = 4.0;
        public const double MinRate = 0.25;

        private readonly IJSRuntime _runtime;

        public HowlGlobal(IJSRuntime runtime)
        {
            _runtime = runtime ?? throw new ArgumentNullException(nameof(runtime));
        }

        public ValueTask Mute(bool muted)
        {
            return _runtime.InvokeVoidAsync("howler.mute", muted);
        }

        public ValueTask<string[]> GetCodecs()
        {
            return _runtime.InvokeAsync<string[]>("howler.getCodecs");
        }

        public ValueTask<bool> IsCodecSupported(string? extension)
        {
            return _runtime.InvokeAsync<bool>("howler.isCodecSupported", extension);
        }
    }
}