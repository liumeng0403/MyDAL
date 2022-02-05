using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Common.Tools;
using MyDAL.Core.Configs;
using MyDAL.Core.Enums;
using MyDAL.DataRainbow.MySQL;
using MyDAL.DataRainbow.XCommon;
using MyDAL.DataRainbow.XCommon.Interfaces;
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
        internal static int CommandTimeout { get; set; } = 30;  // s 

        internal static MethodInfo EnumParse { get; } = typeof(Enum).GetMethod(nameof(Enum.Parse), new Type[] { typeof(Type), typeof(string), typeof(bool) });

        internal static CommandBehavior MultiRow { get; } = CommandBehavior.SequentialAccess | CommandBehavior.SingleResult;

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
        internal static CsTypeConfig CSTC { get; } = new CsTypeConfig();
        internal static SqlParamDefaultType SPDT { get; } = new SqlParamDefaultType();
        internal static RuntimeInfo RI { get; } = new RuntimeInfo();

        /************************************************************************************************************/

        internal static ConcurrentDictionary<string, DbEnum> ConnTypes { get; }
            = new ConcurrentDictionary<string, DbEnum>(
               new List<KeyValuePair<string, DbEnum>>
               {
                   // MySqlConnection 
                   new KeyValuePair<string, DbEnum>("MySql.Data.MySqlClient.MySqlConnection",DbEnum.MySQL),
                   new KeyValuePair<string, DbEnum>("Devart.Data.MySql.MySqlConnection",DbEnum.MySQL)
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
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Int,DTC.IntProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Long,DTC.LongProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Decimal,DTC.DecimalProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Bool,DTC.BoolProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.String,DTC.StringProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.ListString,DTC.ListStringProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.DateTime,DTC.DateTimeProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Guid,DTC.GuidProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Byte,DTC.ByteProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.ByteArray,DTC.ByteArrayProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Char,DTC.CharProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Double,DTC.DoubleProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Float,DTC.FloatProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Sbyte,DTC.SbyteProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Short,DTC.ShortProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Uint,DTC.UintProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Ulong,DTC.UlongProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Ushort,DTC.UshortProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.TimeSpan,DTC.TimeSpanProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.DateTimeOffset,DTC.DateTimeOffsetProc),
                    new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(CSTC.Object,DTC.ObjectProc)
                });
        internal static ConcurrentDictionary<Type, Func<DicParam, Type, Context, ParamInfo>> ParamFuncs { get; }
            = new ConcurrentDictionary<Type, Func<DicParam, Type, Context, ParamInfo>>(
                new List<KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>>
                {
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Int,PIC.IntParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Long,PIC.LongParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Decimal,PIC.DecimalParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Bool,PIC.BoolParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.String,PIC.StringParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.ListString,PIC.ListStringParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.DateTime,PIC.DateTimeParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Guid,PIC.GuidParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Byte,PIC.ByteParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.ByteArray,PIC.ByteArryParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Char,PIC.CharParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Double,PIC.DoubleParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Float,PIC.FloatParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Sbyte,PIC.SbyteParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Short,PIC.ShortParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Uint,PIC.UintParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Ulong,PIC.UlongParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.Ushort,PIC.UshortParam),
                    new KeyValuePair<Type, Func<DicParam, Type, Context, ParamInfo>>(CSTC.TimeSpan,PIC.TimeSpanParam)
                });
        internal static ConcurrentDictionary<Type, Func<DbEnum, ParamTypeEnum>> DefaultParamTypes { get; }
            = new ConcurrentDictionary<Type, Func<DbEnum, ParamTypeEnum>>(
                new List<KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>>
                {
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Bool,SPDT.BoolProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Byte,SPDT.ByteProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.ByteArray,SPDT.ByteArrayProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Char,SPDT.CharProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Decimal,SPDT.DecimalProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Double,SPDT.DoubleProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Float,SPDT.FloatProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Int,SPDT.IntProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Long,SPDT.LongProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Sbyte,SPDT.SbyteProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Short,SPDT.ShortProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Uint,SPDT.UintProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Ulong,SPDT.UlongProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Ushort,SPDT.UshortProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.String,SPDT.StringProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.DateTime,SPDT.DateTimeProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.TimeSpan,SPDT.TimeSpanProc),
                    new KeyValuePair<Type, Func<DbEnum, ParamTypeEnum>>(CSTC.Guid,SPDT.GuidProc)
                });

    }
}
