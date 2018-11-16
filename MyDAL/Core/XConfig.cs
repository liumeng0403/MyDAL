using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace MyDAL.Core
{
    internal class XConfig
    {
        internal static bool IsDebug { get; set; } = false;

        /************************************************************************************************************/

        internal static bool IsCodeFirst { get; set; } = false;
        internal static bool IsNeedChangeDb { get; set; } = true;
        internal static string TablesNamespace { get; set; } = string.Empty;

        /************************************************************************************************************/

        internal static string MySQL { get; } = "MySql.Data.MySqlClient.MySqlConnection";
        internal static DbEnum DB { get; set; } = DbEnum.None;

        internal static int CommandTimeout { get; set; } = 10;  // 10s 

        internal static BindingFlags ClassSelfMember { get; private set; } = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;

        internal static MethodInfo EnumParse { get; } = typeof(Enum).GetMethod(nameof(Enum.Parse), new Type[] { typeof(Type), typeof(string), typeof(bool) });

        internal static CommandBehavior MultiRow { get; } = CommandBehavior.SequentialAccess | CommandBehavior.SingleResult;
        internal static CommandBehavior SingleRow { get; } = CommandBehavior.SequentialAccess | CommandBehavior.SingleResult | CommandBehavior.SingleRow;

        /************************************************************************************************************/

        internal static Type Bool { get; private set; } = typeof(bool);
        internal static Type Byte { get; private set; } = typeof(byte);
        internal static Type ByteArray { get; private set; } = typeof(byte[]);
        internal static string LinqBinary { get; private set; } = "System.Data.Linq.Binary";
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
        internal static Type DateTimeNull { get; private set; } = typeof(DateTime?);
        internal static Type DateTimeOffset { get; private set; } = typeof(DateTimeOffset);
        internal static Type TimeSpan { get; private set; } = typeof(TimeSpan);
        internal static Type Guid { get; private set; } = typeof(Guid);
        internal static Type NullableT { get; private set; } = typeof(Nullable<>);
        internal static Type ListT { get; private set; } = typeof(List<>);
        internal static Type IEnumerableT { get; private set; } = typeof(IEnumerable<>);
        internal static Type Object { get; private set; } = typeof(object);

        /************************************************************************************************************/

        internal static Type XTableAttribute { get; private set; } = typeof(XTableAttribute);
        internal static string XTableFullName { get; private set; } = typeof(XTableAttribute).FullName;

        internal static Type XColumnAttribute { get; private set; } = typeof(XColumnAttribute);
        internal static string XColumnFullName { get; private set; } = typeof(XColumnAttribute).FullName;

        /************************************************************************************************************/

        internal static MethodInfo GetItem { get; } = typeof(IDataRecord)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetIndexParameters().Length > 0 && p.GetIndexParameters()[0].ParameterType == typeof(int))
            .Select(p => p.GetGetMethod())
            .First();

        /************************************************************************************************************/

        internal static string _001 { get; } = "001";
        internal static string _002 { get; } = "002";
        internal static string _003 { get; } = "003";
        internal static string _004 { get; } = "004";
        internal static string _005 { get; } = "005";
        internal static string _006 { get; } = "006";
        internal static string _007 { get; } = "007";
        internal static string _008 { get; } = "008";

        /************************************************************************************************************/

    }
}
