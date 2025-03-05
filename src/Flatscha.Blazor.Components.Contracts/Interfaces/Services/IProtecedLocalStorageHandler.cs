namespace Flatscha.Blazor.Components.Contracts.Interfaces.Services
{
    public interface IProtecedLocalStorageHandler
    {
        ValueTask<string?> GetItemAsync(string key);
        ValueTask SetItemAsync(string key, string value);

        ValueTask<T?> GetItemObject<T>(string key);
        ValueTask SetItemObject<T>(string key, T data);
        ValueTask<IEnumerable<string>> GetKeys();
    }
}
