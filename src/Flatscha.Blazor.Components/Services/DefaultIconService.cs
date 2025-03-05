using Flatscha.Blazor.Components.Contracts.Interfaces.Services;

namespace Flatscha.Blazor.Components.Services
{
    public class DefaultIconService : IDefaultIconService
    {
        public EIconStyle Style { get; }

        public DefaultIconService(EIconStyle style)
        {
            this.Style = style;
        }
    }
}
