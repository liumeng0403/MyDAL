using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Extensions;

namespace Yunyong.DataExchange.DataRainbow.MySQL
{
    internal sealed class MySqlProvider
        : SqlContext, ISqlProvider
    {
        private Context DC { get; set; }
        private StringBuilder X { get; set; } = new StringBuilder();

        private MySqlProvider() { }
        internal MySqlProvider(Context dc)
        {
            DC = dc;
            DC.SqlProvider = this;
        }

        /****************************************************************************************************************/

        private void OrderByHandle1()
        {
            var orders = DC.Parameters.Where(it => IsOrderByParam(it)).ToList();
            var i = 0;
            foreach (var o in orders)
            {
                i++;
                if (o.Func == FuncEnum.None
                    || o.Func == FuncEnum.Column)
                {
                    if (DC.Crud == CrudTypeEnum.Join)
                    {
                        X.Append(o.TableAliasOne); Dot(X); Backquote(X); X.Append(o.ColumnOne); Backquote(X); Spacing(X); X.Append(Option(o.Option));
                    }
                    else
                    {
                        Backquote(X); X.Append(o.ColumnOne); Backquote(X); Spacing(X); X.Append(Option(o.Option));
                    }
                }
                else if (o.Func == FuncEnum.CharLength)
                {
                    if (DC.Crud == CrudTypeEnum.Join)
                    {
                        X.Append(Function(o.Func));
                        LeftBracket(X); X.Append(o.TableAliasOne); Dot(X); Backquote(X); X.Append(o.ColumnOne); Backquote(X); RightBracket(X); Spacing(X); X.Append(Option(o.Option));
                    }
                    else
                    {
                        X.Append(Function(o.Func)); LeftBracket(X); Backquote(X); X.Append(o.ColumnOne); Backquote(X); RightBracket(X); Spacing(X); X.Append(Option(o.Option));
                    }
                }
                else
                {
                    throw new Exception($"{XConfig.EC._013} -- [[{o.Action}-{o.Option}-{o.Func}]] 不能解析!!!");
                }
                if (i != orders.Count) { Comma(X); }
            }
        }
        private void InStrHandle(List<DicParam> dbs)
        {
            var i = 0;
            foreach (var it in dbs)
            {
                i++;
                AT(X); X.Append(it.Param);
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
                LeftBracket(X); SingleQuote(X); Percent(X); SingleQuote(X); Comma(X); AT(X); X.Append(name); Comma(X); SingleQuote(X); Percent(X); SingleQuote(X); RightBracket(X);
            }
            else if ((value.Contains("%") || value.Contains("_"))
                && !value.Contains("/%")
                && !value.Contains("/_"))
            {
                AT(X); X.Append(name);
            }
            else if (value.Contains("/%")
                || value.Contains("/_"))
            {
                AT(X); X.Append(name); Spacing(X); Escape(X); Spacing(X); SingleQuote(X); EscapeChar(X); SingleQuote(X);
            }
            else
            {
                throw new Exception($"{XConfig.EC._015} -- [[{dic.Action}-{dic.Option}-{value}]] 不能解析!!!");
            }
        }

        /****************************************************************************************************************/

        private void CharLengthProcess(DicParam db)
        {
            Spacing(X);
            X.Append(Function(db.Func)); LeftBracket(X);
            if (db.Crud == CrudTypeEnum.Join) { X.Append(db.TableAliasOne); Dot(X); }
            else if (DC.IsSingleTableOption()) { }
            Backquote(X); X.Append(db.ColumnOne); Backquote(X); RightBracket(X);
            X.Append(Compare(db.Compare)); AT(X); X.Append(db.Param);
        }
        private void DateFormatProcess(DicParam db)
        {
            Spacing(X);
            X.Append(Function(db.Func)); LeftBracket(X);
            if (db.Crud == CrudTypeEnum.Join) { X.Append(db.TableAliasOne); Dot(X); }
            else if (DC.IsSingleTableOption()) { }
            Backquote(X); X.Append(db.ColumnOne); Backquote(X); Comma(X); SingleQuote(X); X.Append(db.Format); SingleQuote(X);
            RightBracket(X); X.Append(Compare(db.Compare)); AT(X); X.Append(db.Param);
        }
        private void TrimProcess(DicParam db)
        {
            Spacing(X);
            X.Append(Function(db.Func)); LeftBracket(X);
            if (db.Crud == CrudTypeEnum.Join) { X.Append(db.TableAliasOne); Dot(X); }
            else if (DC.IsSingleTableOption()) { }
            Backquote(X); X.Append(db.ColumnOne); Backquote(X); RightBracket(X);
            X.Append(Compare(db.Compare)); AT(X); X.Append(db.Param);
        }
        private void InProcess(DicParam db)
        {
            Spacing(X);
            if (db.Crud == CrudTypeEnum.Join) { X.Append(db.TableAliasOne); Dot(X); }
            else if (DC.IsSingleTableOption()) { }
            Backquote(X); X.Append(db.ColumnOne); Backquote(X); Spacing(X);
            X.Append(Function(db.Func)); LeftBracket(X); InStrHandle(db.InItems); RightBracket(X);
        }

        /****************************************************************************************************************/

        private void CompareProcess(DicParam db)
        {
            Spacing(X);
            if (db.Crud == CrudTypeEnum.Join) { X.Append(db.TableAliasOne); Dot(X); }
            else if (DC.IsSingleTableOption()) { }
            Backquote(X); X.Append(db.ColumnOne); Backquote(X); X.Append(Compare(db.Compare)); AT(X); X.Append(db.Param);
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
            else if (db.Func == FuncEnum.In || db.Func == FuncEnum.NotIn)
            {
                InProcess(db);
            }
            else
            {
                throw new Exception($"{XConfig.EC._006} -- [[{db.Func}]] 不能处理!!!");
            }
        }
        private void LikeProcess(DicParam db)
        {
            Spacing(X);
            if (db.Crud == CrudTypeEnum.Join) { X.Append(db.TableAliasOne); Dot(X); }
            else if (DC.IsSingleTableOption()) { }
            Backquote(X); X.Append(db.ColumnOne); Backquote(X); X.Append(Option(db.Option)); LikeStrHandle(db);
        }
        private void OneEqualOneProcess(DicParam db)
        {
            Spacing(X);
            AT(X); X.Append(db.Param);
        }
        private void IsNullProcess(DicParam db)
        {
            Spacing(X);
            if (db.Crud == CrudTypeEnum.Join) { X.Append(db.TableAliasOne); Dot(X); }
            else if (DC.IsSingleTableOption()) { }
            Backquote(X); X.Append(db.ColumnOne); Backquote(X); Spacing(X); X.Append(Option(db.Option));
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
                        MultiAction(db.GroupAction, X);
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
                else if (db.Option == OptionEnum.Like)
                {
                    LikeProcess(db);
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
                    throw new Exception($"{XConfig.EC._011} -- [[{db.Action}-{db.Option}]] -- 不能解析!!!");
                }
            }
        }

        /****************************************************************************************************************/

        private void InsertColumn()
        {
            Spacing(X);
            var ps = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.Insert && (it.Option == OptionEnum.Insert || it.Option == OptionEnum.InsertTVP));
            if (ps != null)
            {
                CRLF(X);
                LeftBracket(X); ConcatWithComma(ps.Inserts.Select(it => it.ColumnOne), Backquote, Backquote); RightBracket(X);
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
                    throw new Exception($"{DC.Set} -- 字段:[[{string.Join(",", list.Where(it => it.ParamInfo.Value == DBNull.Value).Select(it => it.ColumnOne))}]]的值不能设为 Null !!!");
                }
            }
            else if (DC.Set == SetEnum.IgnoreNull)
            {
                list = list.Where(it => it.ParamInfo.Value != DBNull.Value)?.ToList();
                if (list == null || list.Count == 0) { throw new Exception("没有设置任何要更新的字段!"); }
            }
            else
            {
                throw new Exception($"{XConfig.EC._012} -- [[{DC.Set}]] 不能解析!!!");
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
                    Backquote(X); X.Append(item.ColumnOne); Backquote(X);
                    Equal(X);
                    Backquote(X); X.Append(item.ColumnOne); Backquote(X); X.Append(Option(item.Option)); AT(X); X.Append(item.Param);
                }
                else if (item.Option == OptionEnum.Set)
                {
                    if (i != 1) { CRLF(X); Tab(X); }
                    Backquote(X); X.Append(item.ColumnOne); Backquote(X); X.Append(Option(item.Option)); AT(X); X.Append(item.Param);
                }
                else
                {
                    throw new Exception($"{XConfig.EC._009} -- [[{item.Action}-{item.Option}]] 不能解析!!!");
                }
                if (i != list.Count) { Comma(X); }
            }
        }
        private void SelectColumn()
        {
            Spacing(X);
            var items = DC.Parameters.Where(it => it.Action == ActionEnum.Select && (it.Option == OptionEnum.Column || it.Option == OptionEnum.ColumnAs))?.ToList();
            if (items == null || items.Count == 0) { Star(X); }
            var i = 0;
            foreach (var dic in items)
            {
                i++;
                if (i != 1) { CRLF(X); Tab(X); }
                if (dic.Func == FuncEnum.None)
                {
                    if (dic.Crud == CrudTypeEnum.Join)
                    {
                        if (dic.Option == OptionEnum.Column)
                        {
                            X.Append(dic.TableAliasOne); Dot(X); Backquote(X); X.Append(dic.ColumnOne); Backquote(X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            X.Append(dic.TableAliasOne); Dot(X); Backquote(X); X.Append(dic.ColumnOne); Backquote(X); As(X); X.Append(dic.ColumnOneAlias);
                        }
                    }
                    else if (dic.Crud == CrudTypeEnum.Query)
                    {
                        if (dic.Option == OptionEnum.Column)
                        {
                            Backquote(X); X.Append(dic.ColumnOne); Backquote(X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            Backquote(X); X.Append(dic.ColumnOne); Backquote(X); As(X); X.Append(dic.ColumnOneAlias);
                        }
                    }
                }
                else if (dic.Func == FuncEnum.DateFormat)
                {
                    if (dic.Crud == CrudTypeEnum.Join)
                    {
                        if (dic.Option == OptionEnum.Column)
                        {
                            X.Append(Function(dic.Func)); LeftBracket(X);
                            X.Append(dic.TableAliasOne); Dot(X); Backquote(X); X.Append(dic.ColumnOne); Backquote(X); Comma(X);
                            SingleQuote(X); X.Append(dic.Format); SingleQuote(X); RightBracket(X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            X.Append(Function(dic.Func)); LeftBracket(X);
                            X.Append(dic.TableAliasOne); Dot(X); Backquote(X); X.Append(dic.ColumnOne); Backquote(X); Comma(X);
                            SingleQuote(X); X.Append(dic.Format); SingleQuote(X); RightBracket(X); As(X); X.Append(dic.ColumnOneAlias);
                        }
                    }
                    else if (dic.Crud == CrudTypeEnum.Query)
                    {
                        if (dic.Option == OptionEnum.Column)
                        {
                            X.Append(Function(dic.Func)); LeftBracket(X);
                            Backquote(X); X.Append(dic.ColumnOne); Backquote(X); Comma(X);
                            SingleQuote(X); X.Append(dic.Format); SingleQuote(X); RightBracket(X);
                        }
                        else if (dic.Option == OptionEnum.ColumnAs)
                        {
                            X.Append(Function(dic.Func)); LeftBracket(X);
                            Backquote(X); X.Append(dic.ColumnOne); Backquote(X); Comma(X);
                            SingleQuote(X); X.Append(dic.Format); SingleQuote(X); RightBracket(X); As(X); X.Append(dic.ColumnOneAlias);
                        }
                    }
                }
                else
                {
                    throw new Exception($"{XConfig.EC._007} -- [[{dic.Func}]] 不能解析!!!");
                }
                if (i != items.Count) { Comma(X); }
            }
        }
        private void InsertValue()
        {
            Spacing(X);
            var items = DC.Parameters.Where(it => it.Action == ActionEnum.Insert && (it.Option == OptionEnum.Insert || it.Option == OptionEnum.InsertTVP))?.ToList();
            var i = 0;
            foreach (var dic in items)
            {
                i++;
                CRLF(X); LeftBracket(X); ConcatWithComma(dic.Inserts.Select(it => it.Param), AT, null); RightBracket(X);
                if (i != items.Count)
                {
                    Comma(X);
                }
            }
        }

        private void Table()
        {
            Spacing(X);
            if (DC.Crud == CrudTypeEnum.Join)
            {
                var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
                Backquote(X); X.Append(dic.TableOne); Backquote(X); As(X); X.Append(dic.TableAliasOne);
                Join();
            }
            else
            {
                Backquote(X); X.Append(DC.SC.GetTableName(DC.SC.GetModelKey(DC.SingleOpName))); Backquote(X);
            }
        }
        private void Join()
        {
            Spacing(X);
            foreach (var item in DC.Parameters)
            {
                if (item.Crud != CrudTypeEnum.Join) { continue; }
                switch (item.Action)
                {
                    case ActionEnum.From: break;    // 已处理 
                    case ActionEnum.InnerJoin:
                    case ActionEnum.LeftJoin:
                        CRLF(X); Tab(X); Action(item.Action, X); Spacing(X); X.Append(item.TableOne); As(X); X.Append(item.TableAliasOne);
                        break;
                    case ActionEnum.On:
                        CRLF(X); Tab(X); Tab(X); Action(item.Action, X); Spacing(X);
                        X.Append(item.TableAliasOne); Dot(X); Backquote(X); X.Append(item.ColumnOne); Backquote(X);
                        X.Append(Compare(item.Compare));
                        X.Append(item.TableAliasTwo); Dot(X); Backquote(X); X.Append(item.ColumnTwo); Backquote(X);
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
                    Action(ActionEnum.Where, X); Spacing(X); X.Append("true"); Spacing(X);
                }
                else
                {
                    Action(ActionEnum.Where, X); Spacing(X); X.Append("false"); Spacing(X);
                }
            }
            foreach (var db in cons)
            {
                CRLF(X); Action(db.Action, X); Spacing(X);
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
            CRLF(X);
            X.Append("order by"); Spacing(X);
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            var key = dic != null ? dic.Key : DC.SC.GetModelKey(DC.SingleOpName);
            var cols = DC.SC.GetColumnInfos(key);
            var props = DC.SC.GetModelProperys(key);

            if (DC.Parameters.Any(it => it.Action == ActionEnum.OrderBy))
            {
                OrderByHandle1();
            }
            else if (props.Any(it => "CreatedOn".Equals(it.Name, StringComparison.OrdinalIgnoreCase)))
            {
                if (DC.Crud == CrudTypeEnum.Join)
                {
                    X.Append(dic.TableAliasOne); Dot(X);
                    Backquote(X); X.Append(props.First(it => "CreatedOn".Equals(it.Name, StringComparison.OrdinalIgnoreCase)).Name); Backquote(X); Spacing(X); X.Append("desc");
                }
                else
                {
                    Backquote(X); X.Append(props.First(it => "CreatedOn".Equals(it.Name, StringComparison.OrdinalIgnoreCase)).Name); Backquote(X); Spacing(X); X.Append("desc");
                }
            }
            else if (cols.Any(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)))
            {
                var pris = cols.Where(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)).ToList();
                var i = 0;
                foreach (var it in pris)
                {
                    i++;
                    if (DC.Crud == CrudTypeEnum.Join)
                    {
                        X.Append(dic.TableAliasOne); Dot(X); Backquote(X); X.Append(it.ColumnName); Backquote(X); Spacing(X); X.Append("desc");
                    }
                    else
                    {
                        Backquote(X); X.Append(it.ColumnName); Backquote(X); Spacing(X); X.Append("desc");
                    }
                    if (i != pris.Count) { Comma(X); }
                }
            }
            else
            {
                if (DC.Crud == CrudTypeEnum.Join)
                {
                    X.Append(dic.TableAliasOne); Dot(X); Backquote(X); X.Append(props.First().Name); Backquote(X); Spacing(X); X.Append("desc");
                }
                else
                {
                    Backquote(X); X.Append(props.First().Name); Backquote(X); Spacing(X); X.Append("desc");
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
            if (DC.Parameters.Any(it => it.Option == OptionEnum.Distinct))
            {
                Spacing(X); X.Append("distinct"); Spacing(X);
            }
        }
        private void CountCD()
        {
            /* 
             * count(*)
             * count(col)
             * count(distinct col)
             */
            Spacing(X);
            var item = DC.Parameters.FirstOrDefault(it => it.Option == OptionEnum.Count);
            if (item != null)
            {
                if (item.Crud == CrudTypeEnum.Query)
                {
                    if ("*".Equals(item.ColumnOne, StringComparison.OrdinalIgnoreCase))
                    {
                        X.Append(Option(item.Option)); LeftBracket(X); X.Append(item.ColumnOne); RightBracket(X);
                    }
                    else
                    {
                        X.Append(Option(item.Option)); LeftBracket(X); Distinct(); Backquote(X); X.Append(item.ColumnOne); Backquote(X); RightBracket(X);
                    }
                }
                else if (item.Crud == CrudTypeEnum.Join)
                {
                    if ("*".Equals(item.ColumnOne, StringComparison.OrdinalIgnoreCase))
                    {
                        X.Append(Option(item.Option)); LeftBracket(X); X.Append(item.ColumnOne); RightBracket(X);
                    }
                    else
                    {
                        X.Append(Option(item.Option)); LeftBracket(X); Distinct(); X.Append(item.TableAliasOne); Dot(X); Backquote(X); X.Append(item.ColumnOne); Backquote(X); RightBracket(X);
                    }
                }
            }
            else
            {
                X.Append("count(*)");
            }
        }
        private void Sum()
        {
            Spacing(X);
            var item = DC.Parameters.FirstOrDefault(it => it.Option == OptionEnum.Sum);
            if (item.Crud == CrudTypeEnum.Query)
            {
                X.Append(Option(item.Option)); LeftBracket(X); Backquote(X); X.Append(item.ColumnOne); Backquote(X); RightBracket(X);
            }
            else if (item.Crud == CrudTypeEnum.Join)
            {
                X.Append(Option(item.Option)); LeftBracket(X); X.Append(item.TableAliasOne); Dot(X); Backquote(X); X.Append(item.ColumnOne); Backquote(X); RightBracket(X);
            }
        }

        /****************************************************************************************************************/

        string ISqlProvider.GetTableName<M>()
        {
            var tableName = string.Empty;
            tableName = DC.AH.GetAttributePropVal<M, XTableAttribute>(a => a.Name);
            if (tableName.IsNullStr())
            {
                tableName = DC.AH.GetAttributePropVal<M, TableAttribute>(a => a.Name);
            }
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new Exception($"类 [[{typeof(M).FullName}]] 必须是与 DB Table 对应的实体类,并且要由 [XTable] 或 [Table] 标记指定类对应的表名!!!");
            }
            return tableName;
        }
        async Task<List<ColumnInfo>> ISqlProvider.GetColumnsInfos(string tableName)
        {
            DC.SQL = new List<string>{
                                $@"
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
                                  "
            };
            return await DC.DS.ExecuteReaderMultiRowAsync<ColumnInfo>();
        }
        void ISqlProvider.GetSQL()
        {
            var list = new List<string>();

            //
            switch (DC.Method)
            {
                case UiMethodEnum.CreateAsync:
                case UiMethodEnum.CreateBatchAsync:
                    InsertInto(X); Table(); InsertColumn(); Values(X); InsertValue(); End(X);
                    list.Add(X.ToString()); X.Clear();
                    break;
                case UiMethodEnum.DeleteAsync:
                    Delete(X); From(X); Table(); Where(); End(X);
                    list.Add(X.ToString()); X.Clear();
                    break;
                case UiMethodEnum.UpdateAsync:
                    Update(X); Table(); Set(X); UpdateColumn(); Where(); End(X);
                    list.Add(X.ToString()); X.Clear();
                    break;
                case UiMethodEnum.TopAsync:
                case UiMethodEnum.ListAsync:
                case UiMethodEnum.AllAsync:
                case UiMethodEnum.FirstOrDefaultAsync:
                    Select(X); Distinct(); SelectColumn(); From(X); Table(); Where(); OrderBy(); Limit(); End(X);
                    list.Add(X.ToString()); X.Clear();
                    break;
                case UiMethodEnum.PagingListAsync:
                case UiMethodEnum.PagingAllListAsync:
                    Select(X); CountCD(); From(X); Table(); Where(); End(X);
                    list.Add(X.ToString()); X.Clear();
                    Select(X); Distinct(); SelectColumn(); From(X); Table(); Where(); OrderBy(); Limit(); End(X);
                    list.Add(X.ToString()); X.Clear();
                    break;
                case UiMethodEnum.ExistAsync:
                case UiMethodEnum.CountAsync:
                    Select(X); CountCD(); From(X); Table(); Where(); End(X);
                    list.Add(X.ToString()); X.Clear();
                    break;
                case UiMethodEnum.SumAsync:
                    Select(X); Sum(); From(X); Table(); Where(); End(X);
                    list.Add(X.ToString()); X.Clear();
                    break;
            }

            //
            if (XConfig.IsDebug)
            {
                XDebug.SQL = list;
            }
            DC.SQL = list;
        }
    }
}
