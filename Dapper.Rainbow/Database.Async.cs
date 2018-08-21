//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Dapper.DynamicParameter;
//using Dapper.Reader;

//namespace Dapper
//{
//    public abstract partial class Database<TDatabase> where TDatabase : Database<TDatabase>, new()
//    {
//        public partial class Table<T, TId>
//        {
//            /// <summary>
//            /// Insert a row into the db asynchronously.
//            /// </summary>
//            /// <param name="data">Either DynamicParameters or an anonymous type or concrete type.</param>
//            /// <returns>The Id of the inserted row.</returns>
//            public virtual async Task<int?> InsertAsync(dynamic data)
//            {
//                var o = (object)data;
//                List<string> paramNames = GetParamNames(o);
//                paramNames.Remove("Id");

//                string cols = string.Join(",", paramNames);
//                string colsParams = string.Join(",", paramNames.Select(p => "@" + p));
//                var sql = "set nocount on insert " + TableName + " (" + cols + ") values (" + colsParams + ") select cast(scope_identity() as int)";

//                return (await database.QueryAsync<int?>(sql, o).ConfigureAwait(false)).Single();
//            }

//            /// <summary>
//            /// Update a record in the DB asynchronously.
//            /// </summary>
//            /// <param name="id">The Id of the record to update.</param>
//            /// <param name="data">The new record.</param>
//            /// <returns>The number of affeced rows.</returns>
//            public Task<int> UpdateAsync(TId id, dynamic data)
//            {
//                List<string> paramNames = GetParamNames((object)data);

//                var builder = new StringBuilder();
//                builder.Append("update ").Append(TableName).Append(" set ");
//                builder.AppendLine(string.Join(",", paramNames.Where(n => n != "Id").Select(p => p + "= @" + p)));
//                builder.Append("where Id = @Id");

//                var parameters = new DynamicParameters(data);
//                parameters.Add("Id", id);

//                return database.ExecuteAsync(builder.ToString(), parameters);
//            }

//            /// <summary>
//            /// Asynchronously deletes a record for the DB.
//            /// </summary>
//            /// <param name="id">The Id of the record to delete.</param>
//            /// <returns>The number of rows affected.</returns>
//            public async Task<bool> DeleteAsync(TId id) =>
//                (await database.ExecuteAsync("delete from " + TableName + " where Id = @id", new { id }).ConfigureAwait(false)) > 0;

//            /// <summary>
//            /// Asynchronously gets a record with a particular Id from the DB.
//            /// </summary>
//            /// <param name="id">The primary key of the table to fetch.</param>
//            /// <returns>The record with the specified Id.</returns>
//            public Task<T> GetAsync(TId id) =>
//                database.QueryFirstOrDefaultAsync<T>("select * from " + TableName + " where Id = @id", new { id });

//            /// <summary>
//            /// Asynchronously gets the first row from this table (order determined by the database provider).
//            /// </summary>
//            /// <returns>Data from the first table row.</returns>
//            public virtual Task<T> FirstAsync() =>
//                database.QueryFirstOrDefaultAsync<T>("select top 1 * from " + TableName);

//            /// <summary>
//            /// Asynchronously gets the all rows from this table.
//            /// </summary>
//            /// <returns>Data from all table rows.</returns>
//            public Task<IEnumerable<T>> AllAsync() =>
//                database.QueryAsync<T>("select * from " + TableName);
//        }

//        /// <summary>
//        /// Asynchronously executes SQL against the current database.
//        /// </summary>
//        /// <param name="sql">The SQL to execute.</param>
//        /// <param name="param">The parameters to use.</param>
//        /// <returns>The number of rows affected.</returns>
//        public Task<int> ExecuteAsync(string sql, dynamic param = null) =>
//            _connection.ExecuteAsync(sql, param as object, _transaction, _commandTimeout);

//        /// <summary>
//        /// Asynchronously queries the current database.
//        /// </summary>
//        /// <typeparam name="T">The type to return.</typeparam>
//        /// <param name="sql">The SQL to execute.</param>
//        /// <param name="param">The parameters to use.</param>
//        /// <returns>An enumerable of <typeparamref name="T"/> for the rows fetched.</returns>
//        public Task<IEnumerable<T>> QueryAsync<T>(string sql, dynamic param = null) =>
//            _connection.QueryAsync<T>(sql, param as object, _transaction, _commandTimeout);

//        /// <summary>
//        /// Asynchronously queries the current database for a single record.
//        /// </summary>
//        /// <typeparam name="T">The type to return.</typeparam>
//        /// <param name="sql">The SQL to execute.</param>
//        /// <param name="param">The parameters to use.</param>
//        /// <returns>An enumerable of <typeparamref name="T"/> for the rows fetched.</returns>
//        public Task<T> QueryFirstOrDefaultAsync<T>(string sql, dynamic param = null) =>
//            _connection.QueryFirstOrDefaultAsync<T>(sql, param as object, _transaction, _commandTimeout);
        


//        /// <summary>
//        /// Execute a command that returns multiple result sets, and access each in turn.
//        /// </summary>
//        /// <param name="sql">The SQL to execute for this query.</param>
//        /// <param name="param">The parameters to use for this query.</param>
//        /// <param name="transaction">The transaction to use for this query.</param>
//        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
//        /// <param name="commandType">Is it a stored proc or a batch?</param>
//        public Task<GridReader> QueryMultipleAsync(string sql, dynamic param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) =>
//            SqlMapper.QueryMultipleAsync(_connection, sql, param, transaction, commandTimeout, commandType);
//    }
//}
