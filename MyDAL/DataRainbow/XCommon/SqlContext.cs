using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.DataRainbow.XCommon.Bases;
using MyDAL.DataRainbow.XCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDAL.DataRainbow.XCommon
{
    internal abstract class SqlContext
           : XSQL
    {

        internal protected Context DC { get; set; }
        internal protected ISql DbSql { get; set; }
        internal protected StringBuilder X { get; set; } = new StringBuilder();

        /****************************************************************************************************************************/

        internal protected static void StringConst(string conStr, StringBuilder sb)
        {
            SingleQuote(sb); sb.Append(conStr); SingleQuote(sb);
        }

        /****************************************************************************************************************************/

        private static bool IsPaging(Context dc)
        {
            if (dc.Method == UiMethodEnum.QueryPagingAsync)
            {
                return true;
            }
            return false;
        }
        private static bool IsWhere(DicParam p)
        {
            if (p.Action == ActionEnum.Where
                || p.Action == ActionEnum.And
                || p.Action == ActionEnum.Or)
            {
                return true;
            }
            return false;
        }

        /****************************************************************************************************************************/

        internal protected static bool IsDistinctParam(DicParam param)
        {
            if (param.Action == ActionEnum.Select
                && param.Option == OptionEnum.ColumnOther
                && param.Compare == CompareXEnum.Distinct)
            {
                return true;
            }
            return false;
        }
        internal protected static bool IsSelectColumnParam(DicParam param)
        {
            if (param.Action == ActionEnum.Select
                && (param.Option == OptionEnum.Column || param.Option == OptionEnum.ColumnAs)
                && param.Columns != null)
            {
                return true;
            }
            return false;
        }
        internal protected static bool IsOrderByParam(DicParam param)
        {
            if (param.Action == ActionEnum.OrderBy
                && (param.Func == FuncEnum.None
                        || param.Func == FuncEnum.CharLength))
            {
                return true;
            }
            return false;
        }
        internal protected static bool IsCountParam(DicParam param)
        {
            if (param.Option == OptionEnum.Column
                && param.Func == FuncEnum.Count)
            {
                return true;
            }
            return false;
        }

        /****************************************************************************************************************************/
        
        private void LikeStrHandle(DicParam dic)
        {
            Spacing(X);
            var name = dic.Param;
            var value = dic.ParamInfo.Value.ToString();
            if (!value.Contains("%")
                && !value.Contains("_"))
            {
                X.Append("CONCAT");
                LeftRoundBracket(X); StringConst(Percent().ToString(), X); Comma(X); DbParam(name, X); Comma(X); StringConst(Percent().ToString(), X); RightRoundBracket(X);
            }
            else if ((value.Contains("%") || value.Contains("_"))
                && !value.Contains("/%")
                && !value.Contains("/_"))
            {
                DbParam(name, X);
            }
            else if (value.Contains("/%")
                || value.Contains("/_"))
            {
                DbParam(name, X); Spacing(X); Escape(X); Spacing(X); StringConst(EscapeChar().ToString(), X);
            }
            else
            {
                throw DC.Exception(XConfig.EC._015, $"{dic.Action}-{dic.Option}-{value}");
            }
        }
        private void InParams(List<DicParam> dbs)
        {
            var i = 0;
            foreach (var it in dbs)
            {
                i++;
                DbParam(it.Param, X);
                if (i != dbs.Count) { Comma(X); }
            }
        }

        /****************************************************************************************************************************/

        private void CharLengthProcess(DicParam db)
        {
            Spacing(X);
            Function(db.Func, X, DC); LeftRoundBracket(X);
            if (db.Crud == CrudEnum.Join)
            {
                DbSql.Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                DbSql.Column(string.Empty, db.TbCol, X);
            }
            RightRoundBracket(X);
            Compare(db.Compare, X, DC); DbParam(db.Param, X);
        }
        private void DateFormatProcess(DicParam db)
        {
            Spacing(X);
            Function(db.Func, X, DC); LeftRoundBracket(X);
            if (db.Crud == CrudEnum.Join)
            {
                DbSql.Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                DbSql.Column(string.Empty, db.TbCol, X);
            }

            Comma(X); StringConst(db.Format, X);
            RightRoundBracket(X); Compare(db.Compare, X, DC); DbParam(db.Param, X);
        }
        private void TrimProcess(DicParam db)
        {
            Spacing(X);
            Function(db.Func, X, DC); LeftRoundBracket(X);
            if (db.Crud == CrudEnum.Join)
            {
                DbSql.Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                DbSql.Column(string.Empty, db.TbCol, X);
            }
            RightRoundBracket(X);
            Compare(db.Compare, X, DC); DbParam(db.Param, X);
        }
        private void InProcess(DicParam db)
        {
            Spacing(X);
            if (db.Crud == CrudEnum.Join)
            {
                DbSql.Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                DbSql.Column(string.Empty, db.TbCol, X);
            }
            Spacing(X);
            Compare(db.Compare, X, DC); LeftRoundBracket(X); InParams(db.InItems); RightRoundBracket(X);
        }
        private void LikeProcess(DicParam db)
        {
            Spacing(X);
            if (db.Crud == CrudEnum.Join)
            {
                DbSql.Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                DbSql.Column(string.Empty, db.TbCol, X);
            }
            Compare(db.Compare, X, DC); LikeStrHandle(db);
        }

        /****************************************************************************************************************************/

        private void CompareProcess(DicParam db)
        {
            if (db.Compare == CompareXEnum.In
                || db.Compare == CompareXEnum.NotIn)
            {
                InProcess(db);
            }
            else if (db.Compare == CompareXEnum.Like
                        || db.Compare == CompareXEnum.NotLike)
            {
                LikeProcess(db);
            }
            else
            {
                Spacing(X);
                if (db.Crud == CrudEnum.Join)
                {
                    DbSql.Column(db.TbAlias, db.TbCol, X);
                }
                else if (DC.IsSingleTableOption())
                {
                    DbSql.Column(string.Empty, db.TbCol, X);
                }
                Compare(db.Compare, X, DC); DbParam(db.Param, X);
            }
        }
        private void FunctionProcess(DicParam db)
        {
            if (db.Func == FuncEnum.CharLength)
            {
                CharLengthProcess(db);
            }
            else if (db.Func == FuncEnum.DateFormat)
            {
                DateFormatProcess(db);
            }
            else if (db.Func == FuncEnum.Trim || db.Func == FuncEnum.LTrim || db.Func == FuncEnum.RTrim)
            {
                TrimProcess(db);
            }
            else
            {
                throw DC.Exception(XConfig.EC._006, db.Func.ToString());
            }
        }
        private void IsNullProcess(DicParam db)
        {
            Spacing(X);
            if (db.Crud == CrudEnum.Join)
            {
                DbSql.Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                DbSql.Column(string.Empty, db.TbCol, X);
            }
            Spacing(X); Option(db.Option, X, DC);
        }

        /****************************************************************************************************************************/

        private void JoinX()
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
                        Action(item.Action, X, DC); Spacing(X); DbSql.TableX(item.TbName, X); As(X); X.Append(item.TbAlias);
                        break;
                    case ActionEnum.On:
                        CRLF(X); Tab(X); Tab(X);
                        Action(item.Action, X, DC); Spacing(X); DbSql.Column(item.TbAlias, item.TbCol, X); Compare(item.Compare, X, DC); DbSql.Column(item.TableAliasTwo, item.ColumnTwo, X);
                        break;
                }
            }
        }
        private void MultiCondition(DicParam db)
        {
            if (db.Group != null)
            {
                var i = 0;
                foreach (var item in db.Group)
                {
                    i++;
                    if (item.Group != null)
                    {
                        LeftRoundBracket(X);
                        MultiCondition(item);
                        RightRoundBracket(X);
                    }
                    else
                    {
                        MultiCondition(item);
                    }
                    if (i != db.Group.Count)
                    {
                        MultiAction(db.GroupAction, X, DC);
                    }
                }
            }
            else
            {
                if (db.Option == OptionEnum.Compare)
                {
                    CompareProcess(db);
                }
                else if (db.Option == OptionEnum.Function)
                {
                    FunctionProcess(db);
                }
                else if (db.Option == OptionEnum.OneEqualOne)
                {
                    DbSql.OneEqualOneProcess(db, X);
                }
                else if (db.Option == OptionEnum.IsNull || db.Option == OptionEnum.IsNotNull)
                {
                    IsNullProcess(db);
                }
                else
                {
                    throw DC.Exception(XConfig.EC._011, $"{db.Action}-{db.Option}");
                }
            }
        }

        /****************************************************************************************************************************/

        internal protected void SelectColumn()
        {
            Spacing(X);
            var col = DC.Parameters.FirstOrDefault(it => IsSelectColumnParam(it));
            if (col == null)
            {
                Star(X);
                return;
            }
            var items = col.Columns.Where(it => it.Option == OptionEnum.Column || it.Option == OptionEnum.ColumnAs)?.ToList();
            if (items == null || items.Count <= 0)
            {
                Star(X);
                return;
            }
            var i = 0;
            foreach (var dic in items)
            {
                i++;
                if (i != 1) { CRLF(X); }
                if (items.Count > 1) { Tab(X); }
                if (dic.Func == FuncEnum.None)
                {
                    if (dic.Crud == CrudEnum.Join)
                    {
                        if (dic.Option == OptionEnum.Column)
                        {
                            DbSql.Column(dic.TbAlias, dic.TbCol, X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            DbSql.Column(dic.TbAlias, dic.TbCol, X); As(X); X.Append(dic.TbColAlias);
                        }
                    }
                    else if (dic.Crud == CrudEnum.Query)
                    {
                        if (dic.Option == OptionEnum.Column)
                        {
                            DbSql.Column(string.Empty, dic.TbCol, X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            DbSql.Column(string.Empty, dic.TbCol, X); As(X); X.Append(dic.TbColAlias);
                        }
                    }
                }
                else if (dic.Func == FuncEnum.DateFormat)
                {
                    if (dic.Crud == CrudEnum.Join)
                    {
                        if (dic.Option == OptionEnum.Column)
                        {
                            Function(dic.Func, X, DC); LeftRoundBracket(X); DbSql.Column(dic.TbAlias, dic.TbCol, X); Comma(X); StringConst(dic.Format, X); RightRoundBracket(X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            Function(dic.Func, X, DC); LeftRoundBracket(X); DbSql.Column(dic.TbAlias, dic.TbCol, X); Comma(X); StringConst(dic.Format, X); RightRoundBracket(X);
                            As(X); X.Append(dic.TbColAlias);
                        }
                    }
                    else if (dic.Crud == CrudEnum.Query)
                    {
                        if (dic.Option == OptionEnum.Column)
                        {
                            Function(dic.Func, X, DC); LeftRoundBracket(X); DbSql.Column(string.Empty, dic.TbCol, X); Comma(X); StringConst(dic.Format, X); RightRoundBracket(X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            Function(dic.Func, X, DC); LeftRoundBracket(X); DbSql.Column(string.Empty, dic.TbCol, X); Comma(X); StringConst(dic.Format, X); RightRoundBracket(X);
                            As(X); X.Append(dic.TbColAlias);
                        }
                    }
                }
                else
                {
                    throw DC.Exception(XConfig.EC._007, dic.Func.ToString());
                }
                if (i != items.Count) { Comma(X); }
            }
        }

        internal protected void DistinctX()
        {
            if (DC.Parameters.Any(it => IsDistinctParam(it)))
            {
                Distinct(X);
            }
        }

        internal protected void Table()
        {
            Spacing(X);
            if (DC.Crud == CrudEnum.Join)
            {
                var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
                DbSql.TableX(dic.TbName, X); As(X); X.Append(dic.TbAlias);
                JoinX();
            }
            else
            {
                var tbm = DC.XC.GetTableModel(DC.XC.GetModelKey(DC.TbM1.FullName));
                DbSql.TableX(tbm.TbName, X);
            }
        }

        internal protected void Where()
        {
            var cons = DC.Parameters.Where(it => IsWhere(it))?.ToList();
            if (cons == null)
            {
                return;
            }
            var where = cons.FirstOrDefault(it => it.Action == ActionEnum.Where);
            var and = cons.FirstOrDefault(it => it.Action == ActionEnum.And);
            var or = cons.FirstOrDefault(it => it.Action == ActionEnum.Or);
            if (where == null
                && (and != null || or != null))
            {
                var aId = and == null ? -1 : and.ID;
                var oId = or == null ? -1 : or.ID;
                if (aId < oId
                    || oId == -1)
                {
                    Action(ActionEnum.Where, X, DC); Spacing(X); X.Append("true"); Spacing(X);
                }
                else
                {
                    Action(ActionEnum.Where, X, DC); Spacing(X); X.Append("false"); Spacing(X);
                }
            }
            foreach (var db in cons)
            {
                CRLF(X); Action(db.Action, X, DC); Spacing(X);
                if (db.Group == null)
                {
                    MultiCondition(db);
                }
                else
                {
                    LeftRoundBracket(X);
                    MultiCondition(db);
                    RightRoundBracket(X);
                }
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


        /****************************************************************************************************************************/



    }
}
