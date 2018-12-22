using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;

namespace Yunyong.DataExchange.AdoNet
{
    internal class DataSource
    {

        private Context DC { get; set; }
        private IDbConnection Conn
        {
            get
            {
                return DC.Conn;
            }
        }
        private int SqlCount
        {
            get
            {
                return DC.SQL.Count;
            }
        }
        private string SqlOne
        {
            get
            {
                return DC.SQL[0];
            }
        }
        private string SqlTwo
        {
            get
            {
                return DC.SQL[1];
            }
        }
        private DbParamInfo Parameter
        {
            get
            {
                return DC.DPH.GetParameters(DC.Parameters);
            }
        }
        private DbDataReader Reader { get; set; }
        private bool IsDateTimeYearColumn(out DicParam dic)
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
                    && (it.CsType == XConfig.TC.DateTime || it.CsType == XConfig.TC.DateTimeNull)
                    && "%Y".Equals(it.Format, StringComparison.OrdinalIgnoreCase)
                    && (it.Option == OptionEnum.Column || it.Option == OptionEnum.ColumnAs));
            if (dic != null)
            {
                dic.Format = "yyyy";
                return true;
            }
            return false;
        }
        private async void ProcessDateTimeYearColumn<F>(List<F> result,DicParam dic)
        {
            while (await Reader.ReadAsync())
            {
                result.Add(
                    DC.GH.ConvertT<F>(
                        new DateTime(
                            Reader.GetInt32(
                                Reader.GetOrdinal(
                                    dic.Option == OptionEnum.Column
                                        ? dic.ColumnOne
                                        : dic.Option == OptionEnum.ColumnAs
                                            ? dic.ColumnOneAlias
                                            : throw new Exception($"{XConfig.EC._016} -- [[{dic.Option}]] 不能解析!!!"))), 1, 1).ToString(dic.Format)));
            }
        }
        private DbCommand SettingCommand(CommandInfo comm, IDbConnection cnn, Action<IDbCommand, DbParamInfo> paramReader)
        {
            var cmd = cnn.CreateCommand();
            cmd.CommandText = comm.CommandText;
            cmd.CommandTimeout = XConfig.CommandTimeout;
            cmd.CommandType = CommandType.Text;
            paramReader?.Invoke(cmd, comm.Parameter);
            return cmd as DbCommand;
        }
        internal async Task OpenAsync(IDbConnection cnn)
        {
            if (cnn is DbConnection dbConn)
            {
                await dbConn.OpenAsync();
            }
            else
            {
                throw new InvalidOperationException("请使用一个已打开连接的 IDbConnection 对象!!!");
            }
        }
        private static CommandBehavior DefaultAllowedCommandBehaviors { get; } = ~CommandBehavior.SingleResult;
        private CommandBehavior AllowedCommandBehaviors { get; set; } = DefaultAllowedCommandBehaviors;
        private CommandBehavior GetBehavior(bool close, CommandBehavior @default)
        {
            return (close ? (@default | CommandBehavior.CloseConnection) : @default) & AllowedCommandBehaviors;
        }
        private bool DisableCommandBehaviorOptimizations(CommandBehavior behavior, Exception ex)
        {
            if (AllowedCommandBehaviors == DefaultAllowedCommandBehaviors
                && (behavior & (CommandBehavior.SingleResult | CommandBehavior.SingleRow)) != 0)
            {
                if (ex.Message.Contains(nameof(CommandBehavior.SingleResult))
                    || ex.Message.Contains(nameof(CommandBehavior.SingleRow)))
                {
                    AllowedCommandBehaviors &= ~(CommandBehavior.SingleResult | CommandBehavior.SingleRow);
                    return true;
                }
            }
            return false;
        }
        private Task<DbDataReader> ExecuteReaderWithFlagsFallbackAsync(DbCommand cmd, bool wasClosed, CommandBehavior behavior)
        {
            var task = cmd.ExecuteReaderAsync(GetBehavior(wasClosed, behavior));
            if (task.Status == TaskStatus.Faulted && DisableCommandBehaviorOptimizations(behavior, task.Exception.InnerException))
            {
                return cmd.ExecuteReaderAsync(GetBehavior(wasClosed, behavior));
            }
            return task;
        }
        private async Task<List<F>> ReadColumn<M, F>(Func<M, F> propertyFunc)
            where M : class
        {
            var result = new List<F>();
            if (IsDateTimeYearColumn(out var dic))
            {
                ProcessDateTimeYearColumn(result, dic);
            }
            else
            {
                var func = DC.XC.GetHandle<M>(SqlOne, Reader);
                while (await Reader.ReadAsync())
                {
                    result.Add(propertyFunc(func(Reader)));
                }
            }
            while (await Reader.NextResultAsync()) { }
            return result;
        }
        private async Task<List<F>> ReadColumn<F>()
        {
            var result = new List<F>();
            if (IsDateTimeYearColumn(out var dic))
            {
                ProcessDateTimeYearColumn(result, dic);
            }
            else
            {
                var col = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.Select && it.Columns != null);
                dic = col?.Columns.FirstOrDefault(it => it.Option == OptionEnum.Column);
                if (dic == null)
                {
                    throw new Exception("[[ReadColumn<F>()]] - 多表连接 - 单列 - 查询 - 异常 !!!");
                }
                var func = DC.XC.GetHandle(SqlOne, Reader, DC.XC.GetModelType(dic.Key));
                var prop = DC.XC.GetModelProperys(dic.Key).FirstOrDefault(it => it.Name.Equals(dic.PropOne, StringComparison.Ordinal));
                while (await Reader.ReadAsync())
                {
                    var obj = func(Reader);
                    result.Add(DC.GH.ConvertT<F>(prop.GetValue(obj)));
                }
            }
            while (await Reader.NextResultAsync()) { }
            return result;
        }

        /*********************************************************************************************************************************************/

        internal DataSource() { }
        internal DataSource(Context dc)
        {
            DC = dc;
            DC.Method = UiMethodEnum.None;
        }

        /*********************************************************************************************************************************************/

        /*
         * ado.net -- DbCommand.[Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)]
         * select -- 所有行
         */
        internal async Task<List<M>> ExecuteReaderMultiRowAsync<M>()
        {
            var comm = new CommandInfo(SqlCount == 1 ? SqlOne : SqlTwo, Parameter);
            bool needClose = Conn.State == ConnectionState.Closed;
            using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
            {
                DbDataReader reader = null;
                try
                {
                    if (needClose) { await OpenAsync(Conn); }
                    reader = await ExecuteReaderWithFlagsFallbackAsync(cmd, needClose, XConfig.MultiRow);
                    if (reader.FieldCount == 0)
                    {
                        return new List<M>();
                    }
                    var func = DC.XC.GetHandle<M>(SqlCount == 1 ? SqlOne : SqlTwo, reader);
                    var result = new List<M>();
                    while (await reader.ReadAsync())
                    {
                        result.Add(func(reader));
                    }
                    while (await reader.NextResultAsync()) { }
                    return result;
                }
                finally
                {
                    using (reader) { }
                    if (needClose) { Conn.Close(); }
                }
            }
        }

        /*
         * ado.net -- DbCommand.[Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)]
         * select -- 单列
         */
        internal async Task<List<F>> ExecuteReaderSingleColumnAsync<M, F>(Func<M, F> propertyFunc)
            where M : class
        {
            var comm = new CommandInfo(SqlCount == 1 ? SqlOne : SqlTwo, Parameter);
            var needClose = Conn.State == ConnectionState.Closed;
            using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
            {
                try
                {
                    if (needClose) { await OpenAsync(Conn); }
                    Reader = await ExecuteReaderWithFlagsFallbackAsync(cmd, needClose, XConfig.MultiRow);
                    return await ReadColumn(propertyFunc);
                }
                finally
                {
                    using (Reader) { }
                    if (needClose) { Conn.Close(); }
                }
            }
        }

        /*
         * ado.net -- DbCommand.[Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)]
         * select -- 单列
         */
        internal async Task<List<F>> ExecuteReaderSingleColumnAsync<F>()
        {
            var comm = new CommandInfo(SqlCount == 1 ? SqlOne : SqlTwo, Parameter);
            var needClose = Conn.State == ConnectionState.Closed;
            using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
            {
                try
                {
                    if (needClose) { await OpenAsync(Conn); }
                    Reader = await ExecuteReaderWithFlagsFallbackAsync(cmd, needClose, XConfig.MultiRow);
                    return await ReadColumn<F>();
                }
                finally
                {
                    using (Reader) { }
                    if (needClose) { Conn.Close(); }
                }
            }
        }

        /*
         * ado.net -- DbCommand.[Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)]
         * Update,Insert,Delete -- 执行成功是返回值为该命令所影响的行数，如果影响的行数为0时返回的值为0，如果数据操作回滚得话返回值为-1
         * 对数据库结构的操作 -- 操作成功时返回的却是-1 , 操作失败的话（如数据表已经存在）往往会发生异常
         */
        internal async Task<int> ExecuteNonQueryAsync()
        {
            var comm = new CommandInfo(SqlOne, Parameter);
            var needClose = Conn.State == ConnectionState.Closed;
            using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
            {
                try
                {
                    if (needClose) { await OpenAsync(Conn); }
                    var result = await cmd.ExecuteNonQueryAsync();
                    return result;
                }
                finally
                {
                    if (needClose) { Conn.Close(); }
                }
            }
        }

        /* 
         * ado.net -- DbCommand.[Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)]
         * select -- 返回结果是查询后的第一行的第一列
         * 非select -- 返回一个未实例化的对象
         */
        internal async Task<T> ExecuteScalarAsync<T>()
        {
            var comm = new CommandInfo(SqlOne, Parameter);
            var cmd = default(DbCommand);
            var needClose = Conn.State == ConnectionState.Closed;
            var result = default(object);
            try
            {
                cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader);
                if (needClose) { await OpenAsync(Conn); }
                result = await cmd.ExecuteScalarAsync();
            }
            finally
            {
                if (needClose) { Conn.Close(); }
                cmd?.Dispose();
            }
            return DC.GH.ConvertT<T>(result);
        }

    }
}
