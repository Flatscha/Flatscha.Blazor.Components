using Flatscha.Blazor.Components.Contracts.Dto;
using Flatscha.Blazor.Components.Dialog;
using Flatscha.Blazor.Components.Modal.Interface;

namespace Flatscha.Blazor.Components.Extensions
{
    public static class FlatschaModalExtensions
    {
        public static async Task<EDialogResult> OpenDialog(this ICascadingFlatschaModal modalService, string message, EDialogButtons buttons)
        {
            var parameter = new ModalParameters
            {
                { nameof(FlatschaMessageDialog.Buttons), buttons }
            };

            var modal = modalService.Open<FlatschaMessageDialog>(message, parameter);
            var res = await modal.Result;

            if (res.IsCanceled) { return EDialogResult.None; }
            if (res.Data is not EDialogResult dialogResult) { throw new Exception($"Ergebnis von {nameof(FlatschaMessageDialog)} ist ungültig [{res.Result}, {res.Data}]"); }

            return dialogResult;
        }
    }
}
