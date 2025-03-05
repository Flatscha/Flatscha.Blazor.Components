namespace Flatscha.Blazor.Components.Search
{
    public partial class SearchComponent<TItem>
    {
        [Parameter] public string Placeholder { get; set; } = null;
        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public RenderFragment<TItem> ItemTemplate { get; set; }
        [Parameter] public Func<IEnumerable<TItem>, string, IEnumerable<TItem>> SearchExpression { get; set; }
        [Parameter] public EventCallback<TItem> OnSelected { get; set; }

        private string _searchValue = string.Empty;
        private List<TItem> _result = [];

        protected override void OnParametersSet()
        {
            this.Search();
            base.OnParametersSet();
        }

        private async Task OnValueChanged(ChangeEventArgs e)
        {
            this._searchValue = e?.Value?.ToString();

            this.Search();
            await this.InvokeAsync(StateHasChanged);
        }

        private void Search()
        {
            if (string.IsNullOrEmpty(this._searchValue))
            {
                this._result = this.Items.ToList();
            }
            else
            {
                this._result = this.SearchExpression(this.Items, this._searchValue).ToList();
            }
        }
    }
}