using MyDAL.Core.Bases;
using System.Data;

namespace MyDAL.DataRainbow.XCommon.Interfaces
{
    internal interface IDbTypeConfig
    {
        DbType IntProc(Context dc, ParamTypeEnum colType);
        DbType LongProc(Context dc, ParamTypeEnum colType);
        DbType DecimalProc(Context dc, ParamTypeEnum colType);
        DbType BoolProc(Context dc, ParamTypeEnum colType);
        DbType StringProc(Context dc, ParamTypeEnum colType);
        DbType DateTimeProc(Context dc, ParamTypeEnum colType);
        DbType GuidProc(Context dc, ParamTypeEnum colType);
        DbType ByteProc(Context dc, ParamTypeEnum colType);
        DbType ByteArrayProc(Context dc, ParamTypeEnum colType);
        DbType CharProc(Context dc, ParamTypeEnum colType);
        DbType DoubleProc(Context dc, ParamTypeEnum colType);
        DbType FloatProc(Context dc, ParamTypeEnum colType);
        DbType SbyteProc(Context dc, ParamTypeEnum colType);
        DbType ShortProc(Context dc, ParamTypeEnum colType);
        DbType UintProc(Context dc, ParamTypeEnum colType);
        DbType UlongProc(Context dc, ParamTypeEnum colType);
        DbType UshortProc(Context dc, ParamTypeEnum colType);
        DbType TimeSpanProc(Context dc, ParamTypeEnum colType);
        DbType DateTimeOffsetProc(Context dc, ParamTypeEnum colType);
        DbType ObjectProc(Context dc, ParamTypeEnum colType);
    }
}
