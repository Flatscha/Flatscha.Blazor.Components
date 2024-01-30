using Flatscha.Blazor.Components.Modal.Interface;
using Flatscha.Blazor.TestApp.Components.Modal;
using Microsoft.AspNetCore.Components;

namespace Flatscha.Blazor.TestApp.Components.Pages
{
    public partial class Home
    {
        [CascadingParameter] private ICascadingFlatschaModal _modal { get; set; }

        private string _icon = "house";
        private void ChangeIcon() => this._icon = this._icon == "house" ? "user" : "house";

        private async Task OpenModal()
        {
            var reference = this._modal.Open<TestModalContent>("Test Modal 2",
                        options: options =>
                        {
                            options.CustomOverlayClass = "test2";
                        });
            var reference2 = this._modal.Open<TestModalContent>("Test Modal");

            var res = await reference.Result;
            var res2 = await reference2.Result;
        }
    }
}