using Microsoft.JSInterop;
using System.Reflection;

namespace Flatscha.Blazor.Components.Extensions
{
    public static class JSRuntimeExtensions
    {
        public static async Task<IJSObjectReference> GetJSModule(this IJSRuntime js, ComponentBase component)
            => await js.InvokeAsync<IJSObjectReference>("import", js.GetJsModulePath(component));

        private static string GetJsModulePath(this IJSRuntime js, ComponentBase component)
        {
            var type = component.GetType();

            var entryAssembly = Assembly.GetEntryAssembly();

            var path = "./";
            if (!type.Assembly.Equals(entryAssembly))
            {
                path += "_content/" + type.Assembly.GetName().Name;
            }

            path = path.Append(type.GetFolderNameSpace());
            path = path.Append(type.GetNameWithoutGenericArity() + ".razor.js");

            return path;
        }

        private static string GetFolderNameSpace(this Type type)
        {
            var ns = type.Namespace
                .Replace(type.Assembly.GetName().Name, string.Empty);

            return ns.Replace(".", "/");
        }

        private static string Append(this string startPath, params string[] paths)
            => paths.Aggregate(startPath, (current, path) => current.TrimEnd('/') + "/" + path.TrimStart('/'));

        private static string GetNameWithoutGenericArity(this Type t)
        {
            var index = t.Name.IndexOf('`');
            return index == -1 ? t.Name : t.Name[..index];
        }
    }
}
