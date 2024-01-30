using Bunit;
using Flatscha.Blazor.Components.Tests.Base;
using Flatscha.Blazor.Components.Wizard;
using System.Linq;

namespace Flatscha.Blazor.Components.Tests.Wizard
{
    public class WizardTests : BaseMultiChildComponentTest<FlatschaWizardStep, FlatschaWizard>
    {
        public WizardTests()
        {
        }

        [Fact]
        public void ActivateStep()
        {
            FlatschaWizardStep activeStep = null;

            var (wizard, wizardStep1, wizardStep2) = this.RenderParentWithChilds(
                p => { p.Add(p => p.ActiveItemChanged, (item) => activeStep = item); },
                p => { p.Add(p => p.Name, this._fixture.Create<string>()); },
                p => { p.Add(p => p.Name, this._fixture.Create<string>()); });

            wizard.Render();

            this.CheckStep(wizard, wizardStep1, ref activeStep);
            this.CheckStep(wizard, wizardStep2, ref activeStep);
        }

        private void CheckStep(IRenderedComponent<FlatschaWizard> wizard, IRenderedComponent<FlatschaWizardStep> wizardStep, ref FlatschaWizardStep? activeStep)
        {
            var navSteps = wizard.FindAll(".flatscha-wizard-nav-step-container");

            var navStep1 = navSteps.FirstOrDefault(x => x.InnerHtml.Contains(wizardStep.Instance.Name));
            Assert.NotNull(navStep1);

            navStep1.Click();
            Assert.NotNull(activeStep);
            Assert.Equal(wizardStep.Instance, activeStep);
        }
    }
}
