using Flatscha.Blazor.Components.General;

namespace Flatscha.Blazor.Components.Tests.General
{
    public class SwitchTests : TestContext
    {
        private readonly Fixture _fixture = new();

        public SwitchTests()
        {
        }

        [Fact]
        public void Create()
        {
            var text = _fixture.Create<string>();

            var cut = RenderComponent<FlatschaSwitch>(parameters =>
            {
                parameters.Add(p => p.Text, text);
            });

            var wrapper = cut.Find(".flatscha-toggle-switch");

            var html = wrapper.InnerHtml;

            Assert.NotNull(html);
            Assert.Contains(text, html);
        }

        [Fact]
        public void Click()
        {
            var eventCalled = false;

            var cut = RenderComponent<FlatschaSwitch>(parameters =>
            {
                parameters.Add(p => p.Checked, true);
                parameters.Add(p => p.CheckedChanged, (bool value) =>
                {
                    eventCalled = true;
                    Assert.False(value);
                });
            });

            var wrapper = cut.Find(".flatscha-toggle-switch");

            Assert.False(eventCalled);
            wrapper.Click();
            Assert.True(eventCalled);
        }
    }
}
