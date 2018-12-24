using System.Data;

namespace Yunyong.DataExchange.AdoNet
{
    internal class CommandInfo
    {
        internal string CommandText { get; }
        internal DbParamInfo Parameter { get; }
        internal CommandType CommandType { get; }
        
        internal CommandInfo(string sql, DbParamInfo paras)
        {
            CommandText = sql;
            Parameter = paras;
            CommandType = CommandType.Text;
        }
    }
}
