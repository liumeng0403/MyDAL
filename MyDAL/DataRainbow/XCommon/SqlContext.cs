using HPC.DAL.Core;
using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using HPC.DAL.Core.Enums.Funcs;
using HPC.DAL.DataRainbow.XCommon.Bases;
using HPC.DAL.DataRainbow.XCommon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HPC.DAL.DataRainbow.XCommon
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

        internal protected void ConcatWithComma(IEnumerable<string> ss, Action<StringBuilder> preSymbol, Action<StringBuilder> afterSymbol)
        {
            var n = ss.Count();
            var i = 0;
            foreach (var s in ss)
            {
                i++;
                preSymbol?.Invoke(X); X.Append(s); afterSymbol?.Invoke(X);
                if (i != n)
                {
                    Comma(X);
                }
            }
        }

        /****************************************************************************************************************************/

        private static bool IsPaging(Context dc)
        {
            if (dc.Method == UiMethodEnum.QueryPaging)
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
                X.Append("concat");
                LeftRoundBracket(X); StringConst(Percent.ToString(), X); Comma(X); DbSql.DbParam(name, X); Comma(X); StringConst(Percent.ToString(), X); RightRoundBracket(X);
            }
            else if ((value.Contains("%") || value.Contains("_"))
                && !value.Contains("/%")
                && !value.Contains("/_"))
            {
                DbSql.DbParam(name, X);
            }
            else if (value.Contains("/%")
                || value.Contains("/_"))
            {
                DbSql.DbParam(name, X); Spacing(X); Escape(X); Spacing(X); StringConst(EscapeChar.ToString(), X);
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._015, $"{dic.Action}-{dic.Option}-{value}");
            }
        }
        private void InParams(List<DicParam> dbs)
        {
            var i = 0;
            foreach (var it in dbs)
            {
                i++;
                DbSql.DbParam(it.Param, X);
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
            Compare(db.Compare, X, DC); DbSql.DbParam(db.Param, X);
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
            RightRoundBracket(X); Compare(db.Compare, X, DC); DbSql.DbParam(db.Param, X);
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
            Compare(db.Compare, X, DC); DbSql.DbParam(db.Param, X);
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
                Compare(db.Compare, X, DC); DbSql.DbParam(db.Param, X);
            }
        }
        private void FunctionProcess(DicParam db)
        {
            if (db.Func == FuncEnum.CharLength)
            {
                CharLengthProcess(db);
            }
            else if (db.Func == FuncEnum.ToString_CS_DateTime_Format)
            {
                DateFormatProcess(db);
            }
            else if (db.Func == FuncEnum.Trim || db.Func == FuncEnum.LTrim || db.Func == FuncEnum.RTrim)
            {
                TrimProcess(db);
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._006, db.Func.ToString());
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
            foreach (var dp in DC.Parameters)
            {
                if (dp.Crud != CrudEnum.Join) { continue; }
                switch (dp.Action)
                {
                    case ActionEnum.From:
                        /* 已处理 */
                        break;
                    case ActionEnum.InnerJoin:
                    case ActionEnum.LeftJoin:
                        var tbm = DC.XC.GetTableModel(dp.TbMType);
                        CRLF(X); Tab(X);
                        Action(dp.Action, X, DC); Spacing(X); DbSql.TableX(tbm.TbName, X); As(X); DbSql.TableXAlias(dp.TbAlias, X);
                        break;
                    case ActionEnum.On:
                        CRLF(X); Tab(X); Tab(X);
                        Action(dp.Action, X, DC); Spacing(X); DbSql.Column(dp.TbAlias, dp.TbCol, X); Compare(dp.Compare, X, DC); DbSql.Column(dp.TableAliasTwo, dp.ColumnTwo, X);
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
                        DbSql.MultiAction(db.GroupAction, X, DC);
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
                    throw XConfig.EC.Exception(XConfig.EC._011, $"{db.Action}-{db.Option}");
                }
            }
        }

        /****************************************************************************************************************************/

        private void SelectAllCols()
        {
            Star(X);
        }
        private void SelectSpecialNoFuncCol(DicParam dic)
        {
            if (dic.Crud == CrudEnum.Join)
            {
                if (dic.Option == OptionEnum.Column)
                {
                    DbSql.Column(dic.TbAlias, dic.TbCol, X);
                }
                else if (dic.Option == OptionEnum.ColumnAs)
                {
                    DbSql.Column(dic.TbAlias, dic.TbCol, X); As(X); DbSql.ColumnAlias(dic.TbColAlias, X);
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
                    DbSql.Column(string.Empty, dic.TbCol, X); As(X); DbSql.ColumnAlias(dic.TbColAlias, X);
                }
            }
        }
        private void SelectSpecialDateFormatCol(DicParam dic)
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
                    As(X); DbSql.ColumnAlias(dic.TbColAlias, X);
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
                    As(X); DbSql.ColumnAlias(dic.TbColAlias, X);
                }
            }
        }
        private void ToStringCol(string tbAlias, string tbCol, ToStringEnum func)
        {
            if (func == ToStringEnum.concat)
            {
                X.Append("concat"); LeftRoundBracket(X); DbSql.Column(tbAlias, tbCol, X); Comma(X); StringConst(string.Empty, X); RightRoundBracket(X);
            }
            else if (func == ToStringEnum.add)
            {
                StringConst(string.Empty, X); X.Append('+'); DbSql.Column(tbAlias, tbCol, X);
            }
            else
            {
                throw XConfig.EC.Exception(XConfig.EC._095, $"函数 -- {func.ToString()} -- 未能解析！");
            }
        }
        private void SelectSpecialCsToStringCol(DicParam dic)
        {
            var ts = default(ToStringEnum);
            if (dic.CsType == XConfig.CSTC.Double)
            {
                ts = ToStringEnum.add;
            }
            else
            {
                ts = ToStringEnum.concat;
            }

            //
            if (dic.Crud == CrudEnum.Join)
            {
                if (dic.Option == OptionEnum.Column)
                {
                    ToStringCol(dic.TbAlias, dic.TbCol, ts);
                }
                else if (dic.Option == OptionEnum.ColumnAs)
                {
                    ToStringCol(dic.TbAlias, dic.TbCol, ts); As(X); DbSql.ColumnAlias(dic.TbColAlias, X);
                }
            }
            else if (dic.Crud == CrudEnum.Query)
            {
                if (dic.Option == OptionEnum.Column)
                {
                    ToStringCol(string.Empty, dic.TbCol, ts);
                }
                else if (dic.Option == OptionEnum.ColumnAs)
                {
                    ToStringCol(string.Empty, dic.TbCol, ts); As(X); DbSql.ColumnAlias(dic.TbColAlias, X);
                }
            }
        }

        /****************************************************************************************************************************/

        internal protected void InsertColumn()
        {
            Spacing(X);
            var ps = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.Insert && it.Option == OptionEnum.Insert);
            if (ps != null)
            {
                CRLF(X);
                LeftRoundBracket(X);
                ConcatWithComma(ps.Inserts.Select(it => it.TbCol), DbSql.ObjLeftSymbol, DbSql.ObjRightSymbol);
                RightRoundBracket(X);
            }
        }

        internal protected void UpdateColumn()
        {
            //
            var list = DC.Parameters.Where(it => it.Action == ActionEnum.Update)?.ToList();
            if (list == null || list.Count == 0) { throw XConfig.EC.Exception(XConfig.EC._053, "没有设置任何要更新的字段!"); }

            //
            Spacing(X);
            var i = 0;
            foreach (var item in list)
            {
                i++;
                if (item.Option == OptionEnum.ChangeAdd
                    || item.Option == OptionEnum.ChangeMinus)
                {
                    if (i != 1) { CRLF(X); Tab(X); }
                    DbSql.Column(string.Empty, item.TbCol, X); Equal(X); DbSql.Column(string.Empty, item.TbCol, X); Option(item.Option, X, DC); DbSql.DbParam(item.Param, X);
                }
                else if (item.Option == OptionEnum.Set)
                {
                    if (i != 1) { CRLF(X); Tab(X); }
                    DbSql.Column(string.Empty, item.TbCol, X); Option(item.Option, X, DC); DbSql.DbParam(item.Param, X);
                }
                else
                {
                    throw XConfig.EC.Exception(XConfig.EC._009, $"{item.Action}-{item.Option}");
                }
                if (i != list.Count) { Comma(X); }
            }
        }

        internal protected void SelectColumn()
        {
            Spacing(X);

            //
            var col = DC.Parameters.FirstOrDefault(it => IsSelectColumnParam(it));
            var items = col?.Columns.Where(it => it.Option == OptionEnum.Column || it.Option == OptionEnum.ColumnAs)?.ToList();
            if (col == null || items == null || items.Count <= 0)
            {
                SelectAllCols();
                return;
            }

            //
            var i = 0;
            foreach (var dic in items)
            {
                i++;
                if (i != 1) { CRLF(X); }
                if (items.Count > 1) { Tab(X); }
                if (dic.Func == FuncEnum.None)
                {
                    SelectSpecialNoFuncCol(dic);
                }
                else if (dic.Func == FuncEnum.ToString_CS_DateTime_Format)
                {
                    SelectSpecialDateFormatCol(dic);
                }
                else if (dic.Func == FuncEnum.ToString_CS)
                {
                    SelectSpecialCsToStringCol(dic);
                }
                else
                {
                    throw XConfig.EC.Exception(XConfig.EC._007, $"函数 -- {dic.Func.ToString()} -- 未解析！");
                }
                if (i != items.Count) { Comma(X); }
            }
        }

        internal protected void Sum()
        {
            Spacing(X);
            var col = DC.Parameters.FirstOrDefault(it => IsSelectColumnParam(it));
            var sum = col.Columns.FirstOrDefault(it => it.Func == FuncEnum.Sum)
                              ?? col.Columns.FirstOrDefault(it => it.Func == FuncEnum.SumNullable);
            if (sum != null)
            {
                var tbAlias = sum.Crud == CrudEnum.Query
                                      ? string.Empty
                                      : sum.Crud == CrudEnum.Join
                                        ? sum.TbAlias
                                        : string.Empty;
                Function(sum.Func, X, DC);
                LeftRoundBracket(X);
                if (sum.Func == FuncEnum.Sum)
                {
                    DbSql.Column(tbAlias, sum.TbCol, X);
                }
                else if (sum.Func == FuncEnum.SumNullable)
                {
                    DbSql.ColumnReplaceNullValueForSum(tbAlias, sum.TbCol, X);
                }
                RightRoundBracket(X);
            }
        }

        internal protected void CountSetMulti(bool isD, bool isDS)
        {
            if (isD)
            {
                DistinctX();
                if (isDS)
                {
                    Star(X);
                }
                else
                {
                    SelectColumn();
                }
            }
            else
            {
                SelectColumn();
            }
            DC.IsMultiColCount = true;
        }
        internal protected void CountDistinct(List<DicParam> cols)
        {
            if (cols == null
                   || cols.Count <= 0)
            {
                CountSetMulti(true, false);
            }
            else if (cols.Count == 1)
            {
                var count = cols.First().Columns?.FirstOrDefault(it => IsCountParam(it));
                if (count != null)
                {
                    if ("*".Equals(count.TbCol, StringComparison.OrdinalIgnoreCase))
                    {
                        CountSetMulti(true, true);
                    }
                    else
                    {
                        Function(count.Func, X, DC); LeftRoundBracket(X); DistinctX();
                        if (count.Crud == CrudEnum.Query)
                        {
                            DbSql.Column(string.Empty, count.TbCol, X);
                        }
                        else if (count.Crud == CrudEnum.Join)
                        {
                            DbSql.Column(count.TbAlias, count.TbCol, X);
                        }
                        RightRoundBracket(X);
                    }
                }
                else
                {
                    if (cols.First().Columns == null
                        || cols.First().Columns.Count <= 0)
                    {
                        throw XConfig.EC.Exception(XConfig.EC._024, "不应该出现的情况！");
                    }
                    else if (cols.First().Columns.Count == 1)
                    {
                        var col = cols.First().Columns.First();
                        if (col.Crud == CrudEnum.Join
                            && "*".Equals(col.TbCol, StringComparison.OrdinalIgnoreCase))
                        {
                            CountSetMulti(true, false);
                        }
                        else
                        {
                            Function(FuncEnum.Count, X, DC); LeftRoundBracket(X); DistinctX();
                            if (col.Crud == CrudEnum.Query)
                            {
                                DbSql.Column(string.Empty, col.TbCol, X);
                            }
                            else if (col.Crud == CrudEnum.Join)
                            {
                                DbSql.Column(col.TbAlias, col.TbCol, X);
                            }
                            RightRoundBracket(X);
                        }
                    }
                    else
                    {
                        CountSetMulti(true, false);
                    }
                }
            }
            else
            {
                CountSetMulti(true, false);
            }
        }
        internal protected void CountNoneDistinct(List<DicParam> cols)
        {
            if (cols == null
                    || cols.Count <= 0)
            {
                X.Append(" count(*) ");
            }
            else if (cols.Count == 1)
            {
                var count = cols.First().Columns?.FirstOrDefault(it => IsCountParam(it));
                if (count != null)
                {
                    if ("*".Equals(count.TbCol, StringComparison.OrdinalIgnoreCase))
                    {
                        Function(count.Func, X, DC); LeftRoundBracket(X); Star(X); RightRoundBracket(X);
                    }
                    else
                    {
                        Function(count.Func, X, DC); LeftRoundBracket(X);
                        if (count.Crud == CrudEnum.Query)
                        {
                            DbSql.Column(string.Empty, count.TbCol, X);
                        }
                        else if (count.Crud == CrudEnum.Join)
                        {
                            DbSql.Column(count.TbAlias, count.TbCol, X);
                        }
                        RightRoundBracket(X);
                    }
                }
                else
                {
                    if (cols.First().Columns == null
                        || cols.First().Columns.Count <= 0)
                    {
                        throw XConfig.EC.Exception(XConfig.EC._025, "不应该出现的情况！");
                    }
                    else if (cols.First().Columns.Count == 1)
                    {
                        var col = cols.First().Columns.First();
                        if (col.Crud == CrudEnum.Join
                            && "*".Equals(col.TbCol, StringComparison.OrdinalIgnoreCase))
                        {
                            CountSetMulti(false, false);
                        }
                        else
                        {
                            Function(FuncEnum.Count, X, DC); LeftRoundBracket(X);
                            if (col.Crud == CrudEnum.Query)
                            {
                                DbSql.Column(string.Empty, col.TbCol, X);
                            }
                            else if (col.Crud == CrudEnum.Join)
                            {
                                DbSql.Column(col.TbAlias, col.TbCol, X);
                            }
                            RightRoundBracket(X);
                        }
                    }
                    else
                    {
                        CountSetMulti(false, false);
                    }
                }
            }
            else
            {
                CountSetMulti(false, false);
            }
        }
        internal protected void Count()
        {
            /* 
             * count(*)
             * count(distinct *) -- CountMulti
             * count(col)
             * count(distinct col)
             * count(cols) -- CountMulti
             * count(distinct cols) -- CountMulti
             */
            Spacing(X);
            var cols = DC.Parameters.Where(it => IsSelectColumnParam(it))?.ToList();
            var isD = DC.Parameters.Any(it => IsDistinctParam(it));
            if (isD)
            {
                CountDistinct(cols);
            }
            else
            {
                CountNoneDistinct(cols);
            }
        }

        internal protected void InsertValue()
        {
            Spacing(X);
            var items = DC.Parameters.Where(it => it.Action == ActionEnum.Insert && it.Option == OptionEnum.Insert)?.ToList();
            var i = 0;
            foreach (var dic in items)
            {
                i++;
                CRLF(X); LeftRoundBracket(X); ConcatWithComma(dic.Inserts.Select(it => it.Param), DbSql.ParamSymbol, null); RightRoundBracket(X);
                if (i != items.Count)
                {
                    Comma(X);
                }
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
                var tbm = DC.XC.GetTableModel(dic.TbMType);
                DbSql.TableX(tbm.TbName, X); As(X); DbSql.TableXAlias(dic.TbAlias, X);
                JoinX();
            }
            else
            {
                var tbm = DC.XC.GetTableModel(DC.TbM1);
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
                var flag = aId < oId || oId == -1;
                Spacing(X); DbSql.WhereTrueOrFalse(DC, flag, X); Spacing(X);
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

        internal protected void OrderByParams()
        {
            var orders = DC.Parameters.Where(it => IsOrderByParam(it)).ToList();
            var i = 0;
            foreach (var o in orders)
            {
                i++;
                if (o.Func == FuncEnum.None)
                {
                    if (DC.Crud == CrudEnum.Join)
                    {
                        DbSql.Column(o.TbAlias, o.TbCol, X); Spacing(X); Option(o.Option, X, DC);
                    }
                    else
                    {
                        DbSql.Column(string.Empty, o.TbCol, X); Spacing(X); Option(o.Option, X, DC);
                    }
                }
                else if (o.Func == FuncEnum.CharLength)
                {
                    if (DC.Crud == CrudEnum.Join)
                    {
                        Function(o.Func, X, DC); LeftRoundBracket(X); DbSql.Column(o.TbAlias, o.TbCol, X); RightRoundBracket(X); Spacing(X); Option(o.Option, X, DC);
                    }
                    else
                    {
                        Function(o.Func, X, DC); LeftRoundBracket(X); DbSql.Column(string.Empty, o.TbCol, X); RightRoundBracket(X); Spacing(X); Option(o.Option, X, DC);
                    }
                }
                else
                {
                    throw XConfig.EC.Exception(XConfig.EC._013, $"{o.Action}-{o.Option}-{o.Func}");
                }
                if (i != orders.Count) { Comma(X); CRLF(X); Tab(X); }
            }
        }
        internal protected void OrderBy()
        {
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            var tbm = DC.XC.GetTableModel(dic != null ? dic.TbMType : DC.TbM1);
            if (DC.Parameters.Any(it => it.Action == ActionEnum.OrderBy))
            {
                CRLF(X); X.Append("order by"); Spacing(X); OrderByParams();
            }
            else
            {
                if (!IsPaging(DC))
                {
                    return;
                }

                var col = DbSql.GetIndex(tbm.TbCols);
                if (col != null)
                {
                    CRLF(X); X.Append("order by"); Spacing(X);
                    if (DC.Crud == CrudEnum.Join)
                    {
                        DbSql.Column(dic.TbAlias, col.ColumnName, X); Spacing(X); X.Append("desc");
                    }
                    else
                    {
                        DbSql.Column(string.Empty, col.ColumnName, X); Spacing(X); X.Append("desc");
                    }
                }
            }
        }

        internal protected void CountMulti()
        {
            if (DC.IsMultiColCount)
            {
                X.Insert(0, "select count(*) \r\nfrom (\r\n");
                X.Append("\r\n         ) temp");
            }
        }

        internal protected void End()
        {
            X.Append(';');
            DC.SQL.Add(X.ToString());
            X.Clear();
        }

        /****************************************************************************************************************************/

    }
}
