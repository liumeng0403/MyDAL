using System.Data;

namespace Yunyong.DataExchange.AdoNet
{
    internal struct CommandInfo
    {
        internal string CommandText { get; }
        internal DbParamInfo Parameters { get; }
        internal CommandType CommandType { get; }
        
        internal CommandInfo(string sql, DbParamInfo paras)
        {
            CommandText = sql;
            Parameters = paras;
            CommandType = CommandType.Text;
        }
    }
}
