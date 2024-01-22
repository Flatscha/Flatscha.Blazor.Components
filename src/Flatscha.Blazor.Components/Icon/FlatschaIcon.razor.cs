
namespace Flatscha.Blazor.Components.Icon
{
    public partial class FlatschaIcon
    {
        private string _faClass = "question";

        private const string FontAwesomeFile = $"_content/Flatscha.Blazor.Components/css/fontawesome.svg";

        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> AllOtherAttributes { get; set; }

        [Parameter, EditorRequired]
        public string FAClass
        {
            get => this._faClass;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) { return; }

                this._faClass = value.StartsWith("fa-") ? value.Replace("fa-", "") : value;
            }
        }
    }
}