namespace Yunyong.DataExchange.Core.Common
{
    internal class XConfig
    {
        internal static bool IsDebug { get; set; } = false;

        internal static bool IsCodeFirst { get; set; } = false;
        internal static bool IsNeedChangeDb { get; set; } = true;
        internal static string TablesNamespace { get; set; } = string.Empty;

    }
}
