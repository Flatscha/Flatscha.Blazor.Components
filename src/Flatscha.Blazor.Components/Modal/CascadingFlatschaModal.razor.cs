using Flatscha.Blazor.Components.Contracts.Dto;
using Flatscha.Blazor.Components.Modal.Dto;
using Flatscha.Blazor.Components.Modal.Interface;
using Microsoft.Extensions.Options;

namespace Flatscha.Blazor.Components.Modal
{
    public partial class CascadingFlatschaModal : ICascadingFlatschaModal
    {
        [Inject] private IOptionsMonitor<ModalOptions> _optionsMonitor { get; set; }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        private readonly List<ModalReference> _modals = [];

        public ModalReference Open<T>(string title, ModalParameters? parameters = null, Action<ModalOptions>? options = null) where T : IComponent
            => this.Open(typeof(T), title, parameters, options);

        public ModalReference Open(Type componentType, string title, ModalParameters? parameters = null, Action<ModalOptions>? options = null)
        {
            if (!typeof(IComponent).IsAssignableFrom(componentType)) { throw new ArgumentException($"{componentType.FullName} has to be a Component"); }

            ModalReference? modalReference = null;

            RenderFragment content = builder =>
            {
                var i = 0;
                builder.OpenComponent(i++, componentType);
                if (parameters is not null)
                {
                    foreach (var parameter in parameters.Parameter)
                    {
                        builder.AddAttribute(i++, parameter.Key, parameter.Value);
                    }
                }
                builder.CloseComponent();
            };

            var currentOption = this._optionsMonitor?.CurrentValue ?? new();
            options?.Invoke(currentOption);

            RenderFragment modal = builder =>
            {
                var i = 0;
                builder.OpenComponent<FlatschaModal>(i++);
                builder.AddAttribute(i++, nameof(FlatschaModal.Title), title);
                builder.AddAttribute(i++, nameof(FlatschaModal.Options), currentOption);
                builder.AddAttribute(i++, nameof(FlatschaModal.ChildContent), content);
                builder.AddComponentReferenceCapture(i++, compRef => modalReference!.Instance = (FlatschaModal)compRef);
                builder.CloseComponent();
            };

            modalReference = new ModalReference(modal, this);

            this._modals.Add(modalReference);

            Task.Run(async () => await this.InvokeAsync(StateHasChanged));

            return modalReference;
        }

        public async Task Close(ModalResult? result = null)
        {
            var modalReference = this._modals.LastOrDefault();

            await this.Close(modalReference, result);
        }

        public async Task Close(FlatschaModal? modal, ModalResult? result = null)
        {
            if (modal is null) { return; }

            var modalReference = this._modals.FirstOrDefault(x => x.Instance == modal);

            await this.Close(modalReference, result);
        }

        public async Task Close(ModalReference? modalReference, ModalResult? result = null)
        {
            if (modalReference is null) { return; }

            modalReference.SetResult(result);

            this._modals.Remove(modalReference);
            await InvokeAsync(StateHasChanged);
        }
    }
}