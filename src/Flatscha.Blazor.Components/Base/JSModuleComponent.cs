using Flatscha.Blazor.Components.Extensions;
using Microsoft.JSInterop;

namespace Flatscha.Blazor.Components.Base
{
    public abstract class JSModuleComponent : ComponentBase, IAsyncDisposable
    {
        [Inject] protected IJSRuntime _js { get; set; } = default!;

        protected IJSObjectReference _jsModule;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                this._jsModule = await this._js.GetJSModule(this);

                await this.OnAfterJSImport();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        protected virtual Task OnAfterJSImport() => Task.CompletedTask;

        public virtual async ValueTask DisposeAsync()
        {
            if (this._jsModule is null) { return; }

            try
            {
                await this._jsModule.DisposeAsync();
            }
            catch (JSDisconnectedException)
            {
            }
        }
    }
}
