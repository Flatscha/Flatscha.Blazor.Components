using Flatscha.Blazor.Components.Contracts.Dto;
using Flatscha.Blazor.Components.Contracts.Enum;
using Flatscha.Blazor.Components.Modal;
using Flatscha.Blazor.Components.Modal.Interface;

namespace Flatscha.Blazor.Components.Tests.Modal
{
    public class ModalTest : TestContext
    {
        private readonly Fixture _fixture = new();

        private Mock<ICascadingFlatschaModal> _mockCascadingModal = new();

        public ModalTest()
        {
        }

        [Fact]
        public void CancelOverlay()
        {
            var text = this._fixture.Create<string>();

            var modal = RenderComponent<FlatschaModal>(parameters =>
            {
                parameters.AddCascadingValue(this._mockCascadingModal.Object);
                parameters.Add(p => p.ChildContent, text);
            });

            var overlay = modal.Find(".flatscha-modal-overlay");
            overlay.Click();

            this._mockCascadingModal.Verify(x => x.Close(modal.Instance, It.Is<ModalResult>(res => res.Result == EDialogResult.Cancel)));
        }

        [Fact]
        public void CancelButton()
        {
            var text = this._fixture.Create<string>();

            var modal = RenderComponent<FlatschaModal>(parameters =>
            {
                parameters.AddCascadingValue(this._mockCascadingModal.Object);
                parameters.Add(p => p.ChildContent, text);
            });

            var closeButton = modal.Find(".flatscha-modal-header .flatscha-icon-button");
            closeButton.Click();

            this._mockCascadingModal.Verify(x => x.Close(modal.Instance, It.Is<ModalResult>(res => res.Result == EDialogResult.Cancel)));
        }
    }
}
