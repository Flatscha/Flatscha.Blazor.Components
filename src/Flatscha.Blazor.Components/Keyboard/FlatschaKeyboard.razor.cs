using Flatscha.Blazor.Components.Base;
using Microsoft.JSInterop;

namespace Flatscha.Blazor.Components.Keyboard
{
    public partial class FlatschaKeyboard : JSModuleComponent
    {
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public EKeyboardType Type { get; set; } = EKeyboardType.German;
        [Parameter] public string InputClassName { get; set; } = "flatscha-keyboard-input";
        [Parameter] public bool AlwaysActive { get; set; }
        [Parameter] public bool OnlyTouch { get; set; }
        [Parameter] public List<List<string>> Keys { get; set; } = [];
        [Parameter] public EventCallback OnEnterPressed { get; set; }
        [Parameter] public EventCallback OnReturnPressed { get; set; }

        private ElementReference _keyboard;

        private bool _shift = false;
        private List<List<string>> _keys = [];

        protected override void OnInitialized()
        {
            this.InitializeKeys();

            base.OnInitialized();
        }

        protected override async Task OnAfterJSImport()
        {
            await this._jsModule.InvokeVoidAsync("addFlatschaKeyboardEvents", this._keyboard, this.InputClassName, this.OnlyTouch, this.AlwaysActive);
        }

        private void InitializeKeys() => this._keys = this.Type switch
        {
            EKeyboardType.Keys => this.Keys,
            EKeyboardType.Numpad => this.NumPadLayout,
            EKeyboardType.English => this.EnglishLayout,
            EKeyboardType.German => this.GermanLayout,
            _ => throw new NotImplementedException(),
        };

        private async Task KeyPressed(string key)
        {
            var shiftedKey = this.GetShiftedKey(key);

            await this._jsModule.InvokeVoidAsync("keyPressed", shiftedKey);
        }

        private string GetShiftedKey(string key)
        {
            if (this.Type is EKeyboardType.Keys) { return key; }

            return this._shift ? key.ToUpper() : key.ToLower();
        }

        private void ShiftPressed()
        {
            this._shift = !this._shift;
        }

        private async Task EnterPressed()
        {
            try
            {
                await this.OnEnterPressed.InvokeAsync();

                await this._jsModule.InvokeVoidAsync("enterPressed", this._keyboard);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private async Task ReturnPressed()
        {
            try
            {
                await this.OnReturnPressed.InvokeAsync();

                await this._jsModule.InvokeVoidAsync("returnPressed");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private async Task ClearPressed() => await this._jsModule.InvokeVoidAsync("clearPressed");
    }
}