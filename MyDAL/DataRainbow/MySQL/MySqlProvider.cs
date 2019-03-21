using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.DataRainbow.XCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDAL.DataRainbow.MySQL
{
    internal sealed class MySqlProvider
        : SqlContext,ISqlProvider
    {
        private MySqlProvider() { }
        internal MySqlProvider(Context dc)
        {
            DC = dc;
            DC.SqlProvider = this;
            DbSql = new MySql();
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

        /****************************************************************************************************************/

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
        private void OneEqualOneProcess(DicParam db)
        {
            Spacing(X); DbParam(db.Param, X);
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
                        LeftRoundBracket(X); MultiCondition(item); RightRoundBracket(X);
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
                LeftRoundBracket(X); ConcatWithComma(ps.Inserts.Select(it => it.TbCol), MySql.Backquote, MySql.Backquote); RightRoundBracket(X);
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
                    DbSql.Column(string.Empty, item.TbCol, X); Equal(X); DbSql.Column(string.Empty, item.TbCol, X); Option(item.Option, X, DC); DbParam(item.Param, X);
                }
                else if (item.Option == OptionEnum.Set)
                {
                    if (i != 1) { CRLF(X); Tab(X); }
                    DbSql.Column(string.Empty, item.TbCol, X); Option(item.Option, X, DC); DbParam(item.Param, X);
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
        private void InsertValue()
        {
            Spacing(X);
            var items = DC.Parameters.Where(it => it.Action == ActionEnum.Insert && it.Option == OptionEnum.Insert)?.ToList();
            var i = 0;
            foreach (var dic in items)
            {
                i++;
                CRLF(X); LeftRoundBracket(X); ConcatWithComma(dic.Inserts.Select(it => it.Param), At, null); RightRoundBracket(X);
                if (i != items.Count)
                {
                    Comma(X);
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
                    LeftRoundBracket(X); MultiCondition(db); RightRoundBracket(X);
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

        private void DistinctX()
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
                Function(item.Func, X, DC); LeftRoundBracket(X); DbSql.Column(string.Empty, item.TbCol, X); RightRoundBracket(X);
            }
            else if (item.Crud == CrudEnum.Join)
            {
                Function(item.Func, X, DC); LeftRoundBracket(X); DbSql.Column(item.TbAlias, item.TbCol, X); RightRoundBracket(X);
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
                                            SELECT distinct
                                                TABLE_NAME as TableName,
                                                column_name as ColumnName,
                                                DATA_TYPE as DataType,
                                                column_default as ColumnDefault,
                                                is_nullable AS IsNullable,
                                                column_comment as ColumnComment,
                                                column_key as KeyType
                                            FROM
                                                information_schema.COLUMNS
                                            WHERE  ( 
                                                                table_schema='{DC.Conn.Database.Trim().ToUpper()}' 
                                                                or table_schema='{DC.Conn.Database.Trim().ToLower()}' 
                                                                or table_schema='{DC.Conn.Database.Trim()}' 
                                                                or table_schema='{DC.Conn.Database}' 
                                                            )
                                                            and  ( 
                                                                        TABLE_NAME = '{tableName.Trim().ToUpper()}' 
                                                                        or TABLE_NAME = '{tableName.Trim().ToLower()}' 
                                                                        or TABLE_NAME = '{tableName.Trim()}' 
                                                                        or TABLE_NAME = '{tableName}' 
                                                                      )
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
                    InsertInto(X); Table(DbSql.TableX, DbSql.Column); InsertColumn(); Values(X); InsertValue(); End();
                    break;
                case UiMethodEnum.DeleteAsync:
                    Delete(X); From(X); Table(DbSql.TableX, DbSql.Column); Where(); End();
                    break;
                case UiMethodEnum.UpdateAsync:
                    Update(X); Table(DbSql.TableX, DbSql.Column); Set(X); UpdateColumn(); Where(); End();
                    break;
                case UiMethodEnum.TopAsync:
                case UiMethodEnum.QueryOneAsync:
                case UiMethodEnum.QueryListAsync:
                    Select(X); DistinctX(); SelectColumn(); From(X); Table(DbSql.TableX, DbSql.Column); Where(); OrderBy(DbSql.GetIndex, DbSql.Column,OrderByParams); Limit(); End();
                    break;
                case UiMethodEnum.QueryPagingAsync:
                    Select(X); Count(); From(X); Table(DbSql.TableX, DbSql.Column); Where(); CountMulti(); End();
                    Select(X); DistinctX(); SelectColumn(); From(X); Table(DbSql.TableX, DbSql.Column); Where(); OrderBy(DbSql.GetIndex, DbSql.Column,OrderByParams); Limit(); End();
                    break;
                case UiMethodEnum.ExistAsync:
                case UiMethodEnum.CountAsync:
                    Select(X); Count(); From(X); Table(DbSql.TableX, DbSql.Column); Where(); CountMulti(); End();
                    break;
                case UiMethodEnum.SumAsync:
                    Select(X); Sum(); From(X); Table(DbSql.TableX, DbSql.Column); Where(); End();
                    break;
            }
            if (XConfig.IsDebug)
            {
                XDebug.SQL = DC.SQL;
            }
        }
    }
}
