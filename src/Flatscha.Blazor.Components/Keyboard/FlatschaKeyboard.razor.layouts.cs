namespace Flatscha.Blazor.Components.Keyboard
{
    public partial class FlatschaKeyboard
    {
        private List<string> Numbers = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "0"];

        private List<List<string>> NumPadLayout =>
        [
            ["7", "8", "9"],
            ["4", "5", "6"],
            ["1", "2", "3"],
            ["0"]
        ];

        private List<List<string>> EnglishLayout =>
        [
            [.. Numbers],
            ["Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P"],
            ["A", "S", "D", "F", "G", "H", "J", "K", "L"],
            ["Z", "X", "C", "V", "B", "N", "M"]
        ];

        private List<List<string>> GermanLayout =>
        [
            [.. Numbers, "ß"],
            ["Q", "W", "E", "R", "T", "Z", "U", "I", "O", "P", "Ü"],
            ["A", "S", "D", "F", "G", "H", "J", "K", "L", "Ö", "Ä"],
            ["Y", "X", "C", "V", "B", "N", "M"]
        ];
    }
}