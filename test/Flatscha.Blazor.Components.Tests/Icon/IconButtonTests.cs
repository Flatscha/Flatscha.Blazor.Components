using Flatscha.Blazor.Components.Icon;
using Flatscha.Blazor.Components.Tests.Base;

namespace Flatscha.Blazor.Components.Tests.Icon
{
    public class IconButtonTests : BaseComponentTest
    {
        public IconButtonTests()
        {
        }

        [Fact]
        public void Create()
        {
            var iconClass = "house";

            var cut = RenderComponent<FlatschaIconButton>(parameters =>
            {
                parameters.Add(p => p.FAClass, iconClass);
            });

            var wrapper = cut.Find("use");

            var href = wrapper.GetAttribute("href");

            Assert.NotNull(href);
            Assert.Contains(iconClass, href);
        }

        [Fact]
        public void Click()
        {
            var eventCalled = false;

            var cut = RenderComponent<FlatschaIconButton>(parameters =>
            {
                parameters.Add(p => p.FAClass, "house");
                parameters.Add(p => p.OnClick, () => eventCalled = true);
            });

            var button = cut.Find(".flatscha-icon-button");

            Assert.False(eventCalled);
            button.Click();
            Assert.True(eventCalled);
        }
    }
}
