
namespace Flatscha.Blazor.Components.Icon
{
    public partial class FlatschaIconText
    {
        [Parameter, EditorRequired] public string FAClass { get; set; }

        [Parameter] public string Text { get; set; } = null;

        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}