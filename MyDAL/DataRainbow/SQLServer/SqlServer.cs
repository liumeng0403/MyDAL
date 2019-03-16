using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.ModelTools;
using System;
using System.Collections.Generic;
using System.Linq;
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

        internal protected void Column(string tbAlias, string colName, StringBuilder sb)
        {
            if (!tbAlias.IsNullStr())
            {
                sb.Append(tbAlias); Dot(sb);
            }
            LeftSquareBracket(sb); sb.Append(colName); RightSquareBracket(sb);
        }
        internal protected void TableX(string table, StringBuilder sb)
        {
            LeftSquareBracket(sb); sb.Append(table); RightSquareBracket(sb);
        }

        /*************************************************************************************************************************************************************/

        internal protected ColumnInfo GetIndex(List<ColumnInfo> cols)
        {
            return
                cols.FirstOrDefault(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)) ??
                cols.FirstOrDefault(it => "UNI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)) ??
                cols.FirstOrDefault(it => "MUL".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)) ??
                cols.FirstOrDefault(it => "NO".Equals(it.IsNullable, StringComparison.OrdinalIgnoreCase)) ??
                cols.FirstOrDefault();
        }

    }

}
