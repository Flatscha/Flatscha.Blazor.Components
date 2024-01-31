
namespace Flatscha.Blazor.Components.Pagination
{
    public partial class FlatschaTabControl
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await base.SetActiveItem(this.Items.FirstOrDefault());
            }

            await base.OnAfterRenderAsync(firstRender);
        }
    }
}