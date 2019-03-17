using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.DataRainbow.XCommon.Bases;
using MyDAL.DataRainbow.XCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDAL.Core.Bases
{
    internal abstract class SqlContext
        : XSQL
    {

        protected Context DC { get; set; }
        protected ISql DbSql { get; set; }
        protected StringBuilder X { get; set; } = new StringBuilder();

        /****************************************************************************************************************************/

        internal protected static void StringConst(string conStr, StringBuilder sb)
        {
            SingleQuote(sb); sb.Append(conStr); SingleQuote(sb);
        }

        /****************************************************************************************************************************/

        protected static bool IsPaging(Context dc)
        {
            if (dc.Method == UiMethodEnum.QueryPagingAsync)
            {
                return true;
            }
            return false;
        }

        /****************************************************************************************************************************/

        protected static bool IsDistinctParam(DicParam param)
        {
            if (param.Action == ActionEnum.Select
                && param.Option == OptionEnum.ColumnOther
                && param.Compare == CompareXEnum.Distinct)
            {
                return true;
            }
            return false;
        }
        protected static bool IsSelectColumnParam(DicParam param)
        {
            if (param.Action == ActionEnum.Select
                && (param.Option == OptionEnum.Column || param.Option == OptionEnum.ColumnAs)
                && param.Columns != null)
            {
                return true;
            }
            return false;
        }
        protected static bool IsOrderByParam(DicParam param)
        {
            if (param.Action == ActionEnum.OrderBy
                && (param.Func == FuncEnum.None
                        || param.Func == FuncEnum.CharLength))
            {
                return true;
            }
            return false;
        }
        protected static bool IsCountParam(DicParam param)
        {
            if (param.Option == OptionEnum.Column
                && param.Func == FuncEnum.Count)
            {
                return true;
            }
            return false;
        }

        /****************************************************************************************************************************/

        internal protected static void DbParam(string param, StringBuilder sb)
        {
            At(sb); sb.Append(param);
        }

        /****************************************************************************************************************************/

        private void JoinX(Action<string, StringBuilder> tableXAction, Action<string, string, StringBuilder> columnAction)
        {
            Spacing(X);
            foreach (var item in DC.Parameters)
            {
                if (item.Crud != CrudEnum.Join) { continue; }
                switch (item.Action)
                {
                    case ActionEnum.From: break;    // 已处理 
                    case ActionEnum.InnerJoin:
                    case ActionEnum.LeftJoin:
                        CRLF(X); Tab(X);
                        Action(item.Action, X, DC); Spacing(X); tableXAction(item.TbName, X); As(X); X.Append(item.TbAlias);
                        break;
                    case ActionEnum.On:
                        CRLF(X); Tab(X); Tab(X);
                        Action(item.Action, X, DC); Spacing(X); columnAction(item.TbAlias, item.TbCol, X); Compare(item.Compare, X, DC); columnAction(item.TableAliasTwo, item.ColumnTwo, X);
                        break;
                }
            }
        }

        internal protected void Table(Action<string, StringBuilder> tableXAction, Action<string, string, StringBuilder> columnAction)
        {
            Spacing(X);
            if (DC.Crud == CrudEnum.Join)
            {
                var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
                tableXAction(dic.TbName, X); As(X); X.Append(dic.TbAlias);
                JoinX(tableXAction, columnAction);
            }
            else
            {
                var tbm = DC.XC.GetTableModel(DC.XC.GetModelKey(DC.TbM1.FullName));
                tableXAction(tbm.TbName, X);
            }
        }

        internal protected void OrderBy(
            Func<List<ColumnInfo>, ColumnInfo> getIndexFunc,
            Action<string, string, StringBuilder> columnAction,
            Action orderByParamsAction)
        {
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            var key = dic != null ? dic.Key : DC.XC.GetModelKey(DC.TbM1.FullName);
            var tbm = DC.XC.GetTableModel(key);
            if (DC.Parameters.Any(it => it.Action == ActionEnum.OrderBy))
            {
                CRLF(X); X.Append("order by"); Spacing(X); orderByParamsAction();
            }
            else
            {
                if (!IsPaging(DC))
                {
                    return;
                }

                var col = getIndexFunc(tbm.TbCols);
                if (col != null)
                {
                    CRLF(X); X.Append("order by"); Spacing(X);
                    if (DC.Crud == CrudEnum.Join)
                    {
                        columnAction(dic.TbAlias, col.ColumnName, X); Spacing(X); X.Append("desc");
                    }
                    else
                    {
                        columnAction(string.Empty, col.ColumnName, X); Spacing(X); X.Append("desc");
                    }
                }
            }
        }
    }
}
