using EasyDAL.Exchange.DataBase;
using EasyDAL.Exchange.MapperX;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.AdoNet
{
    internal static partial class SqlMapper
    {

        /// <summary>
        /// Execute a query asynchronously
        /// </summary>
        internal static Task<IEnumerable<T>> QueryAsync<T>(IDbConnection cnn, string sql, DynamicParameters param) =>
            QueryAsync<T>(cnn, typeof(T), new CommandDefinition(sql, param, CommandType.Text, CommandFlags.Buffered));

        /// <summary>
        /// Execute a single-row query asynchronously
        /// </summary>
        internal static Task<T> QueryFirstOrDefaultAsync<T>(IDbConnection cnn, string sql, DynamicParameters param) =>
            QueryRowAsync<T>(cnn, Row.FirstOrDefault, typeof(T), new CommandDefinition(sql, param, CommandType.Text, CommandFlags.None));       
        
    }
}
