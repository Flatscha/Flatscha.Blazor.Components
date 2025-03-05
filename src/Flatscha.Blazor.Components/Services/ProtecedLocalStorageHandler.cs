using Blazored.LocalStorage;
using Flatscha.Blazor.Components.Contracts.Interfaces.Services;

namespace Flatscha.Blazor.Components.Services
{
    public class ProtecedLocalStorageHandler : IProtecedLocalStorageHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public ProtecedLocalStorageHandler(ILocalStorageService localStorageService)
        {
            this._localStorageService = localStorageService;
        }

        public ValueTask SetItemAsync(string key, string value) => this._localStorageService.SetItemAsStringAsync(key, value);
        public ValueTask SetItemObject<T>(string key, T value) => this._localStorageService.SetItemAsync(key, value);
        public ValueTask<T?> GetItemObject<T>(string key) => this._localStorageService.GetItemAsync<T>(key);
        public ValueTask<string?> GetItemAsync(string key) => this._localStorageService.GetItemAsStringAsync(key);
        public ValueTask<IEnumerable<string>> GetKeys() => this._localStorageService.KeysAsync();
    }
}
