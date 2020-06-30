using System.Data;

namespace MyDAL.AdoNet
{
    internal class CommandInfo
    {
        internal string CommandText { get; }
        internal DbParamInfo Parameter { get; }
        internal CommandType CommandType { get; }

        internal CommandInfo(string sql, DbParamInfo paras)
        {
            CommandText = sql;
            Parameter = paras ?? new DbParamInfo();
            CommandType = CommandType.Text;
        }
    }
}
