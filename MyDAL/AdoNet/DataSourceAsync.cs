using HPC.DAL.AdoNet.Bases;
using HPC.DAL.Core;
using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HPC.DAL.AdoNet
{
    internal sealed class DataSourceAsync
        : DataSource
    {
        internal DataSourceAsync()
            : base()
        { }
        internal DataSourceAsync(Context dc)
            : base(dc)
        { }

        /*********************************************************************************************************************************************/

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
        private async Task ProcessDateTimeYearColumnAsync<F>(List<F> result, DicParam dic)
        {
            while (await Reader.ReadAsync())
            {
                result.Add(
                    DC.GH.ConvertT<F>(
                        new DateTime(
                            Reader.GetInt32(
                                Reader.GetOrdinal(
                                    dic.Option == OptionEnum.Column
                                        ? dic.TbCol
                                        : dic.Option == OptionEnum.ColumnAs
                                            ? dic.TbColAlias
                                            : throw DC.Exception(XConfig.EC._016, dic.Option.ToString()))), 1, 1).ToString(dic.Format)));
            }
        }
        private async Task<List<F>> ReadColumnAsync<F>()
        {
            var result = new List<F>();
            if (IsDateTimeYearColumn(out var dic))
            {
                await ProcessDateTimeYearColumnAsync(result, dic);
            }
            else if (DC.Crud == CrudEnum.SQL)
            {
                while (await Reader.ReadAsync(CancellationToken.None).ConfigureAwait(false))
                {
                    result.Add(DC.GH.ConvertT<F>(Reader.GetValue(0)));
                }
            }
            else
            {
                var col = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.Select && it.Columns != null);
                dic = col?.Columns.FirstOrDefault(it => it.Option == OptionEnum.Column);
                if (dic == null)
                {
                    throw new Exception("[[ReadColumn<F>()]] - 多表连接 - 单列 - 查询 - 异常 !!!");
                }
                var tbm = DC.XC.GetTableModel(dic.TbMType);
                var func = DC.XC.GetHandle(SqlOne, Reader, tbm.TbMType);
                var prop = tbm.TbMProps.FirstOrDefault(it => it.Name.Equals(dic.TbMProp, StringComparison.Ordinal));
                while (await Reader.ReadAsync(CancellationToken.None).ConfigureAwait(false))
                {
                    var obj = func(Reader);
                    result.Add(DC.GH.ConvertT<F>(prop.GetValue(obj)));
                }
            }
            return result;
        }
        private async Task<List<F>> ReadColumnAsync<M, F>(Func<M, F> propertyFunc)
            where M : class
        {
            var result = new List<F>();
            if (IsDateTimeYearColumn(out var dic))
            {
                await ProcessDateTimeYearColumnAsync(result, dic);
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
        private async Task<List<M>> ReadRowAsync<M>()
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
                                result.Data = await ReadColumnAsync<T>();
                            }
                            else
                            {
                                result.Data = await ReadColumnAsync(mapFunc);
                            }
                        }
                        else
                        {
                            result.Data = await ReadRowAsync<T>();
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
                        result = await ReadRowAsync<M>();
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
                        result = await ReadColumnAsync(propertyFunc);
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
                        result = await ReadColumnAsync<F>();
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
