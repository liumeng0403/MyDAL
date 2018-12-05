using System.Text;
using Yunyong.DataExchange.Core.Bases;

namespace Yunyong.DataExchange.DataRainbow.MySQL
{
    internal abstract class MySql
        : SqlContext
    {
        internal protected static void Column(string col, StringBuilder sb)
        {
            Backquote(sb);
            sb.Append(col);
            Backquote(sb);
        }
    }
}
