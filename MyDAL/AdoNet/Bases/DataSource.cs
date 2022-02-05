using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace MyDAL.AdoNet.Bases
{
    internal abstract class DataSource
    {

        /// <summary>
        /// sql 操作 上下文
        /// </summary>
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
        internal protected DbCommand SettingCommand(
            CommandInfo comm, 
            IDbConnection cnn, 
            Action<IDbCommand, DbParamInfo> paramReader)
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

        internal protected void AutoPkProcess<M>(IEnumerable<M> mlist,DbCommand cmd)
        {
            var tm = DC.XC.GetTableModel(typeof(M));
            var ap = DC.XC.GetCommandModel(cmd.GetType());
            if ((DC.Method == UiMethodEnum.Create || DC.Method == UiMethodEnum.CreateBatch)
                && tm.HaveAutoIncrementPK)
            {
                var av = DC.GH.GetObjPropValue(ap.AutoValue, cmd);
                DC.GH.SetObjPropValue(mlist.FirstOrDefault(),tm.AutoIncrementPK.Prop,av);
            }
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
