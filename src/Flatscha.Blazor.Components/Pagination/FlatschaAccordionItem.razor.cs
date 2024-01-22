namespace Flatscha.Blazor.Components.Pagination
{
    public partial class FlatschaAccordionItem
    {
        [CascadingParameter] public FlatschaAccordion Parent { get; set; }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public string Name { get; set; }

        protected override void OnInitialized()
        {
            this.Parent.Items.Add(this);
            base.OnInitialized();
        }

        private async Task SetActive()
        {
            await this.Parent.SetActiveItem(this);
            await this.InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            this.Parent.Items.Remove(this);
        }
    }
}