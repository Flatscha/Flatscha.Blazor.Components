namespace Flatscha.Blazor.Components.Search
{
    public partial class SearchItemComponent
    {
        [Parameter] public string Title { get; set; }
        [Parameter] public string Text { get; set; }
        [Parameter] public string FaClass { get; set; }
        [Parameter] public EventCallback OnClick { get; set; }
    }
}