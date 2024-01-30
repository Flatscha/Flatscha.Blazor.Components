
using Flatscha.Blazor.Components.Pagination;
using Flatscha.Blazor.Components.Tests.Base;

namespace Flatscha.Blazor.Components.Tests.Pagination
{
    public class AccordionTests : BaseMultiChildComponentTest<FlatschaAccordionItem, FlatschaAccordion>
    {
        public AccordionTests()
        {
        }

        [Fact]
        public void ActivateItem()
        {
            FlatschaAccordionItem activeItem = null;

            var (accordion, accordionItem1, accordionItem2) = this.RenderParentWithChilds(parameters =>
            {
                parameters.Add(p => p.ActiveItemChanged, (item) => activeItem = item);
            });

            var item1 = accordionItem1.Find(".flatscha-accordion-item-header");
            var item2 = accordionItem2.Find(".flatscha-accordion-item-header");

            item1.Click();
            Assert.NotNull(activeItem);
            Assert.Equal(accordionItem1.Instance, activeItem);

            item2.Click();
            Assert.NotNull(activeItem);
            Assert.Equal(accordionItem2.Instance, activeItem);
        }
    }
}
