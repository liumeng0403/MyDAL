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
        internal protected IDbConnection Conn
        {
            get
            {
                return DC.Conn;
            }
        }
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
        internal protected DbParamInfo Parameter
        {
            get
            {
                var paras = DC.DPH.GetParameters(DC.Parameters);

                //
                if (XConfig.IsDebug
                    && DC.Parameters != null
                    && DC.Parameters.Count > 0)
                {
                    DC.SetValue();
                }

                return paras;
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
                    it.Func == FuncEnum.DateFormat
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
            DC.Method = UiMethodEnum.None;
        }

    }
}
