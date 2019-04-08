using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.DataRainbow.MySQL;
using HPC.DAL.DataRainbow.SQLServer;
using HPC.DAL.DataRainbow.XCommon.Interfaces;
using System.Data;

namespace HPC.DAL.Core.Configs
{
    internal sealed class DbTypeConfig
        : IDbTypeConfig
    {
        private static IDbTypeConfig MySql { get; } = new MySqlTypeConfig();
        private static IDbTypeConfig SqlServer { get; } = new SqlServerTypeConfig();

        DbType IDbTypeConfig.IntProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.IntProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.IntProc(dc, colType);
                default:
                    return DbType.Int32;
            }
        }
        DbType IDbTypeConfig.LongProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.LongProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.LongProc(dc, colType);
                default:
                    return DbType.Int64;
            }
        }
        DbType IDbTypeConfig.DecimalProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.DecimalProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.DecimalProc(dc, colType);
                default:
                    return DbType.Decimal;
            }
        }
        DbType IDbTypeConfig.BoolProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.BoolProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.BoolProc(dc, colType);
                default:
                    return DbType.Boolean;
            }
        }
        DbType IDbTypeConfig.StringProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.StringProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.StringProc(dc, colType);
                default:
                    return DbType.AnsiString;
            }
        }
        DbType IDbTypeConfig.DateTimeProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.DateTimeProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.DateTimeProc(dc, colType);
                default:
                    return DbType.DateTime2;
            }
        }
        DbType IDbTypeConfig.GuidProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.GuidProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.GuidProc(dc, colType);
                default:
                    return DbType.Guid;
            }
        }
        DbType IDbTypeConfig.ByteProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.ByteProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.ByteProc(dc, colType);
                default:
                    return DbType.Byte;
            }
        }
        DbType IDbTypeConfig.ByteArrayProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.ByteArrayProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.ByteArrayProc(dc, colType);
                default:
                    return DbType.Binary;
            }
        }
        DbType IDbTypeConfig.CharProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.CharProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.CharProc(dc, colType);
                default:
                    return DbType.AnsiString;
            }
        }
        DbType IDbTypeConfig.DoubleProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.DoubleProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.DoubleProc(dc, colType);
                default:
                    return DbType.Double;
            }
        }
        DbType IDbTypeConfig.FloatProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.FloatProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.FloatProc(dc, colType);
                default:
                    return DbType.Single;
            }
        }
        DbType IDbTypeConfig.SbyteProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.SbyteProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.SbyteProc(dc, colType);
                default:
                    return DbType.SByte;
            }
        }
        DbType IDbTypeConfig.ShortProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.ShortProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.ShortProc(dc, colType);
                default:
                    return DbType.Int16;
            }
        }
        DbType IDbTypeConfig.UintProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.UintProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.UintProc(dc, colType);
                default:
                    return DbType.UInt32;
            }
        }
        DbType IDbTypeConfig.UlongProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.UlongProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.UlongProc(dc, colType);
                default:
                    return DbType.UInt64;
            }
        }
        DbType IDbTypeConfig.UshortProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.UshortProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.UshortProc(dc, colType);
                default:
                    return DbType.UInt16;
            }
        }
        DbType IDbTypeConfig.TimeSpanProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.TimeSpanProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.TimeSpanProc(dc, colType);
                default:
                    return DbType.Time;
            }
        }
        DbType IDbTypeConfig.DateTimeOffsetProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.DateTimeOffsetProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.DateTimeOffsetProc(dc, colType);
                default:
                    return DbType.DateTimeOffset;
            }
        }
        DbType IDbTypeConfig.ObjectProc(Context dc, ParamTypeEnum colType)
        {
            switch (dc.DB)
            {
                case DbEnum.MySQL:
                    return MySql.ObjectProc(dc, colType);
                case DbEnum.SQLServer:
                    return SqlServer.ObjectProc(dc, colType);
                default:
                    return DbType.Object;
            }
        }
    }
}
