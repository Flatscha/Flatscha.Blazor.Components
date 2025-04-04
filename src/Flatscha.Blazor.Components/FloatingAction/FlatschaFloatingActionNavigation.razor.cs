namespace Flatscha.Blazor.Components.FloatingAction
{
    public partial class FlatschaFloatingActionNavigation
    {
        [Parameter] public bool Show { get; set; } = true;

        private bool _showActions = false;
        private bool _showNavigation => this.Show && this.Items.Any();

        public async Task Toggle()
        {
            this._showActions = !this._showActions;
            await this.InvokeAsync(StateHasChanged);
        }
    }
}