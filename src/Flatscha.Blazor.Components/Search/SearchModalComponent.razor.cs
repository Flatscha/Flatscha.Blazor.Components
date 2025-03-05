using Flatscha.Blazor.Components.Contracts.Dto;
using Flatscha.Blazor.Components.Contracts.Interfaces.Services;
using Flatscha.Blazor.Components.Modal;

namespace Flatscha.Blazor.Components.Search
{
    public partial class SearchModalComponent<TItem, TFavoriteObject>
    {
        [Inject] private IFavoriteHandler _favoriteHandler { get; set; }

        [CascadingParameter] private FlatschaModal _modal { get; set; } = default!;

        [Parameter] public IEnumerable<TItem> Items { get; set; }
        [Parameter] public Func<TItem, string> Name { get; set; }
        [Parameter] public Func<TItem, string>? Sub { get; set; } = null;
        [Parameter] public string? FavoriteKey { get; set; } = null;
        [Parameter] public Func<TItem, TFavoriteObject>? FavoriteObject { get; set; } = null;
        [Parameter] public string Placeholder { get; set; } = null;
        [Parameter] public bool DisableOrder { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await this._favoriteHandler.ReloadFavLists();

            await base.OnInitializedAsync();
        }

        private string GetName(TItem item) => this.Name is null ? item?.ToString() : this.Name(item);

        private IEnumerable<TItem> Search(IEnumerable<TItem> items, string searchValue)
            => items.Where(item => this.GetName(item)?.Contains(searchValue, StringComparison.CurrentCultureIgnoreCase) ?? false
            || (this.Sub != null && (this.Sub(item)?.Contains(searchValue, StringComparison.CurrentCultureIgnoreCase) ?? false)));

        private Task OnSelected(TItem value) => this._modal.CloseAsync(ModalResult.Ok(value));

        private bool IsFavorite(TItem item) => this.FavoriteKey is not null && this._favoriteHandler.IsFavorite(this.FavoriteKey, item, this.FavoriteObject);
        private async Task ToggleFavorite(TItem item) => await this._favoriteHandler.ToggleFavorite(this.FavoriteKey!, item, this.FavoriteObject);
    }
}