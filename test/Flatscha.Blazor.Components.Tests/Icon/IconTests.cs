using Flatscha.Blazor.Components.Icon;
using Flatscha.Blazor.Components.Tests.Base;

namespace Flatscha.Blazor.Components.Tests.Icon
{
    public class IconTests : BaseComponentTest
    {
        public IconTests()
        {
        }

        [Fact]
        public void Create()
        {
            var iconClass = "house";

            var cut = RenderComponent<FlatschaIcon>(parameters =>
            {
                parameters.Add(p => p.FAClass, iconClass);
            });

            var wrapper = cut.Find("use");

            var href = wrapper.GetAttribute("href");

            Assert.NotNull(href);
            Assert.Contains(iconClass, href);
        }
    }
}
