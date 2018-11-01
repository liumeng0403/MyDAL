using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yunyong.DataExchange.Cache;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Extensions;
using Yunyong.DataExchange.Core.Helper;

namespace Yunyong.DataExchange.AdoNet
{
    internal class DataSource
    {

        /*
         * ado.net -- DbCommand.[Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)]
         * select -- 所有行
         */
        internal async Task<IEnumerable<M>> ExecuteReaderMultiRowAsync<M>(IDbConnection conn, string sql, DbParamInfo paramx)
            where M : class
        {
            paramx = paramx ?? new DbParamInfo();
            var command = new CommandInfo(sql, paramx);
            var mType = typeof(M);
            var param = command.Parameters;
            var identity = new Identity(command.CommandText, command.CommandType, conn, mType, param?.GetType());
            var info = AdoNetHelper.GetCacheInfo(identity);
            bool needClose = conn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(conn, info.ParamReader))
            {
                DbDataReader reader = null;
                try
                {
                    if (needClose)
                    {
                        await conn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                    }
                    reader = await AdoNetHelper.ExecuteReaderWithFlagsFallbackAsync(cmd, needClose, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult, default(CancellationToken)).ConfigureAwait(false);

                    var tuple = info.Deserializer;
                    int hash = AdoNetHelper.GetColumnHash(reader);
                    if (tuple.Func == null || tuple.Hash != hash)
                    {
                        if (reader.FieldCount == 0)
                        {
                            return Enumerable.Empty<M>();
                        }
                        info.Deserializer = new DeserializerState(hash, AdoNetHelper.GetDeserializer(mType, reader));
                        tuple = info.Deserializer;
                    }

                    var func = tuple.Func;
                    var result = new List<M>();
                    var convertToType = Nullable.GetUnderlyingType(mType) ?? mType;
                    while (await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false))
                    {
                        object val = func(reader);
                        if (val == null || val is M)
                        {
                            result.Add((M)val);
                        }
                        else
                        {
                            result.Add((M)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture));
                        }
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
                        conn.Close();
                    }
                }
            }
        }

        /*
         * ado.net -- DbCommand.[Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)]
         * select -- 第一行
         */
        internal async Task<T> ExecuteReaderSingleRowAsync<T>(IDbConnection conn, string sql, DbParamInfo paramx)
        {
            paramx = paramx ?? new DbParamInfo();
            var command = new CommandInfo(sql, paramx);
            var mType = typeof(T);
            var row = RowEnum.FirstOrDefault;
            var param = command.Parameters;
            var identity = new Identity(command.CommandText, command.CommandType, conn, mType, param?.GetType());
            var info = AdoNetHelper.GetCacheInfo(identity);
            bool needClose = conn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(conn, info.ParamReader))
            {
                DbDataReader reader = null;
                try
                {
                    if (needClose)
                    {
                        await conn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                    }
                    reader = await AdoNetHelper.ExecuteReaderWithFlagsFallbackAsync(cmd, needClose, (row & RowEnum.Single) != 0
                    ? CommandBehavior.SequentialAccess | CommandBehavior.SingleResult // need to allow multiple rows, to check fail condition
                    : CommandBehavior.SequentialAccess | CommandBehavior.SingleResult | CommandBehavior.SingleRow, default(CancellationToken)).ConfigureAwait(false);

                    T result = default(T);
                    if (await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false) && reader.FieldCount != 0)
                    {
                        var tuple = info.Deserializer;
                        int hash = AdoNetHelper.GetColumnHash(reader);
                        if (tuple.Func == null || tuple.Hash != hash)
                        {
                            tuple = info.Deserializer = new DeserializerState(hash, AdoNetHelper.GetDeserializer(mType, reader));
                        }

                        var func = tuple.Func;
                        object val = func(reader);
                        if (val == null || val is T)
                        {
                            result = (T)val;
                        }
                        else
                        {
                            var convertToType = Nullable.GetUnderlyingType(mType) ?? mType;
                            result = (T)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture);
                        }
                        if ((row & RowEnum.Single) != 0 && await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false))
                        {
                            AdoNetHelper.ThrowMultipleRows(row);
                        }
                        while (await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false))
                        { /* ignore rows after the first */ }
                    }
                    else if ((row & RowEnum.FirstOrDefault) == 0) // demanding a row, and don't have one
                    {
                        AdoNetHelper.ThrowZeroRows(row);
                    }
                    while (await reader.NextResultAsync(default(CancellationToken)).ConfigureAwait(false))
                    { /* ignore result sets after the first */ }
                    return result;
                }
                finally
                {
                    using (reader)
                    { /* dispose if non-null */ }
                    if (needClose)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /*
         * ado.net -- DbCommand.[Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)]
         * select -- 单列
         */
        internal async Task<IEnumerable<F>> ExecuteReaderSingleColumnAsync<M, F>(IDbConnection conn, string sql, DbParamInfo paramx, Func<M, F> propertyFunc)
            where M : class
        {
            paramx = paramx ?? new DbParamInfo();
            var command = new CommandInfo(sql, paramx);
            var effectiveType = typeof(M);
            var param = command.Parameters;
            var identity = new Identity(command.CommandText, command.CommandType, conn, effectiveType, param?.GetType());
            var info = AdoNetHelper.GetCacheInfo(identity);
            bool needClose = conn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(conn, info.ParamReader))
            {
                var reader = default(DbDataReader);
                try
                {
                    if (needClose)
                    {
                        await conn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                    }

                    reader = await AdoNetHelper.ExecuteReaderWithFlagsFallbackAsync(cmd, needClose, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult, default(CancellationToken)).ConfigureAwait(false);

                    var result = new List<F>();
                    var convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
                    var col = IL.Row(effectiveType, reader);
                    var m = default(M);
                    while (await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false))
                    {
                        var val = col.Handle(reader);
                        if (val == null
                            || val is M)
                        {
                            m = val as M;
                        }
                        else
                        {
                            m = (M)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture);
                        }
                        result.Add(propertyFunc(m));
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
                        conn.Close();
                    }
                }
            }
        }

        /*
         * ado.net -- DbCommand.[Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)]
         * Update,Insert,Delete -- 执行成功是返回值为该命令所影响的行数，如果影响的行数为0时返回的值为0，如果数据操作回滚得话返回值为-1
         * 对数据库结构的操作 -- 操作成功时返回的却是-1 , 操作失败的话（如数据表已经存在）往往会发生异常
         */
        internal async Task<int> ExecuteNonQueryAsync(IDbConnection conn, string sql, DbParamInfo paramx)
        {
            paramx = paramx ?? new DbParamInfo();
            var command = new CommandInfo(sql, paramx);
            var param = command.Parameters;

            var identity = new Identity(command.CommandText, command.CommandType, conn, null, param?.GetType());
            var info = AdoNetHelper.GetCacheInfo(identity);
            bool needClose = conn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(conn, info.ParamReader))
            {
                try
                {
                    if (needClose)
                    {
                        await conn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                    }
                    var result = await cmd.ExecuteNonQueryAsync(default(CancellationToken)).ConfigureAwait(false);
                    return result;
                }
                finally
                {
                    if (needClose)
                    {
                        conn.Close();
                    }
                }
            }
        }

        /* 
         * ado.net -- DbCommand.[Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)]
         * select -- 返回结果是查询后的第一行的第一列
         * 非select -- 返回一个未实例化的对象
         */
        internal async Task<T> ExecuteScalarAsync<T>(IDbConnection conn, string sql, DbParamInfo paramx)
        {
            paramx = paramx ?? new DbParamInfo();
            var command = new CommandInfo(sql, paramx);
            Action<IDbCommand, DbParamInfo> paramReader = null;
            object param = command.Parameters;
            if (param != null)
            {
                var identity = new Identity(command.CommandText, command.CommandType, conn, null, param.GetType());
                paramReader = AdoNetHelper.GetCacheInfo(identity).ParamReader;
            }

            DbCommand cmd = null;
            bool needClose = conn.State == ConnectionState.Closed;
            object result;
            try
            {
                cmd = command.TrySetupAsyncCommand(conn, paramReader);
                if (needClose)
                {
                    await conn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                }
                result = await cmd.ExecuteScalarAsync(default(CancellationToken)).ConfigureAwait(false);
            }
            finally
            {
                if (needClose)
                {
                    conn.Close();
                }
                cmd?.Dispose();
            }
            return GenericHelper.ConvertT<T>(result);
        }

    }
}
