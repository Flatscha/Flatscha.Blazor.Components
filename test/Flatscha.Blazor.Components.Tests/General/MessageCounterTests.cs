using Flatscha.Blazor.Components.General;
using Flatscha.Blazor.Components.Tests.Base;
using Microsoft.Extensions.Logging;

namespace Flatscha.Blazor.Components.Tests.General
{
    public class MessageCounterTests : BaseComponentTest
    {
        public MessageCounterTests()
        {
        }

        [Fact]
        public void Create()
        {
            var cut = RenderComponent<FlatschaMessageCounter>(parameters =>
            {
                parameters.Add(x => x.Counter, 23);
                parameters.Add(x => x.MessageLevel, LogLevel.Error);
            });

            var wrapper = cut.Find(".flatscha-message-counter");

            Assert.Contains("error", wrapper.ClassName);

            var html = wrapper.InnerHtml;

            Assert.NotNull(html);
            Assert.Equal("23", html);
        }

        [Fact]
        public void MaxMessageCounter()
        {
            var cut = RenderComponent<FlatschaMessageCounter>(parameters =>
            {
                parameters.Add(x => x.Counter, 23);
                parameters.Add(x => x.MaxMessageCounter, 20);
            });

            var wrapper = cut.Find(".flatscha-message-counter");

            var html = wrapper.InnerHtml;

            Assert.NotNull(html);
            Assert.Equal("20+", html);
        }
    }
}
