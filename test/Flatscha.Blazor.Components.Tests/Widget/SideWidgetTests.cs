using Flatscha.Blazor.Components.Tests.Base;
using Flatscha.Blazor.Components.Widget;

namespace Flatscha.Blazor.Components.Tests.Widget
{
    public class SideWidgetTests : BaseMultiChildComponentTest<FlatschaSideWidget, CascadingFlatschaSideWidget>
    {
        private const string ClickElementSelector = ".flatscha-side-widget .flatscha-icon-button";

        public SideWidgetTests()
        {
        }

        [Fact]
        public void Activate()
        {
            FlatschaSideWidget activeStep = null;

            var (widgetContainer, widget1, widget2) = this.RenderParentWithChilds(
                p => { p.Add(p => p.ActiveItemChanged, (item) => activeStep = item); });

            widgetContainer.Render();

            widget1.Find(ClickElementSelector).Click();
            Assert.NotNull(activeStep);
            Assert.Equal(widget1.Instance, activeStep);

            widget2.Find(ClickElementSelector).Click();
            Assert.NotNull(activeStep);
            Assert.Equal(widget2.Instance, activeStep);
        }
    }
}
