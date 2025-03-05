using Flatscha.Blazor.Components.Contracts.Dto;
using Flatscha.Blazor.Components.Contracts.Dto.Search;
using Flatscha.Blazor.Components.Modal;

namespace Flatscha.Blazor.Components.Search
{
    public partial class SearchItemListComponent
    {
        [CascadingParameter] public FlatschaModal _modal { get; set; }

        [Parameter] public IEnumerable<SearchItemDto> Items { get; set; }

        private async Task Click(SearchItemDto item)
        {
            item.OnClick?.Invoke();

            await this._modal.CloseAsync(ModalResult.Ok(item.ReturnValue));
        }
    }
}