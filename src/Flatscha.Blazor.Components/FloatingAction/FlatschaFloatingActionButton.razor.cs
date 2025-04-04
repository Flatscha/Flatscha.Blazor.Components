namespace Flatscha.Blazor.Components.FloatingAction
{
    public partial class FlatschaFloatingActionButton
    {

        [Parameter] public string Title { get; set; }

        [Parameter] public EventCallback OnClick { get; set; }

        [Parameter] public string FAClass { get; set; }

        [Parameter] public string CssClass { get; set; }
    }
}