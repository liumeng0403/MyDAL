using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Extensions;
using MyDAL.DataRainbow.XCommon.Bases;
using MyDAL.DataRainbow.XCommon.Interfaces;
using MyDAL.ModelTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDAL.DataRainbow.SQLServer
{
    internal sealed class SqlServer
        : XSQL, ISql
    {

        internal static void LeftSquareBracket(StringBuilder sb)
        {
            sb.Append('[');
        }
        internal static void RightSquareBracket(StringBuilder sb)
        {
            sb.Append(']');
        }

        /*************************************************************************************************************************************************************/

        void ISql.Top(Context dc, StringBuilder sb)
        {
            if (dc.PageIndex.HasValue
                && dc.PageSize.HasValue)
            {
                CRLF(sb); Tab(sb); sb.Append("top"); Spacing(sb); sb.Append(dc.PageSize);
            }
        }
        void ISql.Column(string tbAlias, string colName, StringBuilder sb)
        {
            if (!tbAlias.IsNullStr())
            {
                sb.Append(tbAlias); XSQL.Dot(sb);
            }
            LeftSquareBracket(sb); sb.Append(colName); RightSquareBracket(sb);
        }
        void ISql.TableX(string table, StringBuilder sb)
        {
            LeftSquareBracket(sb); sb.Append(table); RightSquareBracket(sb);
        }
        void ISql.OneEqualOneProcess(DicParam p, StringBuilder sb)
        {
            Spacing(sb); sb.Append("1=1");
        }
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
