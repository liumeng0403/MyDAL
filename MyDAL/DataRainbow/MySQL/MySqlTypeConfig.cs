using MyDAL.Core.Bases;
using MyDAL.DataRainbow.XCommon.Interfaces;
using System.Data;

namespace MyDAL.DataRainbow.MySQL
{
    internal sealed class MySqlTypeConfig
        : IDbTypeConfig
    {

        DbType IDbTypeConfig.IntProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_Int)
            {
                return DbType.Int32;
            }
            else
            {
                return DbType.Int32;
            }
        }
        DbType IDbTypeConfig.LongProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_BigInt)
            {
                return DbType.Int64;
            }
            else
            {
                return DbType.Int64;
            }
        }
        DbType IDbTypeConfig.DecimalProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_Decimal)
            {
                return DbType.Decimal;
            }
            else
            {
                return DbType.Decimal;
            }
        }
        DbType IDbTypeConfig.BoolProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_Bit)
            {
                return DbType.UInt16;
            }
            else
            {
                return DbType.Boolean;
            }
        }
        DbType IDbTypeConfig.StringProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_LongText)
            {
                return DbType.String;
            }
            else
            {
                return DbType.AnsiString;
            }
        }
        DbType IDbTypeConfig.ListStringProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_Set)
            {
                return DbType.String;
            }
            else
            {
                return DbType.AnsiString;
            }
        }
        DbType IDbTypeConfig.DateTimeProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_DateTime)
            {
                return DbType.AnsiString;
            }
            else
            {
                return DbType.DateTime2;
            }
        }
        DbType IDbTypeConfig.GuidProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_Char)
            {
                return DbType.AnsiStringFixedLength;
            }
            else
            {
                return DbType.Guid;
            }
        }
        DbType IDbTypeConfig.ByteProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_TinyInt)
            {
                return DbType.Byte;
            }
            else
            {
                return DbType.Byte;
            }
        }
        DbType IDbTypeConfig.ByteArrayProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Binary;
        }
        DbType IDbTypeConfig.CharProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_VarChar)
            {
                return DbType.AnsiString;
            }
            else
            {
                return DbType.AnsiString;
            }
        }
        DbType IDbTypeConfig.DoubleProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_Double)
            {
                return DbType.Double;
            }
            else
            {
                return DbType.Double;
            }
        }
        DbType IDbTypeConfig.FloatProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_Float)
            {
                return DbType.Single;
            }
            else
            {
                return DbType.Single;
            }
        }
        DbType IDbTypeConfig.SbyteProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_TinyInt)
            {
                return DbType.Int16;
            }
            else
            {
                return DbType.SByte;
            }
        }
        DbType IDbTypeConfig.ShortProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_SmallInt)
            {
                return DbType.Int16;
            }
            else
            {
                return DbType.Int16;
            }
        }
        DbType IDbTypeConfig.UintProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_Int)
            {
                return DbType.UInt32;
            }
            else
            {
                return DbType.UInt32;
            }
        }
        DbType IDbTypeConfig.UlongProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_BigInt)
            {
                return DbType.UInt64;
            }
            else
            {
                return DbType.UInt64;
            }
        }
        DbType IDbTypeConfig.UshortProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_SmallInt)
            {
                return DbType.UInt16;
            }
            else
            {
                return DbType.UInt16;
            }
        }
        DbType IDbTypeConfig.TimeSpanProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.MySQL_Time)
            {
                return DbType.Time;
            }
            else
            {
                return DbType.Time;
            }
        }
        DbType IDbTypeConfig.DateTimeOffsetProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.DateTimeOffset;
        }
        DbType IDbTypeConfig.ObjectProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Object;
        }

    }
}
