using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MyDAL.AdoNet
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
        private async void ProcessDateTimeYearColumn<F>(List<F> result, DicParam dic)
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
                throw new InvalidOperationException("请使用一个继承自 DbConnection 对象的实例!!!");
            }
        }
        private Task<DbDataReader> ExecuteReaderWithRetryAsync(DbCommand cmd, CommandBehavior behavior)
        {
            var reader = cmd.ExecuteReaderAsync(behavior);
            if (reader.Status == TaskStatus.Faulted)
            {
                return cmd.ExecuteReaderAsync(behavior);
            }
            return reader;
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
                while (await Reader.ReadAsync(CancellationToken.None).ConfigureAwait(false))
                {
                    result.Add(propertyFunc(func(Reader)));
                }
            }
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
                var tbm = DC.XC.GetTableModel(dic.Key);
                var func = DC.XC.GetHandle(SqlOne, Reader, tbm.TbMType);
                var prop = tbm.TbMProps.FirstOrDefault(it => it.Name.Equals(dic.PropOne, StringComparison.Ordinal));
                while (await Reader.ReadAsync(CancellationToken.None).ConfigureAwait(false))
                {
                    var obj = func(Reader);
                    result.Add(DC.GH.ConvertT<F>(prop.GetValue(obj)));
                }
            }
            return result;
        }
        private async Task<List<M>> ReadRow<M>()
        {
            var result = new List<M>();
            if (Reader.FieldCount == 0)
            {
                return new List<M>();
            }
            var func = DC.XC.GetHandle<M>(SqlCount == 1 ? SqlOne : SqlTwo, Reader);
            while (await Reader.ReadAsync(CancellationToken.None).ConfigureAwait(false))
            {
                result.Add(func(Reader));
            }
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

        internal async Task<PagingResult<T>> ExecuteReaderPagingAsync<M, T>(bool single, Func<M, T> mapFunc)
            where M : class
        {
            var result = new PagingResult<T>();
            result.PageIndex = DC.PageIndex.Value;
            result.PageSize = DC.PageSize.Value;
            result.Data = new List<T>();
            bool needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { await OpenAsync(Conn); }
                var ci = new CommandInfo(SqlOne, Parameter);
                using (var cmdC = SettingCommand(ci, Conn, ci.Parameter.ParamReader))
                {
                    var obj = await cmdC.ExecuteScalarAsync();
                    result.TotalCount = DC.GH.ConvertT<long>(obj);
                }
                ci = new CommandInfo(SqlTwo, Parameter);
                using (var cmdD = SettingCommand(ci, Conn, ci.Parameter.ParamReader))
                {
                    using (Reader = await ExecuteReaderWithRetryAsync(cmdD, XConfig.MultiRow))
                    {
                        if (single)
                        {
                            if (mapFunc == null)
                            {
                                result.Data = await ReadColumn<T>();
                            }
                            else
                            {
                                result.Data = await ReadColumn(mapFunc);
                            }
                        }
                        else
                        {
                            result.Data = await ReadRow<T>();
                        }
                    }
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }
        internal async Task<List<M>> ExecuteReaderMultiRowAsync<M>()
        {
            var result = new List<M>();
            var ci = new CommandInfo(SqlOne, Parameter);
            bool needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { await OpenAsync(Conn); }
                using (var cmd = SettingCommand(ci, Conn, ci.Parameter.ParamReader))
                {
                    using (Reader = await ExecuteReaderWithRetryAsync(cmd, XConfig.MultiRow))
                    {
                        result = await ReadRow<M>();
                    }
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }
        internal async Task<List<F>> ExecuteReaderSingleColumnAsync<M, F>(Func<M, F> propertyFunc)
            where M : class
        {
            var result = new List<F>();
            var comm = new CommandInfo(SqlOne, Parameter);
            var needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { await OpenAsync(Conn); }
                using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
                {
                    using (Reader = await ExecuteReaderWithRetryAsync(cmd, XConfig.MultiRow))
                    {
                        result = await ReadColumn(propertyFunc);
                    }
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }
        internal async Task<List<F>> ExecuteReaderSingleColumnAsync<F>()
        {
            var result = new List<F>();
            var comm = new CommandInfo(SqlOne, Parameter);
            var needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { await OpenAsync(Conn); }
                using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
                {
                    using (Reader = await ExecuteReaderWithRetryAsync(cmd, XConfig.MultiRow))
                    {
                        result = await ReadColumn<F>();
                    }
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }
        internal async Task<int> ExecuteNonQueryAsync()
        {
            var result = default(int);
            var comm = new CommandInfo(SqlOne, Parameter);
            var needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { await OpenAsync(Conn); }
                using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
                {
                    result = await cmd.ExecuteNonQueryAsync();
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }
        internal async Task<T> ExecuteScalarAsync<T>()
            where T : struct
        {
            var result = default(T);
            var comm = new CommandInfo(SqlOne, Parameter);
            var needClose = Conn.State == ConnectionState.Closed;
            try
            {
                if (needClose) { await OpenAsync(Conn); }
                using (var cmd = SettingCommand(comm, Conn, comm.Parameter.ParamReader))
                {
                    var obj = await cmd.ExecuteScalarAsync();
                    result = DC.GH.ConvertT<T>(obj);
                }
            }
            finally
            {
                if (needClose) { Conn.Close(); }
            }
            return result;
        }

    }
}
