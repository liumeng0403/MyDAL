using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Extensions;
using Yunyong.DataExchange.Core.Helper;

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
        private DataSource() { }
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
            where M : class
        {
            var command = new CommandInfo(SqlCount == 1 ? SqlOne : SqlTwo, Parameter);
            var mType = typeof(M);
            bool needClose = Conn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(Conn, command.Parameter.ParamReader))
            {
                DbDataReader reader = null;
                try
                {
                    if (needClose)
                    {
                        await Conn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                    }
                    reader = await XSQL.ExecuteReaderWithFlagsFallbackAsync(
                        cmd,
                        needClose,
                        CommandBehavior.SequentialAccess | CommandBehavior.SingleResult,
                        default(CancellationToken)).ConfigureAwait(false);
                    if (reader.FieldCount == 0)
                    {
                        return new List<M>();
                    }

                    var func = DC.SC.GetHandle<M>(SqlCount == 1 ? SqlOne : SqlTwo, reader); // tuple.Func;
                    var result = new List<M>();
                    var convertToType = Nullable.GetUnderlyingType(mType) ?? mType;
                    while (await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false))
                    {
                        object val = func(reader);
                        result.Add((M)val);
                    }
                    while (await reader.NextResultAsync(default(CancellationToken)).ConfigureAwait(false))
                    { }
                    return result;
                }
                finally
                {
                    using (reader)
                    { }
                    if (needClose)
                    {
                        Conn.Close();
                    }
                }
            }
        }

        /*
         * ado.net -- DbCommand.[Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)]
         * select -- 第一行
         */
        internal async Task<M> ExecuteReaderSingleRowAsync<M>()
            where M : class
        {
            var command = new CommandInfo(SqlOne, Parameter);
            bool needClose = Conn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(Conn, command.Parameter.ParamReader))
            {
                DbDataReader reader = null;
                try
                {
                    if (needClose) { await Conn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false); }
                    reader = await XSQL.ExecuteReaderWithFlagsFallbackAsync(
                        cmd,
                        needClose,
                        CommandBehavior.SequentialAccess | CommandBehavior.SingleResult | CommandBehavior.SingleRow,
                        default(CancellationToken)).ConfigureAwait(false);
                    var result = default(M);
                    if (await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false) && reader.FieldCount != 0)
                    {
                        result = DC.SC.GetHandle<M>(SqlOne, reader)(reader);
                        while (await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false)) { }
                    }
                    while (await reader.NextResultAsync(default(CancellationToken)).ConfigureAwait(false)) { }
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
        internal async Task<IEnumerable<F>> ExecuteReaderSingleColumnAsync<M, F>(Func<M, F> propertyFunc)
            where M : class
        {
            var command = new CommandInfo(SqlOne, Parameter);
            var mType = typeof(M);
            bool needClose = Conn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(Conn, command.Parameter.ParamReader))
            {
                var reader = default(DbDataReader);
                try
                {
                    if (needClose)
                    {
                        await Conn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                    }

                    reader = await XSQL.ExecuteReaderWithFlagsFallbackAsync(
                        cmd,
                        needClose,
                        CommandBehavior.SequentialAccess | CommandBehavior.SingleResult,
                        default(CancellationToken)).ConfigureAwait(false);
                    var result = new List<F>();
                    var convertToType = Nullable.GetUnderlyingType(mType) ?? mType;
                    var func = DC.SC.GetHandle<M>(SqlOne, reader);
                    while (await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false))
                    {
                        result.Add(propertyFunc(func(reader)));
                    }
                    while (await reader.NextResultAsync(default(CancellationToken)).ConfigureAwait(false))
                    { /* ignore subsequent result sets */ }
                    return result;

                }
                finally
                {
                    using (reader) { /* dispose if non-null */ }
                    if (needClose)
                    {
                        Conn.Close();
                    }
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
            var command = new CommandInfo(SqlOne, Parameter);
            bool needClose = Conn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(Conn, command.Parameter.ParamReader))
            {
                try
                {
                    if (needClose)
                    {
                        await Conn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                    }
                    var result = await cmd.ExecuteNonQueryAsync(default(CancellationToken)).ConfigureAwait(false);
                    return result;
                }
                finally
                {
                    if (needClose)
                    {
                        Conn.Close();
                    }
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
            var command = new CommandInfo(SqlOne, Parameter);
            var cmd = default(DbCommand);
            bool needClose = Conn.State == ConnectionState.Closed;
            object result;
            try
            {
                cmd = command.TrySetupAsyncCommand(Conn, command.Parameter.ParamReader);
                if (needClose)
                {
                    await Conn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                }
                result = await cmd.ExecuteScalarAsync(default(CancellationToken)).ConfigureAwait(false);
            }
            finally
            {
                if (needClose)
                {
                    Conn.Close();
                }
                cmd?.Dispose();
            }
            return DC.GH.ConvertT<T>(result);
        }

    }
}
