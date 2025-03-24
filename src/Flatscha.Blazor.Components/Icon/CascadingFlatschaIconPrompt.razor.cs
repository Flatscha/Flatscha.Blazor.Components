using Flatscha.Blazor.Components.Base;
using Microsoft.JSInterop;

namespace Flatscha.Blazor.Components.Icon
{
    public partial class CascadingFlatschaIconPrompt : JSModuleComponent
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }

        private string _faClass = "check";
        private ElementReference _wrapper;
        private string _colorClass;

        public async Task Show(bool successful) => await this.Show(successful ? "check" : "xmark", successful ? null : "unsuccessful");

        public async Task Show(string faClass, string? colorClass = null)
        {
            if (string.IsNullOrWhiteSpace(faClass)) { return; }

            this._colorClass = colorClass ?? "successful";

            this._faClass = null;
            await this.InvokeAsync(StateHasChanged);
            await Task.Delay(10);
            this._faClass = faClass;
            await this.InvokeAsync(StateHasChanged);

            await this._jsModule.InvokeVoidAsync("flipIconPrompt", this._wrapper);
        }
    }
}