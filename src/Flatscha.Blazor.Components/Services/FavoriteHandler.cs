using Flatscha.Blazor.Components.Contracts.Interfaces.Services;
using Flatscha.Blazor.Components.Extensions;
using Newtonsoft.Json;

namespace Flatscha.Blazor.Components.Services
{
    public class FavoriteHandler : IFavoriteHandler
    {
        public const string FavoriteKey = "Favorite";

        private readonly IProtecedLocalStorageHandler _protecedLocalStorageHandler;

        private Dictionary<string, object> _favoriteDictionary = [];

        public FavoriteHandler(IProtecedLocalStorageHandler protecedLocalStorageHandler)
        {
            this._protecedLocalStorageHandler = protecedLocalStorageHandler;
        }

        public bool IsFavorite<TItem, TFavoriteObject>(string favoriteKey, TItem item, Func<TItem, TFavoriteObject>? objectFunc = null)
            => objectFunc is null ? this.IsFavorite(favoriteKey, item) : this.IsFavorite(favoriteKey, objectFunc(item));

        private bool IsFavorite<TItem>(string favoriteKey, TItem item)
            => this._favoriteDictionary.TryGetValue(favoriteKey, out var value)
               && value.TryGetListFromJsonObject<TItem>(out var favorites)
               && favorites.Any(x => x.Equals(item) || (typeof(TItem) == typeof(object) && x.ToString() == item.ToString()));

        public async Task ToggleFavorite<TItem, TFavoriteObject>(string favoriteKey, TItem item, Func<TItem, TFavoriteObject>? objectFunc = null)
            => await (objectFunc is null ? this.ToggleFavorite(favoriteKey, item) : this.ToggleFavorite(favoriteKey, objectFunc(item)));

        private async Task ToggleFavorite<TItem>(string favoriteKey, TItem item)
        {
            List<TItem> favorites = [];

            if (this._favoriteDictionary.TryGetValue(favoriteKey, out var value))
            {
                value.TryGetListFromJsonObject(out favorites);
            }

            if (!favorites.Remove(item))
            {
                favorites.Add(item);
            }

            this._favoriteDictionary[favoriteKey] = favorites;

            await this.SaveFavList();
        }

        public async Task ReloadFavLists()
        {
            if (this._favoriteDictionary.Any()) { return; }

            var json = await this._protecedLocalStorageHandler.GetItemAsync(FavoriteKey);

            if (string.IsNullOrWhiteSpace(json)) { return; }

            this._favoriteDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json) ?? [];
        }

        public async Task SaveFavList() => await this._protecedLocalStorageHandler.SetItemObject(FavoriteKey, this._favoriteDictionary);
    }
}
