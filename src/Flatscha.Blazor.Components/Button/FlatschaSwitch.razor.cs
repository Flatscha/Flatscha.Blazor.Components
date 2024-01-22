namespace Flatscha.Blazor.Components.Button
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

        [Parameter] public string Text { get; set; }

        private async Task Clicked()
        {
            this.Checked = !this.Checked;
            await this.InvokeAsync(StateHasChanged);
        }

    }
}