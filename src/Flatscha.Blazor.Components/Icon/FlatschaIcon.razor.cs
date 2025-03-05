
using Flatscha.Blazor.Components.Contracts.Interfaces.Services;

namespace Flatscha.Blazor.Components.Icon
{
    public partial class FlatschaIcon
    {
        [Inject] private IDefaultIconService _defaultIconService { get; set; }

        private string _faClass = "question";

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AllOtherAttributes { get; set; }

        [Parameter, EditorRequired]
        public string FAClass
        {
            get => this._faClass;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) { return; }

                if (value.StartsWith("fa-"))
                {
                    this._faClass = value.Replace("fa-", "");
                }
                else
                {
                    this._faClass = value;
                }
            }
        }

        [Parameter] public EIconStyle? Style { get; set; } = null;

        private EIconStyle GetStyle() => this.Style ?? this._defaultIconService.Style;
    }
}