using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDAL.DataRainbow.SQLServer
{
    internal sealed class SqlServerProvider
       : SqlServer, ISqlProvider
    {
        private SqlServerProvider() { }
        internal SqlServerProvider(Context dc)
        {
            DC = dc;
            DC.SqlProvider = this;
        }

        /****************************************************************************************************************/

        private void OrderByParams()
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
                        Column(o.TbAlias, o.TbCol, X); Spacing(X); Option(o.Option, X, DC);
                    }
                    else
                    {
                        Column(string.Empty, o.TbCol, X); Spacing(X); Option(o.Option, X, DC);
                    }
                }
                else if (o.Func == FuncEnum.CharLength)
                {
                    if (DC.Crud == CrudEnum.Join)
                    {
                        Function(o.Func, X, DC); LeftBracket(X); Column(o.TbAlias, o.TbCol, X); RightBracket(X); Spacing(X); Option(o.Option, X, DC);
                    }
                    else
                    {
                        Function(o.Func, X, DC); LeftBracket(X); Column(string.Empty, o.TbCol, X); RightBracket(X); Spacing(X); Option(o.Option, X, DC);
                    }
                }
                else
                {
                    throw DC.Exception(XConfig.EC._013, $"{o.Action}-{o.Option}-{o.Func}");
                }
                if (i != orders.Count) { Comma(X); CRLF(X); Tab(X); }
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
        private void ConcatWithComma(IEnumerable<string> ss, Action<StringBuilder> preSymbol, Action<StringBuilder> afterSymbol)
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
        private void LikeStrHandle(DicParam dic)
        {
            Spacing(X);
            var name = dic.Param;
            var value = dic.ParamInfo.Value.ToString();
            if (!value.Contains("%")
                && !value.Contains("_"))
            {
                X.Append("CONCAT");
                LeftBracket(X); StringConst(Percent().ToString(), X); Comma(X); DbParam(name, X); Comma(X); StringConst(Percent().ToString(), X); RightBracket(X);
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

        /****************************************************************************************************************/

        private void CharLengthProcess(DicParam db)
        {
            Spacing(X);
            Function(db.Func, X, DC); LeftBracket(X);
            if (db.Crud == CrudEnum.Join)
            {
                Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                Column(string.Empty, db.TbCol, X);
            }
            RightBracket(X);
            Compare(db.Compare, X, DC); DbParam(db.Param, X);
        }
        private void DateFormatProcess(DicParam db)
        {
            Spacing(X);
            Function(db.Func, X, DC); LeftBracket(X);
            if (db.Crud == CrudEnum.Join)
            {
                Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                Column(string.Empty, db.TbCol, X);
            }

            Comma(X); StringConst(db.Format, X);
            RightBracket(X); Compare(db.Compare, X, DC); DbParam(db.Param, X);
        }
        private void TrimProcess(DicParam db)
        {
            Spacing(X);
            Function(db.Func, X, DC); LeftBracket(X);
            if (db.Crud == CrudEnum.Join)
            {
                Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                Column(string.Empty, db.TbCol, X);
            }
            RightBracket(X);
            Compare(db.Compare, X, DC); DbParam(db.Param, X);
        }
        private void InProcess(DicParam db)
        {
            Spacing(X);
            if (db.Crud == CrudEnum.Join)
            {
                Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                Column(string.Empty, db.TbCol, X);
            }
            Spacing(X);
            Compare(db.Compare, X, DC); LeftBracket(X); InParams(db.InItems); RightBracket(X);
        }

        /****************************************************************************************************************/

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
                    Column(db.TbAlias, db.TbCol, X);
                }
                else if (DC.IsSingleTableOption())
                {
                    Column(string.Empty, db.TbCol, X);
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
        private void LikeProcess(DicParam db)
        {
            Spacing(X);
            if (db.Crud == CrudEnum.Join)
            {
                Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                Column(string.Empty, db.TbCol, X);
            }
            Compare(db.Compare, X, DC); LikeStrHandle(db);
        }
        private void OneEqualOneProcess(DicParam db)
        {
            Spacing(X); DbParam(db.Param, X);
        }
        private void IsNullProcess(DicParam db)
        {
            Spacing(X);
            if (db.Crud == CrudEnum.Join)
            {
                Column(db.TbAlias, db.TbCol, X);
            }
            else if (DC.IsSingleTableOption())
            {
                Column(string.Empty, db.TbCol, X);
            }
            Spacing(X); Option(db.Option, X, DC);
        }

        /****************************************************************************************************************/

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
                        LeftBracket(X); MultiCondition(item); RightBracket(X);
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
                    OneEqualOneProcess(db);
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

        /****************************************************************************************************************/

        private void InsertColumn()
        {
            Spacing(X);
            var ps = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.Insert && it.Option == OptionEnum.Insert);
            if (ps != null)
            {
                CRLF(X);
                LeftRoundBracket(X); ConcatWithComma(ps.Inserts.Select(it => it.TbCol), LeftSquareBracket, RightSquareBracket); RightRoundBracket(X);
            }
        }
        private void UpdateColumn()
        {
            //
            var list = DC.Parameters.Where(it => it.Action == ActionEnum.Update)?.ToList();
            if (list == null || list.Count == 0) { throw new Exception("没有设置任何要更新的字段!"); }

            //
            if (DC.Set == SetEnum.AllowedNull)
            { }
            else if (DC.Set == SetEnum.NotAllowedNull)
            {
                if (list.Any(it => it.ParamInfo.Value == DBNull.Value))
                {
                    throw new Exception($"{DC.Set} -- 字段:[[{string.Join(",", list.Where(it => it.ParamInfo.Value == DBNull.Value).Select(it => it.TbCol))}]]的值不能设为 Null !!!");
                }
            }
            else if (DC.Set == SetEnum.IgnoreNull)
            {
                list = list.Where(it => it.ParamInfo.Value != DBNull.Value)?.ToList();
                if (list == null || list.Count == 0) { throw new Exception("没有设置任何要更新的字段!"); }
            }
            else
            {
                throw DC.Exception(XConfig.EC._012, DC.Set.ToString());
            }

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
                    Column(string.Empty, item.TbCol, X); Equal(X); Column(string.Empty, item.TbCol, X); Option(item.Option, X, DC); DbParam(item.Param, X);
                }
                else if (item.Option == OptionEnum.Set)
                {
                    if (i != 1) { CRLF(X); Tab(X); }
                    Column(string.Empty, item.TbCol, X); Option(item.Option, X, DC); DbParam(item.Param, X);
                }
                else
                {
                    throw DC.Exception(XConfig.EC._009, $"{item.Action}-{item.Option}");
                }
                if (i != list.Count) { Comma(X); }
            }
        }
        private void SelectColumn()
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
                            Column(dic.TbAlias, dic.TbCol, X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            Column(dic.TbAlias, dic.TbCol, X); As(X); X.Append(dic.TbColAlias);
                        }
                    }
                    else if (dic.Crud == CrudEnum.Query)
                    {
                        if (dic.Option == OptionEnum.Column)
                        {
                            Column(string.Empty, dic.TbCol, X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            Column(string.Empty, dic.TbCol, X); As(X); X.Append(dic.TbColAlias);
                        }
                    }
                }
                else if (dic.Func == FuncEnum.DateFormat)
                {
                    if (dic.Crud == CrudEnum.Join)
                    {
                        if (dic.Option == OptionEnum.Column)
                        {
                            Function(dic.Func, X, DC); LeftBracket(X); Column(dic.TbAlias, dic.TbCol, X); Comma(X); StringConst(dic.Format, X); RightBracket(X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            Function(dic.Func, X, DC); LeftBracket(X); Column(dic.TbAlias, dic.TbCol, X); Comma(X); StringConst(dic.Format, X); RightBracket(X);
                            As(X); X.Append(dic.TbColAlias);
                        }
                    }
                    else if (dic.Crud == CrudEnum.Query)
                    {
                        if (dic.Option == OptionEnum.Column)
                        {
                            Function(dic.Func, X, DC); LeftBracket(X); Column(string.Empty, dic.TbCol, X); Comma(X); StringConst(dic.Format, X); RightBracket(X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            Function(dic.Func, X, DC); LeftBracket(X); Column(string.Empty, dic.TbCol, X); Comma(X); StringConst(dic.Format, X); RightBracket(X);
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
        private void InsertValue()
        {
            Spacing(X);
            var items = DC.Parameters.Where(it => it.Action == ActionEnum.Insert && it.Option == OptionEnum.Insert)?.ToList();
            var i = 0;
            foreach (var dic in items)
            {
                i++;
                CRLF(X); LeftBracket(X); ConcatWithComma(dic.Inserts.Select(it => it.Param), At, null); RightBracket(X);
                if (i != items.Count)
                {
                    Comma(X);
                }
            }
        }

        internal void Table()
        {
            Spacing(X);
            if (DC.Crud == CrudEnum.Join)
            {
                var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
                TableX(dic.TbName, X); As(X); X.Append(dic.TbAlias);
                Join();
            }
            else
            {
                var tbm = DC.XC.GetTableModel(DC.XC.GetModelKey(DC.TbM1.FullName));
                TableX(tbm.TbName, X);
            }
        }

        private void Join()
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
                        Action(item.Action, X, DC); Spacing(X); TableX(item.TbName, X); As(X); X.Append(item.TbAlias);
                        break;
                    case ActionEnum.On:
                        CRLF(X); Tab(X); Tab(X);
                        Action(item.Action, X, DC); Spacing(X); Column(item.TbAlias, item.TbCol, X); Compare(item.Compare, X, DC); Column(item.TableAliasTwo, item.ColumnTwo, X);
                        break;
                }
            }
        }
        private void Where()
        {
            var cons = DC.Parameters.Where(it => it.Action == ActionEnum.Where || it.Action == ActionEnum.And || it.Action == ActionEnum.Or)?.ToList();
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
                    LeftBracket(X); MultiCondition(db); RightBracket(X);
                }
            }
        }
        private void OrderBy()
        {
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            var key = dic != null ? dic.Key : DC.XC.GetModelKey(DC.TbM1.FullName);
            var tbm = DC.XC.GetTableModel(key);
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

                var col = GetIndex(tbm.TbCols);
                if (col != null)
                {
                    CRLF(X); X.Append("order by"); Spacing(X);
                    if (DC.Crud == CrudEnum.Join)
                    {
                        Column(dic.TbAlias, col.ColumnName, X); Spacing(X); X.Append("desc");
                    }
                    else
                    {
                        Column(string.Empty, col.ColumnName, X); Spacing(X); X.Append("desc");
                    }
                }
            }
        }
        private void Limit()
        {
            if (DC.PageIndex.HasValue
                && DC.PageSize.HasValue)
            {
                var start = default(int);
                if (DC.PageIndex > 0)
                {
                    start = ((DC.PageIndex - 1) * DC.PageSize).ToInt();
                }
                CRLF(X); X.Append("limit"); Spacing(X); X.Append(start); Comma(X); X.Append(DC.PageSize);
            }
        }

        private void Distinct()
        {
            if (DC.Parameters.Any(it => IsDistinctParam(it)))
            {
                Distinct(X);
            }
        }
        private void CountMulti()
        {
            if (DC.IsMultiColCount)
            {
                X.Insert(0, "select count(*) \r\nfrom (\r\n");
                X.Append("\r\n         ) temp");
            }
        }
        private void CountSetMulti(bool isD, bool isDS)
        {
            if (isD)
            {
                Distinct();
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
        private void CountDistinct(List<DicParam> cols)
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
                        Function(count.Func, X, DC); LeftBracket(X); Distinct();
                        if (count.Crud == CrudEnum.Query)
                        {
                            Column(string.Empty, count.TbCol, X);
                        }
                        else if (count.Crud == CrudEnum.Join)
                        {
                            Column(count.TbAlias, count.TbCol, X);
                        }
                        RightBracket(X);
                    }
                }
                else
                {
                    if (cols.First().Columns == null
                        || cols.First().Columns.Count <= 0)
                    {
                        throw DC.Exception(XConfig.EC._024, "不应该出现的情况！");
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
                            Function(FuncEnum.Count, X, DC); LeftBracket(X); Distinct();
                            if (col.Crud == CrudEnum.Query)
                            {
                                Column(string.Empty, col.TbCol, X);
                            }
                            else if (col.Crud == CrudEnum.Join)
                            {
                                Column(col.TbAlias, col.TbCol, X);
                            }
                            RightBracket(X);
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
        private void CountNoneDistinct(List<DicParam> cols)
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
                        Function(count.Func, X, DC); LeftBracket(X); Star(X); RightBracket(X);
                    }
                    else
                    {
                        Function(count.Func, X, DC); LeftBracket(X);
                        if (count.Crud == CrudEnum.Query)
                        {
                            Column(string.Empty, count.TbCol, X);
                        }
                        else if (count.Crud == CrudEnum.Join)
                        {
                            Column(count.TbAlias, count.TbCol, X);
                        }
                        RightBracket(X);
                    }
                }
                else
                {
                    if (cols.First().Columns == null
                        || cols.First().Columns.Count <= 0)
                    {
                        throw DC.Exception(XConfig.EC._025, "不应该出现的情况！");
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
                            Function(FuncEnum.Count, X, DC); LeftBracket(X);
                            if (col.Crud == CrudEnum.Query)
                            {
                                Column(string.Empty, col.TbCol, X);
                            }
                            else if (col.Crud == CrudEnum.Join)
                            {
                                Column(col.TbAlias, col.TbCol, X);
                            }
                            RightBracket(X);
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
        private void Count()
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
        private void Sum()
        {
            Spacing(X);
            var col = DC.Parameters.FirstOrDefault(it => IsSelectColumnParam(it));
            var item = col.Columns.FirstOrDefault(it => it.Func == FuncEnum.Sum);
            if (item.Crud == CrudEnum.Query)
            {
                Function(item.Func, X, DC); LeftBracket(X); Column(string.Empty, item.TbCol, X); RightBracket(X);
            }
            else if (item.Crud == CrudEnum.Join)
            {
                Function(item.Func, X, DC); LeftBracket(X); Column(item.TbAlias, item.TbCol, X); RightBracket(X);
            }
        }

        private void End()
        {
            X.Append(';');
            DC.SQL.Add(X.ToString());
            X.Clear();
        }

        /****************************************************************************************************************/

        async Task<List<ColumnInfo>> ISqlProvider.GetColumnsInfos(string tableName)
        {
            DC.SQL.Clear();
            DC.SQL.Add($@"
                                                SELECT   obj.name AS TableName,
                                                        col.name AS ColumnName ,
                                                        t.name AS DataType  ,
                                                        ISNULL(comm.text, '') AS ColumnDefault ,
                                                        CASE WHEN col.isnullable = 1 THEN 'Yes'
                                                                ELSE 'No'
                                                        END AS IsNullable,
                                                        ISNULL(ep.[value], '') AS ColumnComment,
                                                        CASE 
					                                                WHEN EXISTS ( 
												                                                SELECT   1
												                                                FROM     dbo.sysindexes si
																                                                INNER JOIN dbo.sysindexkeys sik 
																				                                                ON si.id = sik.id
																							                                                AND si.indid = sik.indid
																                                                INNER JOIN dbo.syscolumns sc 
																				                                                ON sc.id = sik.id
																							                                                AND sc.colid = sik.colid
																                                                INNER JOIN dbo.sysobjects so 
																				                                                ON so.name = si.name
																							                                                AND so.xtype = 'PK'
												                                                WHERE    sc.id = col.id
															                                                AND sc.colid = col.colid 
											                                                ) 
					                                                THEN 'PK'
					                                                ELSE 'Other'
                                                        END AS KeyType
                                                FROM    dbo.syscolumns col
                                                        LEFT  JOIN dbo.systypes t ON col.xtype = t.xusertype
                                                        inner JOIN dbo.sysobjects obj ON col.id = obj.id
                                                                                            AND obj.xtype = 'U'
                                                                                            AND obj.status >= 0
                                                        LEFT  JOIN dbo.syscomments comm ON col.cdefault = comm.id
                                                        LEFT  JOIN sys.extended_properties ep ON col.id = ep.major_id
                                                                                                        AND col.colid = ep.minor_id
                                                                                                        AND ep.name = 'MS_Description'
                                                        LEFT  JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id
                                                                                                            AND epTwo.minor_id = 0
                                                                                                            AND epTwo.name = 'MS_Description'
                                                WHERE   obj.name = '{tableName.Trim().ToUpper()}'
                                                                or obj.name ='{tableName.Trim().ToLower()}'
                                                                or obj.name ='{tableName.Trim()}'
                                                                or obj.name ='{tableName}'
                                                ORDER BY col.colorder 
                                                ;
                                    ");
            return await DC.DS.ExecuteReaderMultiRowAsync<ColumnInfo>();
        }

        void ISqlProvider.GetSQL()
        {
            DC.SQL.Clear();
            switch (DC.Method)
            {
                case UiMethodEnum.CreateAsync:
                case UiMethodEnum.CreateBatchAsync:
                    InsertInto(X); Table(); InsertColumn(); Values(X); InsertValue(); End();
                    break;
                case UiMethodEnum.DeleteAsync:
                    Delete(X); From(X); Table(); Where(); End();
                    break;
                case UiMethodEnum.UpdateAsync:
                    Update(X); Table(); Set(X); UpdateColumn(); Where(); End();
                    break;
                case UiMethodEnum.TopAsync:
                case UiMethodEnum.QueryOneAsync:
                case UiMethodEnum.QueryListAsync:
                    Select(X); Distinct(); SelectColumn(); From(X); Table(); Where(); OrderBy(); Limit(); End();
                    break;
                case UiMethodEnum.QueryPagingAsync:
                    Select(X); Count(); From(X); Table(); Where(); CountMulti(); End();
                    Select(X); Distinct(); SelectColumn(); From(X); Table(); Where(); OrderBy(); Limit(); End();
                    break;
                case UiMethodEnum.ExistAsync:
                case UiMethodEnum.CountAsync:
                    Select(X); Count(); From(X); Table(); Where(); CountMulti(); End();
                    break;
                case UiMethodEnum.SumAsync:
                    Select(X); Sum(); From(X); Table(); Where(); End();
                    break;
            }
            if (XConfig.IsDebug)
            {
                XDebug.SQL = DC.SQL;
            }
        }
    }
}
