using Newtonsoft.Json.Linq;

namespace Flatscha.Blazor.Components.Extensions
{
    public static class JsonExtensions
    {
        public static bool TryGetListFromJsonObject<TItem>(this object value, out List<TItem> items)
        {
            if (value is List<TItem> currentFavorites)
            {
                items = currentFavorites;
                return true;
            }
            else if (value is JArray jsonArray)
            {
                items = ConvertFromJArray<TItem>(jsonArray);
                return true;
            }
            else
            {
                items = [];
                return false;
            }
        }

        private static List<TItem> ConvertFromJArray<TItem>(JArray jsonArray)
        {
            var deserializedObjects = new List<TItem>();

            foreach (var token in jsonArray)
            {
                var obj = token.ToObject<TItem>();
                deserializedObjects.Add(obj);
            }

            return deserializedObjects;
        }
    }
}
