using Flatscha.Blazor.Components.Base;
using Microsoft.JSInterop;

namespace Flatscha.Blazor.Components.ToolTip
{
    public partial class CascadingFlatschaToolTip : JSModuleComponent, IAsyncDisposable, ICascadingFlatschaToolTip
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }

        private RenderFragment? _toolTipContent = null;
        private string? _text = null;

        private ElementReference? _wrapper;
        private ElementReference _toolTip;

        private bool _showToolTip = false;
        private bool _correctToolTipPosition = false;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (this._correctToolTipPosition && this._wrapper.HasValue)
            {
                this._correctToolTipPosition = false;
                await this._jsModule.InvokeVoidAsync("positionFlatschaToolTip", this._wrapper.Value, this._toolTip);
            }
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
    }
}