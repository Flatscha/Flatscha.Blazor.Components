using Flatscha.Blazor.Components.Modal.Interface;
using Flatscha.Blazor.TestApp.Components.Modal;
using Microsoft.AspNetCore.Components;
using Flatscha.Blazor.Components.Extensions;

namespace Flatscha.Blazor.TestApp.Components.Pages
{
    public partial class Home
    {
        [CascadingParameter] private ICascadingFlatschaModal _modal { get; set; }

        private string _icon = "house";
        private void ChangeIcon() => this._icon = this._icon == "house" ? "user" : "house";

        private string StepError() => this._icon == "house" ? "UserIcon required" : null;

        private async Task OpenModal()
        {
            var reference = this._modal.Open<TestModalContent>("Test Modal 2",
                        options: options =>
                        {
                            options.CustomOverlayClass = "test2";
                        });

            var res = await reference.Result;
        }

        private async Task Load()
        {
            var modal = this._modal.OpenLoading();

            await Task.Delay(5000);

            await modal.Close();
        }
    }
}