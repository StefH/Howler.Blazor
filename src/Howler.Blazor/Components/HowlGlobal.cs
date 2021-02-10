using Howler.Blazor.Validation;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Howler.Blazor.Components
{
    public class HowlGlobal : IHowlGlobal
    {
        public const double MaxRate = 4.0;
        public const double MinRate = 0.25;

        private readonly IJSRuntime _runtime;

        public HowlGlobal(IJSRuntime runtime)
        {
            Guard.NotNull(runtime, nameof(runtime));

            _runtime = runtime;
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