using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Configs;
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

        internal static string MySQL { get; } = "MySql.Data.MySqlClient.MySqlConnection";
        internal static DbEnum DB { get; set; } = DbEnum.None;

        internal static int CommandTimeout { get; set; } = 30;  // s 

        internal static BindingFlags ClassSelfMember { get; private set; } = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;

        internal static MethodInfo EnumParse { get; }
            = typeof(Enum).GetMethod(nameof(Enum.Parse), new Type[] { typeof(Type), typeof(string), typeof(bool) });

        internal static CommandBehavior MultiRow { get; } = CommandBehavior.SequentialAccess | CommandBehavior.SingleResult;
        internal static CommandBehavior SingleRow { get; }
            = CommandBehavior.SequentialAccess | CommandBehavior.SingleResult | CommandBehavior.SingleRow;

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

        internal static DbTypeConfig DTC { get; } = new DbTypeConfig();
        internal static ParamInfoConfig PIC { get; } = new ParamInfoConfig();
        internal static ExceptionConfig EC { get; } = new ExceptionConfig();
        internal static CsTypeConfig TC { get; } = new CsTypeConfig();

        /************************************************************************************************************/

        internal static int CacheRetry { get; } = 3;

        /************************************************************************************************************/

        internal static ConcurrentDictionary<string, ParamTypeEnum> MySQLTypes { get; }
            = new ConcurrentDictionary<string, ParamTypeEnum>(
                new List<KeyValuePair<string, ParamTypeEnum>>
                {
                    new KeyValuePair<string, ParamTypeEnum>( "tinyint",ParamTypeEnum.MySQL_TinyInt),
                    new KeyValuePair<string, ParamTypeEnum>( "smallint",ParamTypeEnum.MySQL_SmallInt),
                    new KeyValuePair<string, ParamTypeEnum>( "mediumint",ParamTypeEnum.MySQL_MediumInt),
                    new KeyValuePair<string, ParamTypeEnum>( "int",ParamTypeEnum.MySQL_Int),
                    new KeyValuePair<string, ParamTypeEnum>( "bigint",ParamTypeEnum.MySQL_BigInt),
                    new KeyValuePair<string, ParamTypeEnum>( "float",ParamTypeEnum.MySQL_Float),
                    new KeyValuePair<string, ParamTypeEnum>( "double",ParamTypeEnum.MySQL_Double),
                    new KeyValuePair<string, ParamTypeEnum>( "decimal",ParamTypeEnum.MySQL_Decimal),
                    new KeyValuePair<string, ParamTypeEnum>( "year",ParamTypeEnum.MySQL_Year),
                    new KeyValuePair<string, ParamTypeEnum>( "time",ParamTypeEnum.MySQL_Time),
                    new KeyValuePair<string, ParamTypeEnum>( "date",ParamTypeEnum.MySQL_Date),
                    new KeyValuePair<string, ParamTypeEnum>( "datetime",ParamTypeEnum.MySQL_DateTime),
                    new KeyValuePair<string, ParamTypeEnum>( "timestamp",ParamTypeEnum.MySQL_TimeStamp),
                    new KeyValuePair<string, ParamTypeEnum>( "char",ParamTypeEnum.MySQL_Char),
                    new KeyValuePair<string, ParamTypeEnum>( "varchar",ParamTypeEnum.MySQL_VarChar),
                    new KeyValuePair<string, ParamTypeEnum>( "tinytext",ParamTypeEnum.MySQL_TinyText),
                    new KeyValuePair<string, ParamTypeEnum>( "text",ParamTypeEnum.MySQL_Text),
                    new KeyValuePair<string, ParamTypeEnum>( "mediumtext",ParamTypeEnum.MySQL_MediumText),
                    new KeyValuePair<string, ParamTypeEnum>( "longtext",ParamTypeEnum.MySQL_LongText),
                    new KeyValuePair<string, ParamTypeEnum>( "enum",ParamTypeEnum.MySQL_Enum),
                    new KeyValuePair<string, ParamTypeEnum>( "set",ParamTypeEnum.MySQL_Set),
                    new KeyValuePair<string, ParamTypeEnum>( "bit",ParamTypeEnum.MySQL_Bit),
                    new KeyValuePair<string, ParamTypeEnum>( "binary",ParamTypeEnum.MySQL_Binary),
                    new KeyValuePair<string, ParamTypeEnum>( "varbinary",ParamTypeEnum.MySQL_VarBinary),
                    new KeyValuePair<string, ParamTypeEnum>( "tinyblob",ParamTypeEnum.MySQL_TinyBlob),
                    new KeyValuePair<string, ParamTypeEnum>( "blob",ParamTypeEnum.MySQL_Blob),
                    new KeyValuePair<string, ParamTypeEnum>( "mediumblob",ParamTypeEnum.MySQL_MediumBlob),
                    new KeyValuePair<string, ParamTypeEnum>( "longblob",ParamTypeEnum.MySQL_LongBlob)
                });

        internal static ConcurrentDictionary<Type, Func<Context, ParamTypeEnum, DbType>> TypeFuncs { get; }
            = new ConcurrentDictionary<Type, Func<Context, ParamTypeEnum, DbType>>(
                new List<KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>>
                {
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Int,DTC.IntProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Long,DTC.LongProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Decimal,DTC.DecimalProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Bool,DTC.BoolProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.String,DTC.StringProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.DateTime,DTC.DateTimeProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Guid,DTC.GuidProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Byte,DTC.ByteProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.ByteArray,DTC.ByteArrayProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Char,DTC.CharProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Double,DTC.DoubleProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Float,DTC.FloatProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Sbyte,DTC.SbyteProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Short,DTC.ShortProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Uint,DTC.UintProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Ulong,DTC.UlongProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Ushort,DTC.UshortProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.TimeSpan,DTC.TimeSpanProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.DateTimeOffset,DTC.DateTimeOffsetProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Object,DTC.ObjectProc)
                });

        internal static ConcurrentDictionary<Type, Func<DicParam, Type, Context, ParamInfo>> ParamFuncs { get; }
            = new ConcurrentDictionary<Type, Func<DicParam, Type, Context, ParamInfo>>(
                new List<KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>>
                {
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Int,PIC.IntParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Long,PIC.LongParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Decimal,PIC.DecimalParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Bool,PIC.BoolParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.String,PIC.StringParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.DateTime,PIC.DateTimeParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Guid,PIC.GuidParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Byte,PIC.ByteParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Char,PIC.CharParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Double,PIC.DoubleParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Float,PIC.FloatParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Sbyte,PIC.SbyteParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Short,PIC.ShortParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Uint,PIC.UintParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Ulong,PIC.UlongParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.Ushort,PIC.UshortParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(TC.TimeSpan,PIC.TimeSpanParam)
                });

    }
}
