using MyDAL.Core.Bases;
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

        internal static MethodInfo EnumParse { get; } = typeof(Enum).GetMethod(nameof(Enum.Parse), new Type[] { typeof(Type), typeof(string), typeof(bool) });

        internal static CommandBehavior MultiRow { get; } = CommandBehavior.SequentialAccess | CommandBehavior.SingleResult;
        internal static CommandBehavior SingleRow { get; } = CommandBehavior.SequentialAccess | CommandBehavior.SingleResult | CommandBehavior.SingleRow;

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

        internal static ExceptionConfig EC { get; } = new ExceptionConfig();
        internal static CsTypeConfig TC { get; } = new CsTypeConfig();

        /************************************************************************************************************/

        internal static int CacheRetry { get; } = 3;

        /************************************************************************************************************/

        internal static ConcurrentDictionary<string, ParamTypeEnum> MySQLTypes { get; }
            = new ConcurrentDictionary<string, ParamTypeEnum>(new List<KeyValuePair<string, ParamTypeEnum>>
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
            = new ConcurrentDictionary<Type, Func<Context, ParamTypeEnum, DbType>>(new List<KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>>
            {
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Int,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_Int)
                    {
                        return DbType.Int32;
                    }
                    else
                    {
                        return DbType.Int32;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Long,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_BigInt)
                    {
                        return DbType.Int64;
                    }
                    else
                    {
                        return DbType.Int64;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Decimal,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_Decimal)
                    {
                        return DbType.Decimal;
                    }
                    else
                    {
                        return DbType.Decimal;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Bool,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_Bit)
                    {
                        return DbType.UInt16;
                    }
                    else
                    {
                        return DbType.Boolean;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.String,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_LongText)
                    {
                        return DbType.String;
                    }
                    else
                    {
                        return DbType.AnsiString;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.DateTime,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_DateTime)
                    {
                        return DbType.AnsiString;
                    }
                    else
                    {
                        return DbType.DateTime2;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Guid,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_Char)
                    {
                        return DbType.AnsiStringFixedLength;
                    }
                    else
                    {
                        return DbType.Guid;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Byte,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_TinyInt)
                    {
                        return DbType.Byte;
                    }
                    else
                    {
                        return DbType.Byte;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.ByteArray,(dc,colType)=>
                {
                    return DbType.Binary;
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Char,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_VarChar)
                    {
                        return DbType.AnsiString;
                    }
                    else
                    {
                        return DbType.AnsiString;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Double,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_Double)
                    {
                        return DbType.Double;
                    }
                    else
                    {
                        return DbType.Double;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Float,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_Float)
                    {
                        return DbType.Single;
                    }
                    else
                    {
                        return DbType.Single;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Sbyte,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_TinyInt)
                    {
                        return DbType.Int16;
                    }
                    else
                    {
                        return DbType.SByte;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Short,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_SmallInt)
                    {
                        return DbType.Int16;
                    }
                    else
                    {
                        return DbType.Int16;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Uint,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_Int)
                    {
                        return DbType.UInt32;
                    }
                    else
                    {
                        return DbType.UInt32;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Ulong,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_BigInt)
                    {
                        return DbType.UInt64;
                    }
                    else
                    {
                        return DbType.UInt64;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Ushort,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_SmallInt)
                    {
                        return DbType.UInt16;
                    }
                    else
                    {
                        return DbType.UInt16;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.TimeSpan,(dc,colType)=>
                {
                    if (colType == ParamTypeEnum.MySQL_Time)
                    {
                        return DbType.Time;
                    }
                    else
                    {
                        return DbType.Time;
                    }
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.DateTimeOffset,(dc,colType)=>
                {
                    return DbType.DateTimeOffset;
                }),
                new KeyValuePair<Type, Func<Context, ParamTypeEnum, DbType>>(TC.Object,(dc,colType)=>
                {
                    return DbType.Object;
                })
            });

    }
}
