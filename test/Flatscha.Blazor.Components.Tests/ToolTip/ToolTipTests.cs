﻿using AutoFixture;
using Flatscha.Blazor.Components.Contracts.Interfaces.Components;
using Flatscha.Blazor.Components.ToolTip;
using Microsoft.AspNetCore.Components;
using Moq;

namespace Flatscha.Blazor.Components.Tests.ToolTip
{
    public class ToolTipTests : TestContext
    {
        private readonly Fixture _fixture = new();

        private Mock<ICascadingFlatschaToolTip> _mockCascadingToolTip = new();

        public ToolTipTests()
        {
        }

        [Fact]
        public void HoverToolTip()
        {
            var text = this._fixture.Create<string>();

            var cut = RenderComponent<FlatschaToolTip>(parameters =>
            {
                parameters.Add(p => p.ToolTip, this._mockCascadingToolTip.Object);
                parameters.Add(p => p.Text, text);
            });

            var wrapper = cut.Find(".tooltip-wrapper");
            wrapper.MouseOver();

            this._mockCascadingToolTip.Verify(x => x.ShowToolTip(It.IsAny<ElementReference>(), null, text));

            wrapper.MouseOut();

            this._mockCascadingToolTip.Verify(x => x.HideToolTip());
        }
    }
}
