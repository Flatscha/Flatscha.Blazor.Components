using Flatscha.Blazor.Components.Base.MultiChild;
using System;

namespace Flatscha.Blazor.Components.Tests.Base
{
    public abstract class BaseMultiChildComponentTest<TChild, TParent> : BaseComponentTest
        where TChild : RegisterAtParentComponentBase<TParent, TChild>, IComponent
        where TParent : MultiChildComponentBase<TChild, TParent>, IComponent
    {
        [Fact]
        public void Create() => this.RenderParentWithChilds();

        protected (IRenderedComponent<TParent> Parent, IRenderedComponent<TChild> Child1, IRenderedComponent<TChild> Child2) RenderParentWithChilds(Action<ComponentParameterCollectionBuilder<TParent>>? parentParameterOptions = null)
        {
            var parent = RenderComponent<TParent>(parentParameterOptions);

            Assert.Empty(parent.Instance.Items);

            var child1 = RenderComponent<TChild>(parameters =>
            {
                parameters.AddCascadingValue(parent.Instance);
            });

            var child2 = RenderComponent<TChild>(parameters =>
            {
                parameters.AddCascadingValue(parent.Instance);
            });

            Assert.Equal(2, parent.Instance.Items.Count);

            return (parent, child1, child2);
        }
    }
}
