using MyDAL.Core.Common;
using MyDAL.DataRainbow.XCommon.Bases;
using MyDAL.DataRainbow.XCommon.Interfaces;
using MyDAL.ModelTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDAL.DataRainbow.MySQL
{
    internal sealed class MySql
        : ISql
    {
        internal static void Backquote(StringBuilder sb)
        {
            sb.Append('`');
        }

        /*************************************************************************************************************************************************************/
   
        void ISql.Column(string tbAlias, string colName, StringBuilder sb)
        {
            if (!tbAlias.IsNullStr())
            {
                sb.Append(tbAlias); XSQL.Dot(sb);
            }
            Backquote(sb); sb.Append(colName); Backquote(sb);
        }
        void ISql.TableX(string table, StringBuilder sb)
        {
            Backquote(sb); sb.Append(table); Backquote(sb);
        }

        /*************************************************************************************************************************************************************/

        ColumnInfo ISql.GetIndex(List<ColumnInfo> cols)
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
