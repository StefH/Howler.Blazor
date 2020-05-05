# Howler.Blazor

A Blazor JSInterop wrapper for [Howler.js](https://howlerjs.com/).

[![NuGet: Howler.Blazor](https://buildstats.info/nuget/Howler.Blazor)](https://www.nuget.org/packages/Howler.Blazor)

## Usage

### Install the NuGet

```
PM> Install-Package Howler.Blazor
```

### Add the required dependency injections
``` diff
public void ConfigureServices(IServiceCollection services)
{
+    services.AddScoped<IHowl, Howl>();
+    services.AddScoped<IHowlGlobal, HowlGlobal>();
}
```

### Add the required javascripts to _Host.cshtml
``` diff
<head>
+    <script src="https://cdnjs.cloudflare.com/ajax/libs/howler/2.1.2/howler.core.min.js" integrity="sha256-q2vnVvwrx3RbYXPyAwx7c2npmULQg2VdCXBoJ5+iigs=" crossorigin="anonymous"></script>
+    <script src="_content/Howler.Blazor/JsInteropHowl.js"></script>
</head>
```

### Use the player
``` html
@page "/example"
@using Howler.Blazor.Components

<!-- Inject services -->
@inject IHowl Howl
@inject IHowlGlobal HowlGlobal

<div>
    <button class="btn btn-primary oi oi-media-play" @onclick="Play"></button>
    <button class="btn btn-primary oi oi-media-stop" @onclick="Stop"></button>
    <button class="btn btn-primary oi oi-media-pause" @onclick="Pause"></button>
    <pre>TotalTime : @TotalTime</pre>
    <pre>State : @State</pre>
    <pre>Supported Codes : @SupportedCodes</pre>
</div>

@code {
    protected TimeSpan TotalTime;
    protected string State = "-";
    protected string SupportedCodes;

    protected override async Task OnInitializedAsync()
    {
        // Display all supported codecs
        var codecs = await HowlGlobal.GetCodecs();
        SupportedCodes = string.Join(", ", codecs);

        // Register callbacks
        Howl.OnPlay += e =>
        {
            State = "Playing";
            TotalTime = e.TotalTime;

            StateHasChanged();
        };

        Howl.OnStop += e =>
        {
            State = "Stopped";

            StateHasChanged();
        };

        Howl.OnPause += e =>
        {
            State = "Paused";

            StateHasChanged();
        };
    }

    protected async Task Play()
    {
        await Howl.Play("https://www.healingfrequenciesmusic.com/wp-content/uploads/2015/03/Love-Abounds-Sample.mp3?_=1");
    }

    protected async Task Stop()
    {
        await Howl.Stop();
    }

    protected async Task Pause()
    {
        await Howl.Pause();
    }
}
```

### Live Demo


### Example Page
![Blazor-WebDAV-AudioPlayer](https://raw.githubusercontent.com/StefH/WebDAV-AudioPlayer/master/resources/example.png "example")
