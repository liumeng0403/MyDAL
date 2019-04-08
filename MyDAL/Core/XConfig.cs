using HPC.DAL.AdoNet;
using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Configs;
using HPC.DAL.Core.Enums;
using HPC.DAL.DataRainbow.MySQL;
using HPC.DAL.DataRainbow.SQLServer;
using HPC.DAL.DataRainbow.XCommon.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace HPC.DAL.Core
{
    internal class XConfig
    {
        internal static bool IsDebug { get; set; } = false;

        /************************************************************************************************************/

        internal static string MySQL { get; } = "MySql.Data.MySqlClient.MySqlConnection";
        internal static string SqlServer { get; } = "System.Data.SqlClient.SqlConnection";

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

        internal static ColTypeConfig CTC { get; } = new ColTypeConfig();
        internal static IDbTypeConfig DTC { get; } = new DbTypeConfig();
        internal static ParamInfoConfig PIC { get; } = new ParamInfoConfig();
        internal static ExceptionConfig EC { get; } = new ExceptionConfig();
        internal static CsTypeConfig TC { get; } = new CsTypeConfig();

        /************************************************************************************************************/

        internal static int CacheRetry { get; } = 3;

        /************************************************************************************************************/

        internal static ConcurrentDictionary<string, DbEnum> ConnTypes { get; }
            = new ConcurrentDictionary<string, DbEnum>(
               new List<KeyValuePair<string, DbEnum>>
               {
                   new KeyValuePair<string, DbEnum>("MySql.Data.MySqlClient.MySqlConnection",DbEnum.MySQL),
                   new KeyValuePair<string, DbEnum>("System.Data.SqlClient.SqlConnection",DbEnum.SQLServer)
               });
        internal static ConcurrentDictionary<string, Func<DbEnum, ParamTypeEnum>> ColTypes { get; }
            = new ConcurrentDictionary<string, Func<DbEnum, ParamTypeEnum>>(
                new List<KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>>
                {
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "bigint",CTC.BigInt),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "binary",CTC.Binary),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "bit",CTC.Bit),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "blob",CTC.Blob),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "char",CTC.Char),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "date",CTC.Date),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "datetime",CTC.DateTime),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "decimal",CTC.Decimal),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "double",CTC.Double),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "enum",CTC.Enum),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "float",CTC.Float),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "int",CTC.Int),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "longblob",CTC.LongBlob),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "longtext",CTC.LongText),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "mediumblob",CTC.MediumBlob),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "mediumint",CTC.MediumInt),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "mediumtext",CTC.MediumText),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "set",CTC.Set),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "smallint",CTC.SmallInt),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "text",CTC.Text),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "time",CTC.Time),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "timestamp",CTC.TimeStamp),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "tinyblob",CTC.TinyBlob),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "tinyint",CTC.TinyInt),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "tinytext",CTC.TinyText),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "varbinary",CTC.VarBinary),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "varchar",CTC.VarChar),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "year",CTC.Year),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "nchar",CTC.NChar),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "nvarchar",CTC.NVarChar),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "ntext",CTC.NText),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "image",CTC.Image),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "numeric",CTC.Numeric),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "smallmoney",CTC.SmallMoney),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "money",CTC.Money),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "real",CTC.Real),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "datetime2",CTC.DateTime2),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "smalldatetime",CTC.SmallDateTime),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "datetimeoffset",CTC.DateTimeOffset),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "sql_variant",CTC.Sql_Variant),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "uniqueidentifier",CTC.UniqueIdentifier),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "xml",CTC.Xml),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "cursor",CTC.Cursor),
                    new KeyValuePair<string, Func<DbEnum, ParamTypeEnum>>( "table",CTC.Table)
                });
        internal static ConcurrentDictionary<Type, Func<Context, ParamTypeEnum, DbType>> DbTypeFuncs { get; }
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
        internal static ConcurrentDictionary<DbEnum, Func<Context, ISqlProvider>> DbProviders { get; }
            = new ConcurrentDictionary<DbEnum, Func<Context, ISqlProvider>>(
               new List<KeyValuePair<DbEnum, Func<Context, ISqlProvider>>>
               {
                   new KeyValuePair<DbEnum, Func<Context, ISqlProvider>>(DbEnum.MySQL,dc=>new MySqlProvider(dc)),
                   new KeyValuePair<DbEnum, Func<Context, ISqlProvider>>(DbEnum.SQLServer,dc=>new SqlServerProvider(dc))
               });

    }
}
