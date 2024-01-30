using Flatscha.Blazor.Components.Base.MultiChild;
using Flatscha.Blazor.Components.Extensions;
using Flatscha.Blazor.Components.Modal.Interface;

namespace Flatscha.Blazor.Components.Wizard
{
    public partial class FlatschaWizard : MultiChildComponentBase<FlatschaWizardStep, FlatschaWizard>
    {
        private int _currentIndex => this.ActiveItem is null ? 0 : this.Items.IndexOf(this.ActiveItem);

        [CascadingParameter] private ICascadingFlatschaModal _modalService { get; set; } = default!;

        [Parameter] public Func<Task<string>> Submit { get; set; }
        [Parameter] public string SubmitText { get; set; } = "Submit";
        [Parameter] public string NextText { get; set; } = "Next";
        [Parameter] public string BackText { get; set; } = "Back";

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await base.SetActiveItem(this.Items.FirstOrDefault());
                await this.InvokeAsync(StateHasChanged);
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        public override async Task SetActiveItem(FlatschaWizardStep? item)
        {
            if (await this.HasStepError(this.ActiveItem)) { return; }

            await base.SetActiveItem(item);
        }

        private async Task<bool> HasStepError(FlatschaWizardStep? item)
        {
            var error = item?.StepError?.Invoke();
            if (!string.IsNullOrWhiteSpace(error))
            {
                await this._modalService.OpenDialog(error, EDialogButtons.OK);
                return true;
            }

            return false;
        }

        private async Task Next()
        {
            if (this._currentIndex < this.Items.Count)
            {
                await this.SetActiveItem(this.Items[this._currentIndex + 1]);
            }
        }

        private async Task Back()
        {
            if (this._currentIndex > 0)
            {
                await this.SetActiveItem(this.Items[this._currentIndex - 1]);
            }
        }

        private async Task SubmitForm()
        {
            foreach (var step in this.Items)
            {
                if (await this.HasStepError(step)) { return; }
            }

            if (this.Submit is null) { return; }

            var error = await this.Submit.Invoke();

            if (!string.IsNullOrWhiteSpace(error))
            {
                await this._modalService.OpenDialog(error, EDialogButtons.OK);
            }
        }

        public async Task ForceStateHasChanged() => await this.InvokeAsync(StateHasChanged);
    }
}