using MyDAL.Core.Bases;
using MyDAL.ModelTools;
using System.Text;

namespace MyDAL.DataRainbow.SQLServer
{
    internal abstract class SqlServer
        : SqlContext
    {

        protected static void LeftSquareBracket(StringBuilder sb)
        {
            sb.Append('[');
        }

        protected static void RightSquareBracket(StringBuilder sb)
        {
            sb.Append(']');
        }

        /*************************************************************************************************************************************************************/

        internal protected static void Column(string tbAlias, string colName, StringBuilder sb)
        {
            if (!tbAlias.IsNullStr())
            {
                sb.Append(tbAlias); Dot(sb);
            }
            LeftSquareBracket(sb); sb.Append(colName); RightSquareBracket(sb);
        }
    }
}
