using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.DataBase;
using EasyDAL.Exchange.MapperX;
using EasyDAL.Exchange.Reader;

namespace EasyDAL.Exchange.AdoNet
{
    internal static partial class SqlMapper
    {
                
        /// <summary>
        /// Execute a query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="cnn">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of <typeparamref name="T"/>; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is
        /// created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        internal static Task<IEnumerable<T>> QueryAsync<T>(IDbConnection cnn, string sql, object param = null) =>
            QueryAsync<T>(cnn, typeof(T), new CommandDefinition(sql, param, null, null, CommandFlags.Buffered));
        
        /// <summary>
        /// Execute a single-row query asynchronously using .NET 4.5 Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="cnn">The connection to query on.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        internal static Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection cnn, string sql, object param = null) =>
            QueryRowAsync<T>(cnn, Row.FirstOrDefault, typeof(T), new CommandDefinition(sql, param, null, null, CommandFlags.None));       
        
    }
}
