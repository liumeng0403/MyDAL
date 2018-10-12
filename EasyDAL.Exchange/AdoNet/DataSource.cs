using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yunyong.DataExchange.Cache;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Extensions;
using Yunyong.DataExchange.Core.Helper;

namespace Yunyong.DataExchange.AdoNet
{
    internal class DataSource
        : ClassInstance<DataSource>
    {

        /*
         * ado.net -- DbCommand.[Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)]
         * select -- 所有行
         */
        internal async Task<IEnumerable<T>> ExecuteReaderMultiRowAsync<T>(IDbConnection cnn, string sql, DynamicParameters paramx) //=>
                                                                                                                                   //ExecuteReaderMultiRowImplAsync<T>(cnn, typeof(T), new CommandDefinition(sql, param, CommandType.Text, CommandFlags.Buffered));
        {
            var command = new CommandDefinition(sql, paramx, CommandType.Text, CommandFlags.Buffered);
            var effectiveType = typeof(T);
            DynamicParameters param = command.Parameters;
            var identity = new Identity(command.CommandText, command.CommandType, cnn, effectiveType, param?.GetType());
            var info = SqlHelper.GetCacheInfo(identity);
            bool needClose = cnn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
            {
                DbDataReader reader = null;
                try
                {
                    if (needClose)
                    {
                        await cnn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                    }
                    reader = await SqlHelper.ExecuteReaderWithFlagsFallbackAsync(cmd, needClose, CommandBehavior.SequentialAccess | CommandBehavior.SingleResult, default(CancellationToken)).ConfigureAwait(false);

                    var tuple = info.Deserializer;
                    int hash = SqlHelper.GetColumnHash(reader);
                    if (tuple.Func == null || tuple.Hash != hash)
                    {
                        if (reader.FieldCount == 0)
                        {
                            return Enumerable.Empty<T>();
                        }
                        info.Deserializer = new DeserializerState(hash, SqlHelper.GetDeserializer(effectiveType, reader, 0, -1, false));
                        tuple = info.Deserializer;
                    }

                    var func = tuple.Func;

                    if (command.Buffered)
                    {
                        var buffer = new List<T>();
                        var convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
                        while (await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false))
                        {
                            object val = func(reader);
                            if (val == null || val is T)
                            {
                                buffer.Add((T)val);
                            }
                            else
                            {
                                buffer.Add((T)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture));
                            }
                        }
                        while (await reader.NextResultAsync(default(CancellationToken)).ConfigureAwait(false))
                        { /* ignore subsequent result sets */ }
                        return buffer;
                    }
                    else
                    {
                        // can't use ReadAsync / cancellation; but this will have to do
                        needClose = false; // don't close if handing back an open reader; rely on the command-behavior
                        var deferred = SqlHelper.ExecuteReaderSync<T>(reader, func, command.Parameters);
                        reader = null; // to prevent it being disposed before the caller gets to see it
                        return deferred;
                    }
                }
                finally
                {
                    using (reader) { /* dispose if non-null */ }
                    if (needClose)
                    {
                        cnn.Close();
                    }
                }
            }
        }

        /*
         * ado.net -- DbCommand.[Task<DbDataReader> ExecuteReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)]
         * select -- 第一行
         */
        internal async Task<T> ExecuteReaderSingleRowAsync<T>(IDbConnection cnn, string sql, DynamicParameters paramx) //=>
                                                                                                                       //ExecuteReaderSingleRowImplAsync<T>(cnn, RowEnum.FirstOrDefault, typeof(T), new CommandDefinition(sql, param, CommandType.Text, CommandFlags.None));
        {
            var command = new CommandDefinition(sql, paramx, CommandType.Text, CommandFlags.None);
            var effectiveType = typeof(T);
            var row = RowEnum.FirstOrDefault;
            DynamicParameters param = command.Parameters;
            var identity = new Identity(command.CommandText, command.CommandType, cnn, effectiveType, param?.GetType());
            var info = SqlHelper.GetCacheInfo(identity);
            bool needClose = cnn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
            {
                DbDataReader reader = null;
                try
                {
                    if (needClose)
                    {
                        await cnn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                    }
                    reader = await SqlHelper.ExecuteReaderWithFlagsFallbackAsync(cmd, needClose, (row & RowEnum.Single) != 0
                    ? CommandBehavior.SequentialAccess | CommandBehavior.SingleResult // need to allow multiple rows, to check fail condition
                    : CommandBehavior.SequentialAccess | CommandBehavior.SingleResult | CommandBehavior.SingleRow, default(CancellationToken)).ConfigureAwait(false);

                    T result = default(T);
                    if (await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false) && reader.FieldCount != 0)
                    {
                        var tuple = info.Deserializer;
                        int hash = SqlHelper.GetColumnHash(reader);
                        if (tuple.Func == null || tuple.Hash != hash)
                        {
                            tuple = info.Deserializer = new DeserializerState(hash, SqlHelper.GetDeserializer(effectiveType, reader, 0, -1, false));
                        }

                        var func = tuple.Func;

                        object val = func(reader);
                        if (val == null || val is T)
                        {
                            result = (T)val;
                        }
                        else
                        {
                            var convertToType = Nullable.GetUnderlyingType(effectiveType) ?? effectiveType;
                            result = (T)Convert.ChangeType(val, convertToType, CultureInfo.InvariantCulture);
                        }
                        if ((row & RowEnum.Single) != 0 && await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false))
                        {
                            SqlHelper.ThrowMultipleRows(row);
                        }
                        while (await reader.ReadAsync(default(CancellationToken)).ConfigureAwait(false))
                        { /* ignore rows after the first */ }
                    }
                    else if ((row & RowEnum.FirstOrDefault) == 0) // demanding a row, and don't have one
                    {
                        SqlHelper.ThrowZeroRows(row);
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
                        cnn.Close();
                    }
                }
            }
        }

        /*
         * ado.net -- DbCommand.[Task<int> ExecuteNonQueryAsync(CancellationToken cancellationToken)]
         * Update,Insert,Delete -- 执行成功是返回值为该命令所影响的行数，如果影响的行数为0时返回的值为0，如果数据操作回滚得话返回值为-1
         * 对数据库结构的操作 -- 操作成功时返回的却是-1 , 操作失败的话（如数据表已经存在）往往会发生异常
         */
        internal async Task<int> ExecuteNonQueryAsync(IDbConnection cnn, string sql, DynamicParameters paramx) //=>
                                                                                                               //ExecuteNonQueryImplAsync(cnn, new CommandDefinition(sql, param, CommandType.Text, CommandFlags.Buffered));
        {
            var command = new CommandDefinition(sql, paramx, CommandType.Text, CommandFlags.Buffered);
            DynamicParameters param = command.Parameters;

            var identity = new Identity(command.CommandText, command.CommandType, cnn, null, param?.GetType());
            var info = SqlHelper.GetCacheInfo(identity);
            bool needClose = cnn.State == ConnectionState.Closed;
            using (var cmd = command.TrySetupAsyncCommand(cnn, info.ParamReader))
            {
                try
                {
                    if (needClose)
                    {
                        await cnn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                    }
                    var result = await cmd.ExecuteNonQueryAsync(default(CancellationToken)).ConfigureAwait(false);
                    return result;
                }
                finally
                {
                    if (needClose)
                    {
                        cnn.Close();
                    }
                }
            }
        }

        /* 
         * ado.net -- DbCommand.[Task<object> ExecuteScalarAsync(CancellationToken cancellationToken)]
         * select -- 返回结果是查询后的第一行的第一列
         * 非select -- 返回一个未实例化的对象
         */
        internal async Task<T> ExecuteScalarAsync<T>(IDbConnection cnn, string sql, DynamicParameters paramx) //=>
                                                                                                              //ExecuteScalarImplAsync<T>(cnn, new CommandDefinition(sql, param, CommandType.Text, CommandFlags.Buffered));
        {
            var command = new CommandDefinition(sql, paramx, CommandType.Text, CommandFlags.Buffered);
            Action<IDbCommand, DynamicParameters> paramReader = null;
            object param = command.Parameters;
            if (param != null)
            {
                var identity = new Identity(command.CommandText, command.CommandType, cnn, null, param.GetType());
                paramReader = SqlHelper.GetCacheInfo(identity).ParamReader;
            }

            DbCommand cmd = null;
            bool needClose = cnn.State == ConnectionState.Closed;
            object result;
            try
            {
                cmd = command.TrySetupAsyncCommand(cnn, paramReader);
                if (needClose)
                {
                    await cnn.TryOpenAsync(default(CancellationToken)).ConfigureAwait(false);
                }
                result = await cmd.ExecuteScalarAsync(default(CancellationToken)).ConfigureAwait(false);
            }
            finally
            {
                if (needClose)
                {
                    cnn.Close();
                }
                cmd?.Dispose();
            }
            return GenericHelper.ConvertT<T>(result);
        }

    }
}
