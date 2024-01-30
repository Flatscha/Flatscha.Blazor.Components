using Flatscha.Blazor.Components.Contracts.Enum;

namespace Flatscha.Blazor.Components.Contracts.Dto.Dialog
{
    public class DialogButtonParameter
    {
        public bool OK { get; set; }
        public bool Cancel { get; set; }
        public bool Yes { get; set; }
        public bool No { get; set; }
        public bool Retry { get; set; }
        public bool Ignore { get; set; }

        public DialogButtonParameter()
        {
        }

        public DialogButtonParameter(EDialogButtons buttons)
        {
            switch (buttons)
            {
                case EDialogButtons.OK:
                    this.OK = true;
                    break;
                case EDialogButtons.OKCancel:
                    this.OK = true;
                    this.Cancel = true;
                    break;
                case EDialogButtons.YesNo:
                    this.Yes = true;
                    this.No = true;
                    break;
                case EDialogButtons.YesNoCancel:
                    this.Yes = true;
                    this.No = true;
                    this.Cancel = true;
                    break;
                case EDialogButtons.Retry:
                    this.Retry = true;
                    break;
                case EDialogButtons.RetryCancel:
                    this.Retry = true;
                    this.Cancel = true;
                    break;
                case EDialogButtons.OKIgnore:
                    this.OK = true;
                    this.Ignore = true;
                    break;
            }
        }
    }
}
