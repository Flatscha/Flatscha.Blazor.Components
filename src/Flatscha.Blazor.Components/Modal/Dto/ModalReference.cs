using Flatscha.Blazor.Components.Contracts.Dto;

namespace Flatscha.Blazor.Components.Modal.Dto
{
    public class ModalReference
    {
        private readonly TaskCompletionSource<ModalResult> _taskCompletionSource = new(TaskCreationOptions.RunContinuationsAsynchronously);
        private readonly CascadingFlatschaModal _modal;

        public RenderFragment? Content { get; }
        public FlatschaModal? Instance { get; set; }

        public Task<ModalResult> Result => this._taskCompletionSource.Task;

        public ModalReference(RenderFragment? content, CascadingFlatschaModal modal)
        {
            this.Content = content;
            this._modal = modal;
        }

        public Task Close(ModalResult? result = null) => this._modal.Close(this, result);

        internal void SetResult(ModalResult? result = null) => this._taskCompletionSource.TrySetResult(result ?? new(EDialogResult.Cancel));
    }
}
