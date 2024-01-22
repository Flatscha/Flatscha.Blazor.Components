namespace Flatscha.Blazor.Components.Contracts.Interfaces.Components
{
    public interface ICascadingFlatschaToolTip
    {
        Task ShowToolTip(ElementReference reference, RenderFragment? content = null, string? text = null);
        Task HideToolTip();
    }
}
