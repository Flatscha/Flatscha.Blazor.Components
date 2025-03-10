using Microsoft.Extensions.Logging;

namespace Flatscha.Blazor.Components.Extensions
{
    public static class EnumExtensions
    {
        public static EAlertLevel GetAlertLevel(this LogLevel self)
           => self switch
           {
               LogLevel.Critical => EAlertLevel.Error,
               LogLevel.Error => EAlertLevel.Error,
               LogLevel.Warning => EAlertLevel.Warning,
               LogLevel.Information => EAlertLevel.Info,
               LogLevel.Debug => EAlertLevel.Normal,
               LogLevel.Trace => EAlertLevel.Normal,
               _ => EAlertLevel.Unknown,
           };

        public static string GetLevelClass(this LogLevel self)
            => self switch
            {
                LogLevel.Trace => "trace",
                LogLevel.Debug => "debug",
                LogLevel.Warning => "warn",
                LogLevel.Error => "error",
                LogLevel.Critical => "fatal",
                _ => "info",
            };
    }
}
