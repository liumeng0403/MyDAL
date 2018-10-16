using System;

namespace Yunyong.DataExchange.Core.Common
{
    internal class XConfig
    {
        internal static bool IsDebug { get; set; } = false;

        /************************************************************************************************************/

        internal static bool IsCodeFirst { get; set; } = false;
        internal static bool IsNeedChangeDb { get; set; } = true;
        internal static string TablesNamespace { get; set; } = string.Empty;

        /************************************************************************************************************/

        internal static Type Bool { get; private set; } = typeof(bool);
        internal static Type Byte { get; private set; } = typeof(byte);
        internal static Type Char { get; private set; } = typeof(char);
        internal static Type Decimal { get; private set; } = typeof(decimal);
        internal static Type Double { get; private set; } = typeof(double);
        internal static Type Float { get; private set; } = typeof(float);
        internal static Type Int { get; private set; } = typeof(int);
        internal static Type Long { get; private set; } = typeof(long);
        internal static Type Sbyte { get; private set; } = typeof(sbyte);
        internal static Type Short { get; private set; } = typeof(short);
        internal static Type Uint { get; private set; } = typeof(uint);
        internal static Type Ulong { get; private set; } = typeof(ulong);
        internal static Type Ushort { get; private set; } = typeof(ushort);
        internal static Type String { get; private set; } = typeof(string);
        internal static Type DateTime { get; private set; } = typeof(DateTime);
        internal static Type TimeSpan { get; private set; } = typeof(TimeSpan);
        internal static Type Guid { get; private set; } = typeof(Guid);
        internal static Type NullableT { get; private set; } = typeof(Nullable<>);

    }
}
