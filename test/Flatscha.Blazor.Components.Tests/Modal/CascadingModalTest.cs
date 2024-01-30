using Flatscha.Blazor.Components.Contracts.Dto;
using Flatscha.Blazor.Components.Contracts.Enum;
using Flatscha.Blazor.Components.General;
using Flatscha.Blazor.Components.Modal;
using System.Threading.Tasks;

namespace Flatscha.Blazor.Components.Tests.Modal
{
    public class CascadingModalTest : TestContext
    {
        public CascadingModalTest()
        {
        }

        [Fact]
        public async Task AddRemove()
        {
            var cut = RenderComponent<CascadingFlatschaModal>();

            var modals = cut.FindAll(".flatscha-modal");
            Assert.Empty(modals);

            var reference = cut.Instance.Open<FlatschaAlert>("Test");
            var reference2 = cut.Instance.Open<FlatschaAlert>("Test2");

            cut.Render();

            modals = cut.FindAll(".flatscha-modal");
            Assert.Equal(2, modals.Count);

            await cut.Instance.CloseAll();

            cut.Render();

            modals = cut.FindAll(".flatscha-modal");
            Assert.Empty(modals);
        }

        [Fact]
        public async Task CloseSingle()
        {
            var cut = RenderComponent<CascadingFlatschaModal>();

            var reference = cut.Instance.Open<FlatschaAlert>("Test");
            var reference2 = cut.Instance.Open<FlatschaAlert>("Test2");

            cut.Render();

            await cut.Instance.Close(reference2, ModalResult.Ok());

            var modals = cut.FindAll(".flatscha-modal");
            Assert.Single(modals);

            var res = await reference2.Result;

            Assert.Equal(EDialogResult.OK, res.Result);
        }

        [Fact]
        public async Task CloseModal()
        {
            var cut = RenderComponent<CascadingFlatschaModal>();

            var reference = cut.Instance.Open<FlatschaAlert>("Test");
            var reference2 = cut.Instance.Open<FlatschaAlert>("Test2");

            cut.Render();

            await reference2.Close(ModalResult.Ok());

            var modals = cut.FindAll(".flatscha-modal");
            Assert.Single(modals);

            var res = await reference2.Result;

            Assert.Equal(EDialogResult.OK, res.Result);
        }
    }
}
