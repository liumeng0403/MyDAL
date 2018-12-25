using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System;

namespace MyDAL.DataRainbow.MySQL
{
    internal abstract class MySql
        : SqlContext
    {
        internal protected static void DbParam(string param, StringBuilder sb)
        {
            At(sb); sb.Append(param);
        }
        internal protected static void Column(string tbAlias, string colName, StringBuilder sb)
        {
            if (!tbAlias.IsNullStr())
            {
                sb.Append(tbAlias); Dot(sb);
            }
            Backquote(sb); sb.Append(colName); Backquote(sb);
        }
        internal protected static void TableX(string table, StringBuilder sb)
        {
            Backquote(sb); sb.Append(table); Backquote(sb);
        }

        /*************************************************************************************************************************************************************/

        internal protected static ColumnInfo GetIndex(List<ColumnInfo> cols)
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
