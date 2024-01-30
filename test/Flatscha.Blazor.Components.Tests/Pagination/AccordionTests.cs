
using Flatscha.Blazor.Components.Pagination;

namespace Flatscha.Blazor.Components.Tests.Pagination
{
    public class AccordionTests : TestContext
    {
        private readonly Fixture _fixture = new();

        public AccordionTests()
        {
        }

        [Fact]
        public void Create()
        {
            var accordion = RenderComponent<FlatschaAccordion>();

            Assert.Empty(accordion.Instance.Items);

            var accordionItemName1 = this._fixture.Create<string>();
            var accordionItem1 = RenderComponent<FlatschaAccordionItem>(parameters =>
            {
                parameters.Add(p => p.Name, accordionItemName1);
                parameters.AddCascadingValue(accordion.Instance);
            });

            var accordionItemName2 = this._fixture.Create<string>();
            var accordionItem2 = RenderComponent<FlatschaAccordionItem>(parameters =>
            {
                parameters.Add(p => p.Name, accordionItemName2);
                parameters.AddCascadingValue(accordion.Instance);
            });

            Assert.Equal(2, accordion.Instance.Items.Count);
        }

        [Fact]
        public void ActivateItem()
        {
            FlatschaAccordionItem activeItem = null;

            var accordion = RenderComponent<FlatschaAccordion>(parameters =>
            {
                parameters.Add(p => p.ActiveItemChanged, (item) => activeItem = item);
            });

            var accordionItemName1 = this._fixture.Create<string>();
            var accordionItem1 = RenderComponent<FlatschaAccordionItem>(parameters =>
            {
                parameters.Add(p => p.Name, accordionItemName1);
                parameters.AddCascadingValue(accordion.Instance);
            });

            var accordionItemName2 = this._fixture.Create<string>();
            var accordionItem2 = RenderComponent<FlatschaAccordionItem>(parameters =>
            {
                parameters.Add(p => p.Name, accordionItemName2);
                parameters.AddCascadingValue(accordion.Instance);
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
