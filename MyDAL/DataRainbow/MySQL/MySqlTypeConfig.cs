using HPC.DAL.Core.Bases;
using HPC.DAL.DataRainbow.XCommon.Interfaces;
using System.Data;

namespace HPC.DAL.DataRainbow.MySQL
{
    internal sealed class MySqlTypeConfig
        : IDbTypeConfig
    {

        DbType IDbTypeConfig.IntProc(Context dc, ParamTypeEnum colType)
        {
            if (colType == ParamTypeEnum.Int_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.BigInt_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.Decimal_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.Bit_MySQL_SqlServer
                || colType == ParamTypeEnum.TinyInt_MySQL_SqlServer
                || colType == ParamTypeEnum.Int_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.LongText_MySQL)
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
            if (colType == ParamTypeEnum.Set_MySQL)
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
            if (colType == ParamTypeEnum.DateTime_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.Char_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.TinyInt_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.VarChar_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.Double_MySQL)
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
            if (colType == ParamTypeEnum.Float_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.TinyInt_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.SmallInt_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.Int_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.BigInt_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.SmallInt_MySQL_SqlServer)
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
            if (colType == ParamTypeEnum.Time_MySQL_SqlServer)
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
