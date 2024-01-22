namespace Flatscha.Blazor.Components.Pagination
{
    public partial class FlatschaAccordion
    {
        public List<FlatschaAccordionItem> Items = new();

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public FlatschaAccordionItem ActiveItem { get; set; }

        [Parameter] public EventCallback<FlatschaAccordionItem> ActiveItemChanged { get; set; }

        public async Task SetActiveItem(FlatschaAccordionItem item)
        {
            if (this.ActiveItem is null || this.ActiveItem != item)
            {
                this.ActiveItem = item;
                await this.ActiveItemChanged.InvokeAsync(item);
                await this.InvokeAsync(StateHasChanged);
            }
            else
            {
                this.ActiveItem = null;
                await this.ActiveItemChanged.InvokeAsync(null);
                await this.InvokeAsync(StateHasChanged);
            }
        }
    }
}