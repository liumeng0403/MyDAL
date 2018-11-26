using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDAL.DataRainbow.MySQL
{
    internal class MySqlProvider
        : XSQL, ISqlProvider
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

        private string OrderByHandle1()
        {
            var list = new List<string>();
            var orders = DC.Parameters.Where(it => it.Action == ActionEnum.OrderBy);
            foreach (var o in orders)
            {
                if (o.Func == FuncEnum.None
                    || o.Func == FuncEnum.Column)
                {
                    if (DC.Crud == CrudTypeEnum.Join)
                    {
                        list.Add($" {o.TableAliasOne}.`{o.ColumnOne}` {Option(o.Option)} ");
                    }
                    else
                    {
                        list.Add($" `{o.ColumnOne}` {Option(o.Option)} ");
                    }
                }
                else if (o.Func == FuncEnum.CharLength)
                {
                    if (DC.Crud == CrudTypeEnum.Join)
                    {
                        list.Add($" {Function(o.Func)}({o.TableAliasOne}.`{o.ColumnOne}`) {Option(o.Option)} ");
                    }
                    else
                    {
                        list.Add($" {Function(o.Func)}(`{o.ColumnOne}`) {Option(o.Option)} ");
                    }
                }
            }
            return string.Join(",", list);
        }

        /****************************************************************************************************************/

        private void ConcatWithComma(StringBuilder sb, IEnumerable<string> ss, Action<StringBuilder> preSymbol, Action<StringBuilder> afterSymbol)
        {
            var n = ss.Count();
            var i = 0;
            foreach (var s in ss)
            {
                i++;
                preSymbol?.Invoke(sb); sb.Append(s); afterSymbol?.Invoke(sb);
                if (i != n)
                {
                    Comma(sb);
                }
            }
        }
        private string LikeStrHandle(DicParam dic)
        {
            var name = dic.Param;
            var value = dic.ParamInfo.Value.ToString(); // dic.CsValue;
            if (!value.Contains("%")
                && !value.Contains("_"))
            {
                return $"CONCAT('%',@{name},'%')";
            }
            else if ((value.Contains("%") || value.Contains("_"))
                && !value.Contains("/%")
                && !value.Contains("/_"))
            {
                return $"@{name}";
            }
            else if (value.Contains("/%")
                || value.Contains("/_"))
            {
                return $"@{name} escape '/' ";
            }

            throw new Exception(value);
        }
        private void CharLengthProcess(DicParam db, bool isMulti, StringBuilder sb)
        {
            Spacing(sb);
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(Function(db.Func));
                    LeftBracket(sb); sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
                else if (DC.IsSingleTableOption())
                {
                    sb.Append(Function(db.Func));
                    LeftBracket(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(Action(db.Action)); Spacing(sb);
                    sb.Append(Function(db.Func));
                    LeftBracket(sb); sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
                else if (DC.IsSingleTableOption())
                {
                    sb.Append(Action(db.Action)); Spacing(sb);
                    sb.Append(Function(db.Func));
                    LeftBracket(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
            }
        }
        private void DateFormatProcess(DicParam db, bool isMulti, StringBuilder sb)
        {
            Spacing(sb);
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(Function(db.Func));
                    LeftBracket(sb);
                    sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); Comma(sb); SingleQuote(sb); sb.Append(db.Format); SingleQuote(sb);
                    RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
                else if (DC.IsSingleTableOption())
                {
                    sb.Append(Function(db.Func));
                    LeftBracket(sb);
                    Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); Comma(sb); SingleQuote(sb); sb.Append(db.Format); SingleQuote(sb);
                    RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(Action(db.Action)); Spacing(sb);
                    sb.Append(Function(db.Func));
                    LeftBracket(sb);
                    sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); Comma(sb); SingleQuote(sb); sb.Append(db.Format); SingleQuote(sb);
                    RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
                else if (DC.IsSingleTableOption())
                {
                    sb.Append(Action(db.Action)); Spacing(sb);
                    sb.Append(Function(db.Func));
                    LeftBracket(sb);
                    Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); Comma(sb); SingleQuote(sb); sb.Append(db.Format); SingleQuote(sb);
                    RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
            }
        }
        private void TrimProcess(DicParam db, bool isMulti, StringBuilder sb)
        {
            Spacing(sb);
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(Function(db.Func)); LeftBracket(sb); sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
                else if (DC.IsSingleTableOption())
                {
                    sb.Append(Function(db.Func)); LeftBracket(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(Action(db.Action)); Spacing(sb);
                    sb.Append(Function(db.Func)); LeftBracket(sb); sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
                else if (DC.IsSingleTableOption())
                {
                    sb.Append(Action(db.Action)); Spacing(sb);
                    sb.Append(Function(db.Func)); LeftBracket(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); RightBracket(sb);
                    sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
            }
        }
        private void InProcess(DicParam db, bool isMulti, StringBuilder sb)
        {
            Spacing(sb);
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); Spacing(sb);
                    sb.Append(Function(db.Func)); LeftBracket(sb); sb.Append(InStrHandle(db.InItems)); RightBracket(sb);
                }
                else if (DC.IsSingleTableOption())
                {
                    Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); Spacing(sb);
                    sb.Append(Function(db.Func)); LeftBracket(sb); sb.Append(InStrHandle(db.InItems)); RightBracket(sb);
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(Action(db.Action)); Spacing(sb); sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); Spacing(sb);
                    sb.Append(Function(db.Func)); LeftBracket(sb); sb.Append(InStrHandle(db.InItems)); RightBracket(sb);
                }
                else if (DC.IsSingleTableOption())
                {
                    sb.Append(Action(db.Action)); Spacing(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); Spacing(sb);
                    sb.Append(Function(db.Func)); LeftBracket(sb); sb.Append(InStrHandle(db.InItems)); RightBracket(sb);
                }
            }
        }

        /****************************************************************************************************************/

        private void CompareProcess(DicParam db, bool isMulti, StringBuilder sb)
        {
            Spacing(sb);
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
                else if (DC.IsSingleTableOption())
                {
                    Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(Action(db.Action)); Spacing(sb);
                    sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
                else if (DC.IsSingleTableOption())
                {
                    sb.Append(Action(db.Action)); Spacing(sb);
                    Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); sb.Append(Compare(db.Compare)); AT(sb); sb.Append(db.Param);
                }
            }
        }
        private void FunctionProcess(DicParam db, bool isMulti, StringBuilder sb)
        {
            if (db.Func == FuncEnum.CharLength)
            {
                CharLengthProcess(db, isMulti, sb);
            }
            else if (db.Func == FuncEnum.DateFormat)
            {
                DateFormatProcess(db, isMulti, sb);
            }
            else if (db.Func == FuncEnum.Trim || db.Func == FuncEnum.LTrim || db.Func == FuncEnum.RTrim)
            {
                TrimProcess(db, isMulti, sb);
            }
            else if (db.Func == FuncEnum.In || db.Func == FuncEnum.NotIn)
            {
                InProcess(db, isMulti, sb);
            }
            else
            {
                throw new Exception($"{XConfig.EC._006} -- [[{db.Func}]] 不能处理!!!");
            }
        }
        private void LikeProcess(DicParam db, bool isMulti, StringBuilder sb)
        {
            Spacing(sb);
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); sb.Append(Option(db.Option)); sb.Append(LikeStrHandle(db));
                }
                else if (DC.IsSingleTableOption())
                {
                    Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); sb.Append(Option(db.Option)); sb.Append(LikeStrHandle(db));
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    sb.Append(Action(db.Action)); Spacing(sb); sb.Append(db.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb);
                    sb.Append(Option(db.Option));
                    sb.Append(LikeStrHandle(db));
                }
                else if (DC.IsSingleTableOption())
                {
                    sb.Append(Action(db.Action)); Spacing(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); sb.Append(Option(db.Option)); sb.Append(LikeStrHandle(db));
                }
            }
        }
        private void OneEqualOneProcess(DicParam db, bool isMulti, StringBuilder sb)
        {
            Spacing(sb);
            if (isMulti)
            {
                AT(sb); sb.Append(db.Param);
            }
            else
            {
                sb.Append(Action(db.Action)); Spacing(sb); AT(sb); sb.Append(db.Param);
            }
        }
        private string InStrHandle(List<DicParam> dbs)
        {
            return $" {string.Join(",", dbs.Select(it => $" @{it.Param} "))} ";
        }
        private void IsNullProcess(DicParam db, bool isMulti, StringBuilder sb)
        {
            Spacing(sb);
            if (isMulti)
            {
                Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); Spacing(sb); sb.Append(Option(db.Option));
            }
            else
            {
                sb.Append(Action(db.Action)); Spacing(sb); Backquote(sb); sb.Append(db.ColumnOne); Backquote(sb); Spacing(sb); sb.Append(Option(db.Option));
            }
        }

        /****************************************************************************************************************/

        private void MultiCondition(DicParam db, bool isMulti, StringBuilder sb)
        {
            if (db.Group != null)
            {
                var i = 0;
                foreach (var item in db.Group)
                {
                    i++;
                    if (item.Group != null)
                    {
                        LeftBracket(sb); MultiCondition(item, true, sb); RightBracket(sb);
                    }
                    else
                    {
                        MultiCondition(item, isMulti, sb);
                    }
                    if (i != db.Group.Count)
                    {
                        MultiAction(db.GroupAction, sb);
                    }
                }
            }
            else
            {
                if (db.Option == OptionEnum.Compare)
                {
                    CompareProcess(db, isMulti, sb);
                }
                else if (db.Option == OptionEnum.Function)
                {
                    FunctionProcess(db, isMulti, sb);
                }
                else if (db.Option == OptionEnum.Like)
                {
                    LikeProcess(db, isMulti, sb);
                }
                else if (db.Option == OptionEnum.OneEqualOne)
                {
                    OneEqualOneProcess(db, isMulti, sb);
                }
                else if (db.Option == OptionEnum.IsNull || db.Option == OptionEnum.IsNotNull)
                {
                    IsNullProcess(db, isMulti, sb);
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
                LeftBracket(X); ConcatWithComma(X, ps.Inserts.Select(it => it.ColumnOne), Backquote, Backquote); RightBracket(X);
            }
        }
        private void UpdateColumn()
        {
            var list = DC.Parameters.Where(it => it.Action == ActionEnum.Update)?.ToList();
            if (list == null || list.Count == 0) { throw new Exception("没有设置任何要更新的字段!"); }
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
                CRLF(X); LeftBracket(X); ConcatWithComma(X, dic.Inserts.Select(it => it.Param), AT, null); RightBracket(X);
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
                        CRLF(X); Tab(X); X.Append(Action(item.Action)); Spacing(X); X.Append(item.TableOne); As(X); X.Append(item.TableAliasOne);
                        break;
                    case ActionEnum.On:
                        CRLF(X); Tab(X); Tab(X); X.Append(Action(item.Action)); Spacing(X);
                        X.Append(item.TableAliasOne); Dot(X); Backquote(X); X.Append(item.ColumnOne); Backquote(X);
                        X.Append(Compare(item.Compare));
                        X.Append(item.TableAliasTwo); Dot(X); Backquote(X); X.Append(item.ColumnTwo); Backquote(X);
                        break;
                }
            }
        }
        private void Where()
        {
            Spacing(X);
            var cons = DC.Parameters.Where(it => it.Action == ActionEnum.Where || it.Action == ActionEnum.And || it.Action == ActionEnum.Or)?.ToList();
            if(cons==null)
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
                    X.Append(Action(ActionEnum.Where)); Spacing(X); X.Append("true"); Spacing(X);
                }
                else
                {
                    X.Append(Action(ActionEnum.Where)); Spacing(X); X.Append("false"); Spacing(X);
                }
            }
            foreach (var db in cons)
            {
                if (db.Group == null)
                {
                    MultiCondition(db, false, X);
                }
                else
                {
                    X.Append(Action(db.Action)); Spacing(X); LeftBracket(X); MultiCondition(db, true, X); RightBracket(X);
                }
            }
        }
        private void OrderBy(StringBuilder sb)
        {
            CRLF(sb);
            sb.Append("order by");
            var str = string.Empty;
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            var key = dic != null ? dic.Key : DC.SC.GetModelKey(DC.SingleOpName);
            var cols = DC.SC.GetColumnInfos(key);
            var props = DC.SC.GetModelProperys(key);

            if (DC.Parameters.Any(it => it.Action == ActionEnum.OrderBy))
            {
                str = OrderByHandle1();
            }
            else if (props.Any(it => "CreatedOn".Equals(it.Name, StringComparison.OrdinalIgnoreCase)))
            {
                if (DC.Crud == CrudTypeEnum.Join)
                {
                    str = $" {dic.TableAliasOne}.`{props.First(it => "CreatedOn".Equals(it.Name, StringComparison.OrdinalIgnoreCase)).Name}` desc ";
                }
                else
                {
                    str = $" `{props.First(it => "CreatedOn".Equals(it.Name, StringComparison.OrdinalIgnoreCase)).Name}` desc ";
                }
            }
            else if (cols.Any(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)))
            {
                if (DC.Crud == CrudTypeEnum.Join)
                {
                    str = string.Join(",", cols.Where(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)).Select(it => $" {dic.TableAliasOne}.`{it.ColumnName}` desc "));
                }
                else
                {
                    str = string.Join(",", cols.Where(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)).Select(it => $" `{it.ColumnName}` desc "));
                }
            }
            else
            {
                if (DC.Crud == CrudTypeEnum.Join)
                {
                    str = $" {dic.TableAliasOne}.`{props.First().Name}` desc ";
                }
                else
                {
                    str = $" `{props.First().Name}` desc ";
                }
            }

            sb.Append(str);
        }
        private void Limit(StringBuilder sb)
        {
            if (DC.PageIndex.HasValue
                && DC.PageSize.HasValue)
            {
                var start = default(int);
                if (DC.PageIndex > 0)
                {
                    start = ((DC.PageIndex - 1) * DC.PageSize).ToInt();
                }
                CRLF(sb);
                sb.Append("limit");
                Spacing(sb);
                sb.Append(start);
                Comma(sb);
                sb.Append(DC.PageSize);
            }
        }

        private void Set(StringBuilder sb)
        {
            CRLF(sb);
            sb.Append("set");
        }
        private void Distinct(StringBuilder sb)
        {
            if (DC.Parameters.Any(it => it.Option == OptionEnum.Distinct))
            {
                Spacing(sb);
                sb.Append("distinct");
                Spacing(sb);
            }
        }
        private void As(StringBuilder sb)
        {
            Spacing(sb);
            sb.Append("as");
            Spacing(sb);
        }
        private void CountCD(StringBuilder sb)
        {
            /* 
             * count(*)
             * count(col)
             * count(distinct col)
             */
            Spacing(sb);
            var item = DC.Parameters.FirstOrDefault(it => it.Option == OptionEnum.Count);
            if (item != null)
            {
                if (item.Crud == CrudTypeEnum.Query)
                {
                    if ("*".Equals(item.ColumnOne, StringComparison.OrdinalIgnoreCase))
                    {
                        sb.Append(Option(item.Option));
                        LeftBracket(sb);
                        sb.Append(item.ColumnOne);
                        RightBracket(sb);
                    }
                    else
                    {
                        sb.Append(Option(item.Option));
                        LeftBracket(sb);
                        Distinct(sb);
                        Backquote(sb);
                        sb.Append(item.ColumnOne);
                        Backquote(sb);
                        RightBracket(sb);
                    }
                }
                else if (item.Crud == CrudTypeEnum.Join)
                {
                    if ("*".Equals(item.ColumnOne, StringComparison.OrdinalIgnoreCase))
                    {
                        sb.Append(Option(item.Option));
                        LeftBracket(sb);
                        sb.Append(item.ColumnOne);
                        RightBracket(sb);
                    }
                    else
                    {
                        sb.Append(Option(item.Option));
                        LeftBracket(sb);
                        Distinct(sb);
                        sb.Append(item.TableAliasOne);
                        Dot(sb);
                        Backquote(sb);
                        sb.Append(item.ColumnOne);
                        Backquote(sb);
                        RightBracket(sb);
                    }
                }
            }
            else
            {
                sb.Append("count(*)");
            }
        }
        private void Sum(StringBuilder sb)
        {
            Spacing(sb);
            var item = DC.Parameters.FirstOrDefault(it => it.Option == OptionEnum.Sum);
            if (item.Crud == CrudTypeEnum.Query)
            {
                sb.Append(Option(item.Option)); LeftBracket(sb); Backquote(sb); sb.Append(item.ColumnOne); Backquote(sb); RightBracket(sb);
            }
            else if (item.Crud == CrudTypeEnum.Join)
            {
                sb.Append(Option(item.Option)); LeftBracket(sb); sb.Append(item.TableAliasOne); Dot(sb); Backquote(sb); sb.Append(item.ColumnOne); Backquote(sb); RightBracket(sb);
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
                    InsertInto(X); Table(); InsertColumn(); Values(X); InsertValue(); End(X);
                    list.Add(X.ToString()); X.Clear();
                    break;
                case UiMethodEnum.CreateBatchAsync:
                    LockTables(X); Table(); Write(X); End(X);
                    DisableKeysS(X); Table(); DisableKeysE(X); End(X);
                    InsertInto(X); Table(); InsertColumn(); Values(X); InsertValue(); End(X);
                    EnableKeysS(X); Table(); EnableKeysE(X); End(X);
                    UnlockTables(X); End(X);
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
                    Select(X); Distinct(X); SelectColumn(); From(X); Table(); Where(); OrderBy(X); Limit(X); End(X);
                    list.Add(X.ToString()); X.Clear();
                    break;
                case UiMethodEnum.PagingListAsync:
                case UiMethodEnum.PagingAllListAsync:
                    Select(X); CountCD(X); From(X); Table(); Where(); End(X);
                    list.Add(X.ToString()); X.Clear();
                    Select(X); Distinct(X); SelectColumn(); From(X); Table(); Where(); OrderBy(X); Limit(X); End(X);
                    list.Add(X.ToString()); X.Clear();
                    break;
                case UiMethodEnum.ExistAsync:
                case UiMethodEnum.CountAsync:
                    Select(X); CountCD(X); From(X); Table(); Where(); End(X);
                    list.Add(X.ToString()); X.Clear();
                    break;
                case UiMethodEnum.SumAsync:
                    Select(X); Sum(X); From(X); Table(); Where(); End(X);
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
