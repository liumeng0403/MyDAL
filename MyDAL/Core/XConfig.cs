using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Common.Tools;
using MyDAL.Core.Configs;
using MyDAL.Core.Enums;
using MyDAL.DataRainbow.MySQL;
using MyDAL.DataRainbow.XCommon;
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
        
        internal static MySqlTypeConfig DTC { get; } = new MySqlTypeConfig();
        internal static ParamInfoConfig PIC { get; } = new ParamInfoConfig();
        internal static ExceptionConfig EC { get; } = new ExceptionConfig();
        internal static CsTypeConfig CSTC { get; } = new CsTypeConfig();
        internal static RuntimeInfo RI { get; } = new RuntimeInfo();

        /************************************************************************************************************/

        /// <summary>
        /// 连接类型
        /// </summary>
        internal static string ConnType { get; } = "MySqlConnection";
        
        internal static ConcurrentDictionary<string, ParamTypeEnum> ColTypes { get; }
            = new ConcurrentDictionary<string, ParamTypeEnum>(
                new List<KeyValuePair<string, ParamTypeEnum>>
                {
                    new KeyValuePair<string, ParamTypeEnum>( "bigint",ParamTypeEnum.BigInt_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "binary",ParamTypeEnum.Binary_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "bit",ParamTypeEnum.Bit_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "blob",ParamTypeEnum.Blob_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "char",ParamTypeEnum.Char_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "date",ParamTypeEnum.Date_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "datetime",ParamTypeEnum.DateTime_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "decimal",ParamTypeEnum.Decimal_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "double",ParamTypeEnum.Double_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "enum",ParamTypeEnum.Enum_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "float",ParamTypeEnum.Float_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "int",ParamTypeEnum.Int_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "longblob",ParamTypeEnum.LongBlob_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "longtext",ParamTypeEnum.LongText_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "mediumblob",ParamTypeEnum.MediumBlob_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "mediumint",ParamTypeEnum.MediumInt_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "mediumtext",ParamTypeEnum.MediumText_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "set",ParamTypeEnum.Set_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "smallint",ParamTypeEnum.SmallInt_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "text",ParamTypeEnum.Text_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "time",ParamTypeEnum.Time_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "timestamp",ParamTypeEnum.TimeStamp_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "tinyblob",ParamTypeEnum.TinyBlob_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "tinyint",ParamTypeEnum.TinyInt_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "tinytext",ParamTypeEnum.TinyText_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "varbinary",ParamTypeEnum.VarBinary_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "varchar",ParamTypeEnum.VarChar_MySQL_SqlServer),
                    new KeyValuePair<string, ParamTypeEnum>( "year",ParamTypeEnum.Year_MySQL),
                    new KeyValuePair<string, ParamTypeEnum>( "nchar",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "nvarchar",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "ntext",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "image",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "numeric",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "smallmoney",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "money",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "real",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "datetime2",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "smalldatetime",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "datetimeoffset",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "sql_variant",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "uniqueidentifier",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "xml",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "cursor",ParamTypeEnum.None),
                    new KeyValuePair<string, ParamTypeEnum>( "table",ParamTypeEnum.None)
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
        internal static ConcurrentDictionary<Type, ParamTypeEnum> DefaultParamTypes { get; }
            = new ConcurrentDictionary<Type, ParamTypeEnum>(
                new List<KeyValuePair<Type, ParamTypeEnum>>
                {
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Bool,ParamTypeEnum.Bit_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Byte,ParamTypeEnum.TinyInt_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.ByteArray,ParamTypeEnum.LongBlob_MySQL),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Char,ParamTypeEnum.VarChar_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Decimal,ParamTypeEnum.Decimal_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Double,ParamTypeEnum.Double_MySQL),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Float,ParamTypeEnum.Float_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Int,ParamTypeEnum.Int_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Long,ParamTypeEnum.BigInt_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Sbyte,ParamTypeEnum.TinyInt_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Short,ParamTypeEnum.SmallInt_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Uint,ParamTypeEnum.Int_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Ulong,ParamTypeEnum.BigInt_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Ushort,ParamTypeEnum.SmallInt_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.String,ParamTypeEnum.LongText_MySQL),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.DateTime,ParamTypeEnum.DateTime_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.TimeSpan,ParamTypeEnum.Time_MySQL_SqlServer),
                    new KeyValuePair<Type, ParamTypeEnum>(CSTC.Guid,ParamTypeEnum.Char_MySQL_SqlServer)
                });

    }
}
