namespace Flatscha.Blazor.Components.Contracts.Interfaces.Services
{
    public interface IFavoriteHandler
    {
        bool IsFavorite<TItem, TFavoriteObject>(string favoriteKey, TItem item, Func<TItem, TFavoriteObject>? objectFunc = null);
        Task ReloadFavLists();
        Task ToggleFavorite<TItem, TFavoriteObject>(string favoriteKey, TItem item, Func<TItem, TFavoriteObject>? objectFunc = null);
    }
}
