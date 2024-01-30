using Flatscha.Blazor.Components.Contracts.Dto;
using Flatscha.Blazor.Components.Modal.Dto;

namespace Flatscha.Blazor.Components.Modal.Interface
{
    public interface ICascadingFlatschaModal
    {
        ModalReference Open<T>(string title, ModalParameters? parameters = null, Action<ModalOptions>? options = null) where T : IComponent;
        ModalReference Open(Type componentType, string title, ModalParameters? parameters = null, Action<ModalOptions>? options = null);
        Task Close(ModalReference? modalReference, ModalResult? result = null);
        Task Close(FlatschaModal? modal, ModalResult? result = null);
        Task Close(ModalResult? result = null);
        Task CloseAll(ModalResult? result = null);
    }
}
