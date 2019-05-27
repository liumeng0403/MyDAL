using HPC.DAL.Core;
using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using System;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace HPC.DAL.AdoNet.Bases
{
    internal abstract class DataSource
    {

        internal protected Context DC { get; private set; }
        internal protected IDbConnection Conn { get; private set; }
        internal protected IDbTransaction Tran { get; private set; }
        internal protected int SqlCount
        {
            get
            {
                return DC.SQL.Count;
            }
        }
        internal protected string SqlOne
        {
            get
            {
                return DC.SQL[0];
            }
        }
        internal protected string SqlTwo
        {
            get
            {
                return DC.SQL[1];
            }
        }
        private DbParamInfo _dbParamInfo { get; set; }
        internal protected DbParamInfo Parameter
        {
            get
            {
                DC.OutPutSQL();
                if (!DC.IsSetParam)
                {
                    DC.IsSetParam = true;
                    _dbParamInfo = DC.DPH.GetParameters(DC.Parameters);
                    return _dbParamInfo;
                }
                else
                {
                    return _dbParamInfo;
                }
            }
        }
        internal protected DbDataReader Reader { get; set; }
        internal protected bool IsDateTimeYearColumn(out DicParam dic)
        {
            var col = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.Select && it.Columns != null);
            if (col == null)
            {
                dic = default(DicParam);
                return false;
            }
            dic = col
                .Columns
                .FirstOrDefault(it =>
                    it.Func == FuncEnum.ToString_CS_DateTime_Format
                    && (it.CsType == XConfig.CSTC.DateTime || it.CsType == XConfig.CSTC.DateTimeNull)
                    && "%Y".Equals(it.Format, StringComparison.OrdinalIgnoreCase)
                    && (it.Option == OptionEnum.Column || it.Option == OptionEnum.ColumnAs));
            if (dic != null)
            {
                dic.Format = "yyyy";
                return true;
            }
            return false;
        }
        internal protected DbCommand SettingCommand(CommandInfo comm, IDbConnection cnn, Action<IDbCommand, DbParamInfo> paramReader)
        {
            var cmd = cnn.CreateCommand();
            if (Tran != null)
            {
                cmd.Transaction = Tran;
            }
            cmd.CommandText = comm.CommandText;
            cmd.CommandTimeout = XConfig.CommandTimeout;
            cmd.CommandType = CommandType.Text;
            paramReader?.Invoke(cmd, comm.Parameter);
            return cmd as DbCommand;
        }

        /*********************************************************************************************************************************************/

        internal protected DataSource() { }
        internal protected DataSource(Context dc)
        {
            DC = dc;
            Conn = dc.XConn.Conn;
            Tran = dc.XConn.Tran;
            DC.Method = UiMethodEnum.None;
        }

    }
}
