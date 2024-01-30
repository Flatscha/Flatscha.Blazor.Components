using System.Collections.ObjectModel;

namespace Flatscha.Blazor.Components.Base.MultiChild
{
    public abstract class MultiChildComponentBase<TChild, TSelf> : ComponentBase
        where TChild : RegisterAtParentComponentBase<TSelf, TChild>, IComponent
        where TSelf : MultiChildComponentBase<TChild, TSelf>, IComponent
    {
        private List<TChild> _items = new();
        public ReadOnlyCollection<TChild> Items => this._items.AsReadOnly();

        [Parameter] public RenderFragment? ChildContent { get; set; }

        [Parameter] public TChild? ActiveItem { get; set; }

        [Parameter] public EventCallback<TChild?> ActiveItemChanged { get; set; }

        protected override void OnInitialized()
        {
            if (this.GetType() != typeof(TSelf)) { throw new ArgumentException($"Generic type {nameof(TSelf)} has to match own type"); }
        }

        public void Add(TChild child)
        {
            if (this._items.Contains(child)) { return; }

            this._items.Add(child);
        }

        public void Remove(TChild child) => this._items.Remove(child);

        public virtual async Task SetActiveItem(TChild? item)
        {
            if (this.ActiveItem is not null)
            {
                if (this.ActiveItem == item) { return; }

                await this.ActiveItem.SetInactive();
            }

            this.ActiveItem = item;
            await ActiveItemChanged.InvokeAsync(item);

            await InvokeAsync(StateHasChanged);
        }
    }
}
