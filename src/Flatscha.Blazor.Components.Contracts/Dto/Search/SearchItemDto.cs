namespace Flatscha.Blazor.Components.Contracts.Dto.Search
{
    public class SearchItemDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FAClass { get; set; } = string.Empty;
        public Action? OnClick { get; set; } = null;
        public object ReturnValue { get; set; } = null;

        public SearchItemDto() { }

        public SearchItemDto(string title, string description, string faClass, Action? onClick, object returnValue) : this()
        {
            this.Title = title;
            this.Description = description;
            this.FAClass = faClass;
            this.OnClick = onClick;
            this.ReturnValue = returnValue;
        }
    }
}
