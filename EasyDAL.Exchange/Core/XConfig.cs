using MyDAL.Core.Enums;
using System;
using System.Collections.Concurrent;
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

        /// <summary>
        /// Default is 4000, any value larger than this field will not have the default value applied.
        /// </summary>
        internal static int StringDefaultLength { get; private set; } = 4000;

        internal static BindingFlags ClassSelfMember { get; private set; } = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;

        internal static string LinqBinary { get; } = "System.Data.Linq.Binary";

        internal static MethodInfo EnumParse { get; } = typeof(Enum).GetMethod(nameof(Enum.Parse), new Type[] { typeof(Type), typeof(string), typeof(bool) });

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
        internal static Type ListT { get; private set; } = typeof(List<>);
        internal static Type Object { get; private set; } = typeof(object);

        /************************************************************************************************************/

        internal static Type XTableAttribute { get; private set; } = typeof(XTableAttribute);
        internal static string XTableFullName { get; private set; } = typeof(XTableAttribute).FullName;

        internal static Type XColumnAttribute { get; private set; } = typeof(XColumnAttribute);
        internal static string XColumnFullName { get; private set; } = typeof(XColumnAttribute).FullName;

        /************************************************************************************************************/

        internal static ConcurrentDictionary<Type, DbType> TypeDBTypeDic { get; private set; } = new ConcurrentDictionary<Type, DbType>
        {
            [typeof(byte)] = DbType.Byte,
            [typeof(byte[])] = DbType.Binary,
            [typeof(byte?)] = DbType.Byte,

            [typeof(sbyte)] = DbType.SByte,
            [typeof(sbyte?)] = DbType.SByte,

            [typeof(short)] = DbType.Int16,
            [typeof(short?)] = DbType.Int16,

            [typeof(ushort)] = DbType.UInt16,
            [typeof(ushort?)] = DbType.UInt16,

            [typeof(int)] = DbType.Int32,
            [typeof(int?)] = DbType.Int32,

            [typeof(uint)] = DbType.UInt32,
            [typeof(uint?)] = DbType.UInt32,

            [typeof(long)] = DbType.Int64,
            [typeof(long?)] = DbType.Int64,

            [typeof(ulong)] = DbType.UInt64,
            [typeof(ulong?)] = DbType.UInt64,

            [typeof(float)] = DbType.Single,
            [typeof(float?)] = DbType.Single,

            [typeof(double)] = DbType.Double,
            [typeof(double?)] = DbType.Double,

            [typeof(decimal)] = DbType.Decimal,
            [typeof(decimal?)] = DbType.Decimal,

            [typeof(bool)] = DbType.Boolean,
            [typeof(bool?)] = DbType.Boolean,

            [typeof(string)] = DbType.String,

            [typeof(char)] = DbType.StringFixedLength,
            [typeof(char?)] = DbType.StringFixedLength,

            [typeof(Guid)] = DbType.Guid,
            [typeof(Guid?)] = DbType.Guid,

            [typeof(DateTime)] = DbType.DateTime,
            [typeof(DateTime?)] = DbType.DateTime,

            [typeof(DateTimeOffset)] = DbType.DateTimeOffset,
            [typeof(DateTimeOffset?)] = DbType.DateTimeOffset,

            [typeof(TimeSpan)] = DbType.Time,
            [typeof(TimeSpan?)] = DbType.Time,

            [typeof(object)] = DbType.Object
        };

        /************************************************************************************************************/

        internal static MethodInfo GetItem { get; } = typeof(IDataRecord)
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(p => p.GetIndexParameters().Length > 0 && p.GetIndexParameters()[0].ParameterType == typeof(int))
            .Select(p => p.GetGetMethod())
            .First();

        /************************************************************************************************************/

    }
}
