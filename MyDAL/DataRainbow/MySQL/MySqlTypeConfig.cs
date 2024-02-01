﻿using MyDAL.Core.Bases;
using System.Data;

namespace MyDAL.DataRainbow.MySQL
{
    internal sealed class MySqlTypeConfig
    {

        internal DbType IntProc(Context dc, ParamTypeEnum colType)
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
        internal DbType LongProc(Context dc, ParamTypeEnum colType)
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
        internal DbType DecimalProc(Context dc, ParamTypeEnum colType)
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
        internal DbType BoolProc(Context dc, ParamTypeEnum colType)
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
        internal DbType StringProc(Context dc, ParamTypeEnum colType)
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
        internal DbType ListStringProc(Context dc, ParamTypeEnum colType)
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
        internal DbType DateTimeProc(Context dc, ParamTypeEnum colType)
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
        internal DbType GuidProc(Context dc, ParamTypeEnum colType)
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
        internal DbType ByteProc(Context dc, ParamTypeEnum colType)
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
        internal DbType ByteArrayProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Binary;
        }
        internal DbType CharProc(Context dc, ParamTypeEnum colType)
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
        internal DbType DoubleProc(Context dc, ParamTypeEnum colType)
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
        internal DbType FloatProc(Context dc, ParamTypeEnum colType)
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
        internal DbType SbyteProc(Context dc, ParamTypeEnum colType)
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
        internal DbType ShortProc(Context dc, ParamTypeEnum colType)
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
        internal DbType UintProc(Context dc, ParamTypeEnum colType)
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
        internal DbType UlongProc(Context dc, ParamTypeEnum colType)
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
        internal DbType UshortProc(Context dc, ParamTypeEnum colType)
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
        internal DbType TimeSpanProc(Context dc, ParamTypeEnum colType)
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
        internal DbType DateTimeOffsetProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.DateTimeOffset;
        }
        internal DbType ObjectProc(Context dc, ParamTypeEnum colType)
        {
            return DbType.Object;
        }

    }
}
