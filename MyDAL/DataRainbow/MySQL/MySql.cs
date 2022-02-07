using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.DataRainbow.XCommon.Bases;
using MyDAL.DataRainbow.XCommon.Interfaces;
using MyDAL.ModelTools;
using MyDAL.Tools;
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
        void ISql.ParamSymbol(StringBuilder sb)
        {
            sb.Append(QuestionMark);
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
                DbSql.TableXAlias(tbAlias, sb); Dot(sb);
            }

            if ("*".Equals(colName, StringComparison.OrdinalIgnoreCase))
            {
                sb.Append(colName);
            }
            else
            {
                DbSql.ObjLeftSymbol(sb); sb.Append(colName); DbSql.ObjRightSymbol(sb);
            }
        }
        void ISql.ColumnAlias(string colAlias, StringBuilder sb)
        {
            DbSql.ObjLeftSymbol(sb); sb.Append(colAlias); DbSql.ObjRightSymbol(sb);
        }
        void ISql.ColumnReplaceNullValueForSum(string tbAlias, string colName, StringBuilder sb)
        {
            sb.Append("ifnull");
            LeftRoundBracket(sb);
            DbSql.Column(tbAlias, colName, sb); Comma(sb); sb.Append('0');
            RightRoundBracket(sb);
        }
        void ISql.TableX(string tbName, StringBuilder sb)
        {
            DbSql.ObjLeftSymbol(sb); sb.Append(tbName); DbSql.ObjRightSymbol(sb);
        }
        void ISql.TableXAlias(string tbAlias, StringBuilder sb)
        {
            DbSql.ObjLeftSymbol(sb); sb.Append(tbAlias); DbSql.ObjRightSymbol(sb);
        }
        void ISql.MultiAction(ActionEnum action, StringBuilder sb, Context dc)
        {
            if (action == ActionEnum.And)
            {
                Spacing(sb); sb.Append("&&"); Spacing(sb);
            }
            else if (action == ActionEnum.Or)
            {
                Spacing(sb); sb.Append("||"); Spacing(sb);
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._010, action.ToString());
            }
        }
        void ISql.DbParam(string param, StringBuilder sb)
        {
            DbSql.ParamSymbol(sb); sb.Append(param);
        }
        void ISql.OneEqualOneProcess(DicParam p, StringBuilder sb)
        {
            Spacing(sb); DbSql.DbParam(p.Param, sb);
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
