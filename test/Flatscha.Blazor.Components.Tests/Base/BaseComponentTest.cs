using Flatscha.Blazor.Components.Contracts.Enum;
using Flatscha.Blazor.Components.Contracts.Interfaces.Services;
using Flatscha.Blazor.Components.Services;

namespace Flatscha.Blazor.Components.Tests.Base
{
    public abstract class BaseComponentTest : TestContext
    {
        protected readonly Fixture _fixture = new();

        protected BaseComponentTest()
        {
            Services.AddSingleton<IDefaultIconService>(sp => new DefaultIconService(EIconStyle.Solid));
        }
    }
}
