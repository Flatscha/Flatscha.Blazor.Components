using Flatscha.Blazor.Components.Contracts.Dto;
using Flatscha.Blazor.Components.Dialog;
using Flatscha.Blazor.Components.Loading;
using Flatscha.Blazor.Components.Modal.Dto;
using Flatscha.Blazor.Components.Modal.Interface;

namespace Flatscha.Blazor.Components.Extensions
{
    public static class FlatschaModalExtensions
    {
        public static async Task<EDialogResult> OpenDialog(this ICascadingFlatschaModal modal, string message, EDialogButtons buttons)
        {
            var parameter = new ModalParameters
            {
                { nameof(FlatschaMessageDialog.Buttons), buttons }
            };

            var res = await modal.Open<FlatschaMessageDialog>(message, parameter).Result;

            if (res.IsCanceled) { return EDialogResult.None; }
            if (res.Data is not EDialogResult dialogResult) { throw new Exception($"Ergebnis von {nameof(FlatschaMessageDialog)} ist ungültig [{res.Result}, {res.Data}]"); }

            return dialogResult;
        }

        public static ModalReference OpenLoading(this ICascadingFlatschaModal modal)
            => modal.Open<FlatschaLoading>(null, null, options =>
                {
                    options.HideHeader = true;
                    options.HideCloseButton = true;
                });
    }
}
