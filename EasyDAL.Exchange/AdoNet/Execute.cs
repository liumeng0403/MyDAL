using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.MapperX;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.AdoNet
{
    internal static partial class SqlMapper
    {
        
        /// <summary>
        /// Execute a command asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <param name="cnn">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The number of rows affected.</returns>
        internal static Task<int> ExecuteAsync(IDbConnection cnn, string sql, object param = null) =>
            ExecuteAsync(cnn, new CommandDefinition(sql, param, null, null, CommandFlags.Buffered));
        
        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        internal static Task<T> ExecuteScalarAsync<T>(IDbConnection cnn, string sql, object param = null) =>
            ExecuteScalarImplAsync<T>(cnn, new CommandDefinition(sql, param, null, null, CommandFlags.Buffered));


    }
}
