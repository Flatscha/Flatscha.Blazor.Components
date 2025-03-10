using Microsoft.Extensions.Logging;

namespace Flatscha.Blazor.Components.Pagination
{
    public partial class FlatschaTabs<T>
    {
        [Parameter] public List<T> Values { get; set; }
        [Parameter] public T Value { get; set; }
        [Parameter] public EventCallback<T> ValueChanged { get; set; }
        [Parameter] public RenderFragment<T> ChildContent { get; set; }
        [Parameter] public Func<T, string> Header { get; set; } = x => x.ToString();

        [Parameter] public Func<T, int> MessageCounter { get; set; } = null;
        [Parameter] public int MaxMessageCounter { get; set; } = 0;
        [Parameter] public LogLevel MessageLevel { get; set; } = LogLevel.Information;

        protected override async Task OnParametersSetAsync()
        {
            if (this.Values is not null && this.Values.Any() && this.Value is null)
            {
                var firstTab = this.Values.FirstOrDefault();
                await this.TabClicked(firstTab);
            }
            await base.OnParametersSetAsync();
        }

        private async Task TabClicked(T page)
        {
            this.Value = page;
            await this.ValueChanged.InvokeAsync(page);
        }
    }
}