using Microsoft.JSInterop;

namespace Flatscha.Blazor.Components.ToolTip
{
    public partial class CascadingFlatschaToolTip : ComponentBase, IAsyncDisposable, ICascadingFlatschaToolTip
    {
        [Inject] private IJSRuntime _js { get; set; } = default!;

        [Parameter] public RenderFragment? ChildContent { get; set; }

        private RenderFragment? _toolTipContent = null;
        private string? _text = null;

        private IJSObjectReference _jsModule;

        private ElementReference? _wrapper;
        private ElementReference _toolTip;

        private bool _showToolTip = false;
        private bool _correctToolTipPosition = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                this._jsModule = await this._js.InvokeAsync<IJSObjectReference>("import", $"./_content/{this.GetType().Assembly.GetName().Name}/ToolTip/{nameof(CascadingFlatschaToolTip)}.razor.js");
            }

            if (this._correctToolTipPosition && this._wrapper.HasValue)
            {
                this._correctToolTipPosition = false;
                await this._jsModule.InvokeVoidAsync("positionFlatschaToolTip", this._wrapper.Value, this._toolTip);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task ShowToolTip(ElementReference reference, RenderFragment? content = null, string? text = null)
        {
            this._wrapper = reference;
            this._toolTipContent = content;
            this._text = text;

            this._showToolTip = true;
            this._correctToolTipPosition = true;

            await this.InvokeAsync(StateHasChanged);
        }

        public async Task HideToolTip()
        {
            this._wrapper = null;

            this._showToolTip = false;
            this._correctToolTipPosition = false;

            await this.InvokeAsync(StateHasChanged);
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (this._jsModule is not null)
                {
                    await this._jsModule.DisposeAsync();
                }
            }
            catch (JSDisconnectedException)
            {
            }
        }
    }
}