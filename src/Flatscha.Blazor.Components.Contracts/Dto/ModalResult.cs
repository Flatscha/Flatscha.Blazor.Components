using Flatscha.Blazor.Components.Contracts.Enum;

namespace Flatscha.Blazor.Components.Contracts.Dto
{
    public class ModalResult
    {
        public EDialogResult Result { get; }
        public object? Data { get; }
        public Type? DataType => this.Data?.GetType();

        public ModalResult(EDialogResult result = EDialogResult.OK, object? data = null)
        {
            this.Result = result;
            this.Data = data;
        }

        public static ModalResult Ok(object? data = null) => new(EDialogResult.OK, data);
        public static ModalResult Cancel(object? data = null) => new(EDialogResult.Cancel, data);
        public static ModalResult Retry(object? data = null) => new(EDialogResult.Retry, data);
        public static ModalResult Ignore(object? data = null) => new(EDialogResult.Ignore, data);
        public static ModalResult Yes(object? data = null) => new(EDialogResult.Yes, data);
        public static ModalResult No(object? data = null) => new(EDialogResult.No, data);

        public bool IsSuccessful => this.Result is EDialogResult.OK or EDialogResult.Yes;
        public bool IsCanceled => this.Result is EDialogResult.Cancel;

        public bool TryGetData<T>(out T res)
        {
            if (this.Result is T realData)
            {
                res = realData;
                return true;
            }

            res = default;
            return false;
        }

        public bool TryGetSuccessfulData<T>(out T res)
        {
            if (!this.IsSuccessful)
            {
                res = default;
                return false;
            }

            return this.TryGetData(out res);
        }
    }
}
