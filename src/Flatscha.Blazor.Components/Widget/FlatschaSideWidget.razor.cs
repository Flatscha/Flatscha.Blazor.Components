using Flatscha.Blazor.Components.Base.MultiChild;

namespace Flatscha.Blazor.Components.Widget
{
    public partial class FlatschaSideWidget : RegisterAtParentComponentBase<CascadingFlatschaSideWidget, FlatschaSideWidget>
    {
        [Parameter] public int OpenedWidth { get; set; } = 20;
        [Parameter] public int OpenedHeight { get; set; } = 40;
        [Parameter] public string FAClass { get; set; } = "angle-left";

        private int _childIndex = 0;

        protected override void RegisterAtParent()
        {
            base.RegisterAtParent();
            this._childIndex = this.Parent.Items.IndexOf(this);
        }

        protected override void DeregisterFromParent()
        {
            base.DeregisterFromParent();
            this._childIndex = this.Parent.Items.IndexOf(this);
        }
    }
}