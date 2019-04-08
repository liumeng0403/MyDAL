using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
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
        : XSQL, ISql
    {

        private ISql DbSql { get; set; }
        internal MySql()
        {
            DbSql = this;
        }

        /*************************************************************************************************************************************************************/

        void ISql.ObjLeftSymbol(StringBuilder sb)
        {
            sb.Append('`');
        }
        void ISql.ObjRightSymbol(StringBuilder sb)
        {
            sb.Append('`');
        }

        /*************************************************************************************************************************************************************/

        void ISql.Top(Context dc, StringBuilder sb)
        {
            if (dc.PageIndex.HasValue
                && dc.PageSize.HasValue)
            {
                var start = default(int);
                if (dc.PageIndex > 0)
                {
                    start = ((dc.PageIndex - 1) * dc.PageSize).ToInt();
                }
                CRLF(sb); sb.Append("limit"); Spacing(sb); sb.Append(start); Comma(sb); sb.Append(dc.PageSize);
            }
        }
        void ISql.Column(string tbAlias, string colName, StringBuilder sb)
        {
            if (!tbAlias.IsNullStr())
            {
                sb.Append(tbAlias); XSQL.Dot(sb);
            }
            DbSql.ObjLeftSymbol(sb); sb.Append(colName); DbSql.ObjRightSymbol(sb);
        }
        void ISql.ColumnReplaceNullValueForSum(string tbAlias, string colName, StringBuilder sb)
        {
            sb.Append("ifnull");
            LeftRoundBracket(sb);
            DbSql.Column(tbAlias, colName, sb); Comma(sb); sb.Append('0');
            RightRoundBracket(sb);
        }
        void ISql.TableX(string table, StringBuilder sb)
        {
            DbSql.ObjLeftSymbol(sb); sb.Append(table); DbSql.ObjRightSymbol(sb);
        }
        void ISql.OneEqualOneProcess(DicParam p, StringBuilder sb)
        {
            Spacing(sb); DbParam(p.Param, sb);
        }
        void ISql.WhereTrueOrFalse(Context dc, bool flag, StringBuilder sb)
        {
            if (flag)
            {
                Action(ActionEnum.Where, sb, dc); Spacing(sb); sb.Append("true");
            }
            else
            {
                Action(ActionEnum.Where, sb, dc); Spacing(sb); sb.Append("false");
            }
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
        void ISql.Pager(Context dc, StringBuilder sb)
        {
            if (dc.PageIndex.HasValue
                && dc.PageSize.HasValue)
            {
                var start = default(int);
                if (dc.PageIndex > 0)
                {
                    start = ((dc.PageIndex - 1) * dc.PageSize).ToInt();
                }
                CRLF(sb); sb.Append("limit"); Spacing(sb); sb.Append(start); Comma(sb); sb.Append(dc.PageSize);
            }
        }
    }
}
