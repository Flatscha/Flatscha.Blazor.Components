using Microsoft.Extensions.Logging;

namespace Flatscha.Blazor.Components.General
{
    public partial class FlatschaMessageCounter
    {
        [Parameter] public int Counter { get; set; } = 0;

        [Parameter] public int MaxMessageCounter { get; set; } = 0;

        [Parameter] public LogLevel MessageLevel { get; set; } = LogLevel.Information;
    }
}