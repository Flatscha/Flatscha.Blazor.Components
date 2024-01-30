using Flatscha.Blazor.Components.Contracts.Dto;
using Flatscha.Blazor.Components.Modal.Interface;

namespace Flatscha.Blazor.Components.Modal
{
    public partial class FlatschaModal : ComponentBase
    {
        [CascadingParameter] private ICascadingFlatschaModal _modal { get; set; } = default!;

        [Parameter] public string Title { get; set; }
        [Parameter] public ModalOptions Options { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }

        public async Task CloseAsync(ModalResult? result = null) => await this._modal.Close(this, result);

        public async Task CancelAsync() => await this.CloseAsync(new(EDialogResult.Cancel));
    }
}