using Flatscha.Blazor.Components.Modal.Interface;
using Flatscha.Blazor.TestApp.Components.Modal;
using Microsoft.AspNetCore.Components;
using Flatscha.Blazor.Components.Extensions;
using Flatscha.Blazor.Components.Contracts.Dto.Search;
using Flatscha.Blazor.Components.Contracts.Dto;
using Flatscha.Blazor.Components.Search;

namespace Flatscha.Blazor.TestApp.Components.Pages
{
    public partial class Home
    {
        [CascadingParameter] private ICascadingFlatschaModal _modal { get; set; }

        private string _icon = "house";
        private void ChangeIcon() => this._icon = this._icon == "house" ? "user" : "house";

        private string StepError() => this._icon == "house" ? "UserIcon required" : null;

        private async Task OpenModal()
        {
            var reference = this._modal.Open<TestModalContent>("Test Modal 2",
                        options: options =>
                        {
                            options.CustomOverlayClass = "test2";
                        });

            var res = await reference.Result;
        }

        private async Task Load()
        {
            var modal = this._modal.OpenLoading();

            await Task.Delay(5000);

            await modal.Close();
        }

        private async Task ShowSearchItemModal()
        {
            var actions = new List<SearchItemDto>
            {
                new() {
                    Title = "Bearbeiten",
                    Description = "Aktionen kann bearbeitet werden",
                    FAClass = "pen",
                },
                new() {
                    Title = "Löschen",
                    Description = "Aktionen kann gelöscht werden",
                    FAClass = "trash-can",
                },
                new() {
                    Title = "Starten",
                    Description = "Aktionen kann gestartet werden",
                    FAClass = "play",
                },
            };

            var parameters = new ModalParameters
            {
                { nameof(SearchItemListComponent.Items), actions }
            };

            var modal = this._modal.Open<SearchItemListComponent>("Aktionen", parameters);
        }

        private async Task ShowSearchModal()
        {
            var parameters = new ModalParameters
            {
                { nameof(SearchModalComponent<Range, Index>.Items), new List<Range>
                    {
                        new(10, 20),
                        new(0, 20),
                        new(40, 48),
                        new(1200, 28000),
                    }
                },
                { nameof(SearchModalComponent<Range, Index>.FavoriteKey), "TestFavorit" },
                { nameof(SearchModalComponent<Range, Index>.FavoriteObject), (Range dto) => dto.Start },
                { nameof(SearchModalComponent<Range, Index>.Name), (Range dto) => $"Start {dto.Start}" },
                { nameof(SearchModalComponent<Range, Index>.Sub), (Range dto) => $"End {dto.End}" },
                { nameof(SearchModalComponent<Range, Index>.DisableOrder), true },
            };
            var modalInstance = this._modal.Open<SearchModalComponent<Range, Index>>("Test Suche", parameters);

            var result = await modalInstance.Result;
        }
    }
}