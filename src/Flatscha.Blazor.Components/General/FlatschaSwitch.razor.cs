namespace Flatscha.Blazor.Components.General
{
    public partial class FlatschaSwitch
    {
        private bool _checked;

        [Parameter]
        public bool Checked
        {
            get => this._checked;
            set
            {
                if (this._checked == value) { return; }

                this._checked = value;
                this.CheckedChanged.InvokeAsync(value);
            }
        }

        [Parameter] public EventCallback<bool> CheckedChanged { get; set; }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        private async Task Clicked()
        {
            this.Checked = !this.Checked;
            await this.InvokeAsync(StateHasChanged);
        }

    }
}