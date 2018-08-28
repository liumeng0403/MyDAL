using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.DynamicParameter;
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
        /// Execute a command asynchronously
        /// </summary>
        internal static Task<int> ExecuteAsync(IDbConnection cnn, string sql, DynamicParameters param) =>
            ExecuteAsync(cnn, new CommandDefinition(sql, param, CommandType.Text, CommandFlags.Buffered));
        
        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        internal static Task<T> ExecuteScalarAsync<T>(IDbConnection cnn, string sql, DynamicParameters param) =>
            ExecuteScalarImplAsync<T>(cnn, new CommandDefinition(sql, param, CommandType.Text, CommandFlags.Buffered));


    }
}
