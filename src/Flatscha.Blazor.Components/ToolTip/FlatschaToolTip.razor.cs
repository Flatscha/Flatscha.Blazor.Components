
namespace Flatscha.Blazor.Components.ToolTip
{
    public partial class FlatschaToolTip : ComponentBase
    {
        [CascadingParameter] public ICascadingFlatschaToolTip ToolTip { get; set; } = default!;

        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public RenderFragment? ToolTipContent { get; set; } = null;
        [Parameter] public string Text { get; set; } = null;

        private ElementReference _wrapper;

        private async Task ShowToolTip() => await this.ToolTip.ShowToolTip(this._wrapper, this.ToolTipContent, this.Text);

        private async Task HideToolTip() => await this.ToolTip.HideToolTip();
    }
}