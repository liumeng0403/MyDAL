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
        : XSQL
    {
        
        internal MySql()
        { }

        /*************************************************************************************************************************************************************/

        /// <summary>
        /// 左 符号 '`'
        /// </summary>
        internal void ObjLeftSymbol(StringBuilder sb)
        {
            sb.Append('`');
        }
        /// <summary>
        /// 右 符号 '`'
        /// </summary>
        internal void ObjRightSymbol(StringBuilder sb)
        {
            sb.Append('`');
        }
        /// <summary>
        /// 参数 符号 '?'
        /// </summary>
        internal void ParamSymbol(StringBuilder sb)
        {
            sb.Append(QuestionMark);
        }

        /*************************************************************************************************************************************************************/

        internal void Top(Context dc, StringBuilder sb)
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
        internal void Column(string tbAlias, string colName, StringBuilder sb)
        {
            if (!tbAlias.IsNullStr())
            {
                TableXAlias(tbAlias, sb); Dot(sb);
            }

            if ("*".Equals(colName, StringComparison.OrdinalIgnoreCase))
            {
                sb.Append(colName);
            }
            else
            {
                ObjLeftSymbol(sb); sb.Append(colName); ObjRightSymbol(sb);
            }
        }
        internal void ColumnAlias(string colAlias, StringBuilder sb)
        {
            ObjLeftSymbol(sb); sb.Append(colAlias); ObjRightSymbol(sb);
        }
        internal void ColumnReplaceNullValueForSum(string tbAlias, string colName, StringBuilder sb)
        {
            sb.Append("ifnull");
            LeftRoundBracket(sb);
            Column(tbAlias, colName, sb); Comma(sb); sb.Append('0');
            RightRoundBracket(sb);
        }
        internal void TableX(string tbName, StringBuilder sb)
        {
            ObjLeftSymbol(sb); sb.Append(tbName); ObjRightSymbol(sb);
        }
        internal void TableXAlias(string tbAlias, StringBuilder sb)
        {
            ObjLeftSymbol(sb); sb.Append(tbAlias); ObjRightSymbol(sb);
        }
        internal void MultiAction(ActionEnum action, StringBuilder sb, Context dc)
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
        internal void DbParam(string param, StringBuilder sb)
        {
            ParamSymbol(sb); sb.Append(param);
        }
        internal void OneEqualOneProcess(DicParam p, StringBuilder sb)
        {
            Spacing(sb); DbParam(p.Param, sb);
        }
        internal void WhereTrueOrFalse(Context dc, bool flag, StringBuilder sb)
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
        internal ColumnInfo GetIndex(List<ColumnInfo> cols)
        {
            return
                cols.FirstOrDefault(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)) ??
                cols.FirstOrDefault(it => "UNI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)) ??
                cols.FirstOrDefault(it => "MUL".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)) ??
                cols.FirstOrDefault(it => "NO".Equals(it.IsNullable, StringComparison.OrdinalIgnoreCase)) ??
                cols.FirstOrDefault();
        }
        internal void Pager(Context dc, StringBuilder sb)
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
