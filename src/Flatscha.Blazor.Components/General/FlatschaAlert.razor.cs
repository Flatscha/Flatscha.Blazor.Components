namespace Flatscha.Blazor.Components.General
{
    public partial class FlatschaAlert
    {
        [Parameter] public EAlertLevel Level { get; set; }

        [Parameter] public RenderFragment? ChildContent { get; set; }

        private string GetAlertLevelClass() => this.Level switch
        {
            EAlertLevel.Normal => "alert-normal",
            EAlertLevel.Info => "alert-info",
            EAlertLevel.Success => "alert-success",
            EAlertLevel.Warning => "alert-warn",
            EAlertLevel.Error => "alert-error",
            _ => string.Empty,
        };

        private string GetAlertLevelIcon() => this.Level switch
        {
            EAlertLevel.Normal => "calendar-week",
            EAlertLevel.Info => "circle-info",
            EAlertLevel.Success => "circle-check",
            EAlertLevel.Warning => "triangle-exclamation",
            EAlertLevel.Error => "circle-exclamation",
            _ => string.Empty,
        };

    }
}