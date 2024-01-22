
namespace Flatscha.Blazor.Components.Icon
{
    public partial class FlatschaIconButton
    {
        [Parameter, EditorRequired] public string FAClass { get; set; }

        [Parameter] public EventCallback OnClick { get; set; }

        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> AllOtherAttributes { get; set; }

    }
}