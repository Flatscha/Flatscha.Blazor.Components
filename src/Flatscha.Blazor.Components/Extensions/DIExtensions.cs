using Blazored.LocalStorage;
using Flatscha.Blazor.Components.Contracts.Interfaces.Services;
using Flatscha.Blazor.Components.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Flatscha.Blazor.Components.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddFlatschaUtilities(this IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();

            services.AddScoped<IProtecedLocalStorageHandler, ProtecedLocalStorageHandler>();
            services.AddScoped<IFavoriteHandler, FavoriteHandler>();

            return services;
        }
    }
}
