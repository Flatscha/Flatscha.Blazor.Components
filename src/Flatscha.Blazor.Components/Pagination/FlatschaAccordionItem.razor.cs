using Flatscha.Blazor.Components.Base.MultiChild;

namespace Flatscha.Blazor.Components.Pagination
{
    public partial class FlatschaAccordionItem : RegisterAtParentComponentBase<FlatschaAccordion, FlatschaAccordionItem>
    {
        [Parameter] public string Name { get; set; }
    }
}