using MyDAL.Core.Bases;
using System.Text;

namespace MyDAL.DataRainbow.MySQL
{
    internal abstract class MySql
        : SqlContext
    {
        internal protected static void Column(string col, StringBuilder sb)
        {
            Backquote(sb); sb.Append(col); Backquote(sb);
        }
        internal protected static void TableX(string table,StringBuilder sb)
        {
            Backquote(sb);sb.Append(table);Backquote(sb);
        }
    }
}
