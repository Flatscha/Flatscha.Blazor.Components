using Flatscha.Blazor.Components.Base.MultiChild;

namespace Flatscha.Blazor.Components.Wizard
{
    public partial class FlatschaWizardStep : RegisterAtParentComponentBase<FlatschaWizard, FlatschaWizardStep>
    {
        [Parameter] public string Name { get; set; }
        [Parameter] public bool WithHeader { get; set; } = false;
        [Parameter] public Func<string>? StepError { get; set; }
    }
}