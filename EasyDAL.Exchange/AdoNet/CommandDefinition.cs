using System;
using System.Data;
using Yunyong.DataExchange.Core;

namespace Yunyong.DataExchange.AdoNet
{
    /// <summary>
    /// sql 操作定义
    /// </summary>
    internal struct CommandDefinition
    {

        /// <summary>
        /// The command (sql or a stored-procedure name) to execute
        /// </summary>
        public string CommandText { get; }

        /// <summary>
        /// The parameters associated with the command
        /// </summary>
        public DbParameters Parameters { get; }

        /// <summary>
        /// The type of command that the command-text represents
        /// </summary>
        public CommandType CommandType { get; }

        /// <summary>
        /// Should data be buffered before returning?
        /// </summary>
        public bool Buffered => (Flags & CommandFlags.Buffered) != 0;

        /// <summary>
        /// Additional state flags against this command
        /// </summary>
        public CommandFlags Flags { get; }

        /// <summary>
        /// Initialize the command definition
        /// </summary>
        /// <param name="commandText">The text for this command.</param>
        /// <param name="parameters">The parameters for this command.</param>
        /// <param name="transaction">The transaction for this command to participate in.</param>
        /// <param name="commandTimeout">The timeout (in seconds) for this command.</param>
        /// <param name="commandType">The <see cref="CommandType"/> for this command.</param>
        /// <param name="flags">The behavior flags for this command.</param>
        /// <param name="cancellationToken">The cancellation token for this command.</param>
        public CommandDefinition(string commandText, DbParameters parameters, CommandType commandType, CommandFlags flags = CommandFlags.Buffered)
        {
            CommandText = commandText;
            Parameters = parameters;
            CommandType = commandType;
            Flags = flags;
        }

        //internal IDbCommand SetupCommand(IDbConnection cnn, Action<IDbCommand, DbParameters> paramReader)
        //{
        //    var cmd = cnn.CreateCommand();
        //    cmd.CommandText = CommandText;
        //    cmd.CommandTimeout = XConfig.CommandTimeout;
        //    cmd.CommandType = CommandType;
        //    paramReader?.Invoke(cmd, Parameters);
        //    return cmd;
        //}

    }
}
