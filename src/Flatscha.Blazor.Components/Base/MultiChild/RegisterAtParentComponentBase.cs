namespace Flatscha.Blazor.Components.Base.MultiChild
{
    public abstract class RegisterAtParentComponentBase<TParent, TSelf> : ComponentBase, IDisposable
        where TParent : MultiChildComponentBase<TSelf, TParent>, IComponent
        where TSelf : RegisterAtParentComponentBase<TParent, TSelf>, IComponent
    {
        [CascadingParameter] protected TParent Parent { get; set; }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        public bool Active { get; private set; } = false;

        protected override void OnInitialized()
        {
            if (this.GetType() != typeof(TSelf)) { throw new ArgumentException($"Generic type {nameof(TSelf)} has to match own type"); }

            this.RegisterAtParent();

            base.OnInitialized();
        }

        protected virtual void RegisterAtParent() => this.Parent.Add((TSelf)this);
        protected virtual void DeregisterFromParent() => this.Parent.Remove((TSelf)this);

        public Task Toggle() => this.Active ? this.SetInactive() : this.SetActive();

        public virtual async Task SetActive()
        {
            if (this.Active) { return; }

            this.Active = true;
            await this.Parent.SetActiveItem((TSelf)this);
            await this.InvokeAsync(StateHasChanged);
        }

        public virtual async Task SetInactive()
        {
            if (!this.Active) { return; }

            this.Active = false;
            await this.InvokeAsync(StateHasChanged);
        }

        public void Dispose() => this.DeregisterFromParent();
    }
}
