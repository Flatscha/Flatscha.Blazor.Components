using Flatscha.Blazor.Components.Contracts.Dto;
using Flatscha.Blazor.Components.Contracts.Dto.Dialog;
using Flatscha.Blazor.Components.Modal.Interface;

namespace Flatscha.Blazor.Components.Dialog
{
    public partial class FlatschaMessageDialog
    {
        [CascadingParameter] private ICascadingFlatschaModal _modal { get; set; } = default!;

        [Parameter] public EDialogButtons Buttons { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }

        private DialogButtonParameter _dialogButtons = new();

        protected override void OnParametersSet()
        {
            this._dialogButtons = new(this.Buttons);

            base.OnParametersSet();
        }

        private async Task Submit(EDialogResult result) => await this._modal.Close(ModalResult.Ok(result));

    }
}