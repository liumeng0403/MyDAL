using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyDAL.DBRainbow.MySQL
{
    internal class MySqlProvider
        : ISqlProvider
    {
        private Context DC { get; set; }

        private MySqlProvider() { }
        internal MySqlProvider(Context dc)
        {
            DC = dc;
            DC.SqlProvider = this;
        }

        /****************************************************************************************************************/

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
        private string InStrHandle(List<DicParam> dbs)
        {
            return $" {string.Join(",", dbs.Select(it => $" @{it.Param} "))} ";
        }

        private string GetOrderByPart<M>()
        {
            var str = string.Empty;
            var key = DC.SC.GetModelKey(typeof(M).FullName);
            var cols = DC.SC.GetColumnInfos(key);
            var props = DC.SC.GetModelProperys(key);

            if (DC.Parameters.Any(it => it.Action == ActionEnum.OrderBy))
            {
                var list = new List<string>();
                var orders = DC.Parameters.Where(it => it.Action == ActionEnum.OrderBy);
                foreach (var o in orders)
                {
                    if (o.Func == FuncEnum.None
                        || o.Func == FuncEnum.Column)
                    {
                        list.Add($" `{o.ColumnOne}` {ConditionOption(o.Option)} ");
                    }
                    else if (o.Func == FuncEnum.CharLength)
                    {
                        list.Add($" {ConditionFunc(o.Func)}(`{o.ColumnOne}`) {ConditionOption(o.Option)} ");
                    }
                }
                str = string.Join(",", list);
            }
            else if (props.Any(it => "CreatedOn".Equals(it.Name, StringComparison.OrdinalIgnoreCase)))
            {
                str = $" `{props.First(it => "CreatedOn".Equals(it.Name, StringComparison.OrdinalIgnoreCase)).Name}` desc ";
            }
            else if (cols.Any(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)))
            {
                str = string.Join(",", cols.Where(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)).Select(it => $" `{it.ColumnName}` desc "));
            }
            else
            {
                str = $" `{DC.SC.GetModelProperys(DC.SC.GetModelKey(typeof(M).FullName)).First().Name}` desc ";
            }

            str = $" \r\n order by {str}";

            return str;
        }
        private string GetOrderByPart()
        {
            var str = string.Empty;
            var dic = DC.Parameters.First(it => !string.IsNullOrWhiteSpace(it.Key));
            var cols = DC.SC.GetColumnInfos(dic.Key);
            var props = DC.SC.GetModelProperys(dic.Key);

            if (DC.Parameters.Any(it => it.Action == ActionEnum.OrderBy))
            {
                var list = new List<string>();
                var orders = DC.Parameters.Where(it => it.Action == ActionEnum.OrderBy);
                foreach (var o in orders)
                {
                    if (o.Func == FuncEnum.None
                        || o.Func == FuncEnum.Column)
                    {
                        list.Add($" {o.TableAliasOne}.`{o.ColumnOne}` {ConditionOption(o.Option)} ");
                    }
                    else if (o.Func == FuncEnum.CharLength)
                    {
                        list.Add($" {ConditionFunc(o.Func)}({o.TableAliasOne}.`{o.ColumnOne}`) {ConditionOption(o.Option)} ");
                    }
                }
                str = string.Join(",", list);
            }
            else if (props.Any(it => "CreatedOn".Equals(it.Name, StringComparison.OrdinalIgnoreCase)))
            {
                str = $" {dic.TableAliasOne}.`{props.First(it => "CreatedOn".Equals(it.Name, StringComparison.OrdinalIgnoreCase)).Name}` desc ";
            }
            else if (cols.Any(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)))
            {
                str = string.Join(",", cols.Where(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)).Select(it => $" {dic.TableAliasOne}.`{it.ColumnName}` desc "));
            }
            else
            {
                str = $" {dic.TableAliasOne}.`{DC.SC.GetModelProperys(dic.Key).First().Name}` desc ";
            }

            str = $" \r\n order by {str}";

            return str;
        }

        private string Limit()
        {
            if (DC.PageIndex.HasValue
                && DC.PageSize.HasValue)
            {
                var start = default(int);
                if (DC.PageIndex > 0)
                {
                    start = ((DC.PageIndex - 1) * DC.PageSize).ToInt();
                }
                return $" \r\n limit {start},{DC.PageSize}";
            }
            else
            {
                return string.Empty;
            }
        }

        /****************************************************************************************************************/

        private string CompareProcess(DicParam db, bool isMulti)
        {
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {db.TableAliasOne}.`{db.ColumnOne}`{ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" `{db.ColumnOne}`{ConditionCompare(db.Compare)}@{db.Param} ";
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {ConditionAction(db.Action)} {db.TableAliasOne}.`{db.ColumnOne}`{ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {ConditionAction(db.Action)} `{db.ColumnOne}`{ConditionCompare(db.Compare)}@{db.Param} ";
                }
            }
            throw new Exception("CompareProcess 未能处理!!!");
        }
        private string LikeProcess(DicParam db, bool isMulti)
        {
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {db.TableAliasOne}.`{db.ColumnOne}`{ConditionOption(db.Option)}{LikeStrHandle(db)} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" `{db.ColumnOne}`{ConditionOption(db.Option)}{LikeStrHandle(db)} ";
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {ConditionAction(db.Action)} {db.TableAliasOne}.`{db.ColumnOne}`{ConditionOption(db.Option)}{LikeStrHandle(db)} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {ConditionAction(db.Action)} `{db.ColumnOne}`{ConditionOption(db.Option)}{LikeStrHandle(db)} ";
                }
            }
            throw new Exception("LikeProcess 未能处理!!!");
        }
        private string CharLengthProcess(DicParam db, bool isMulti)
        {
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {ConditionOption(db.Option)}({db.TableAliasOne}.`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {ConditionOption(db.Option)}(`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {ConditionAction(db.Action)} {ConditionOption(db.Option)}({db.TableAliasOne}.`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {ConditionAction(db.Action)} {ConditionOption(db.Option)}(`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                }
            }
            throw new Exception("CharLengthProcess 未能处理!!!");
        }
        private string TrimProcess(DicParam db, bool isMulti)
        {
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {ConditionOption(db.Option)}({db.TableAliasOne}.`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {ConditionOption(db.Option)}(`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {ConditionAction(db.Action)} {ConditionOption(db.Option)}({db.TableAliasOne}.`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {ConditionAction(db.Action)} {ConditionOption(db.Option)}(`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                }
            }
            throw new Exception("TrimProcess 未能处理!!!");
        }
        private string OneEqualOneProcess(DicParam db, bool isMulti)
        {
            if (isMulti)
            {
                return $" @{db.Param} ";
            }
            else
            {
                return $" {ConditionAction(db.Action)} @{db.Param} ";
            }
            throw new Exception("OneEqualOneProcess 未能处理!!!");
        }
        private string InProcess(DicParam db, bool isMulti)
        {
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {db.TableAliasOne}.`{db.ColumnOne}` {ConditionOption(db.Option)}({InStrHandle(db.InItems)}) ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" `{db.ColumnOne}` {ConditionOption(db.Option)}({InStrHandle(db.InItems)}) ";
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {ConditionAction(db.Action)} {db.TableAliasOne}.`{db.ColumnOne}` {ConditionOption(db.Option)}({InStrHandle(db.InItems)}) ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {ConditionAction(db.Action)} `{db.ColumnOne}` {ConditionOption(db.Option)}({InStrHandle(db.InItems)}) ";
                }
            }
            throw new Exception("InProcess 未能处理!!!");
        }
        private string IsNullProcess(DicParam db, bool isMulti)
        {
            if (isMulti)
            {
                return $" `{db.ColumnOne}` {ConditionOption(db.Option)} ";
            }
            else
            {
                return $" {ConditionAction(db.Action)} `{db.ColumnOne}` {ConditionOption(db.Option)} ";
            }
            throw new Exception("IsNullProcess 未能处理!!!");
        }

        /****************************************************************************************************************/

        private static string ConditionAction(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.None:
                    return "";
                case ActionEnum.Insert:
                    return "";
                case ActionEnum.Update:
                    return "";
                case ActionEnum.Select:
                    return "";
                case ActionEnum.From:
                    return "";
                case ActionEnum.InnerJoin:
                    return " inner join ";
                case ActionEnum.LeftJoin:
                    return " left join ";
                case ActionEnum.On:
                    return " on ";
                case ActionEnum.Where:
                    return " \r\n where ";
                case ActionEnum.And:
                    return " \r\n \t and ";
                case ActionEnum.Or:
                    return " \r\n \t or ";
                case ActionEnum.OrderBy:
                    return "";
            }
            return " ";
        }
        private static string MultiConditionAction(ActionEnum action)
        {
            switch (action)
            {
                case ActionEnum.And:
                    return " && ";
                case ActionEnum.Or:
                    return " || ";
            }
            return " ";
        }
        private static string ConditionOption(OptionEnum option)
        {
            switch (option)
            {
                case OptionEnum.None:
                    return "<<<<<";
                case OptionEnum.Insert:
                    return "";
                case OptionEnum.InsertTVP:
                    return "";
                case OptionEnum.Set:
                    return "=";
                case OptionEnum.ChangeAdd:
                    return "+";
                case OptionEnum.ChangeMinus:
                    return "-";
                case OptionEnum.Column:
                    return "";
                case OptionEnum.ColumnAs:
                    break;
                case OptionEnum.Compare:
                    return "";
                case OptionEnum.Like:
                    return " like ";
                case OptionEnum.In:
                    return " in ";
                case OptionEnum.InHelper:
                    break;
                case OptionEnum.NotIn:
                    return " not in ";
                case OptionEnum.Count:
                    return " count";
                case OptionEnum.CharLength:
                    return " char_length";
                case OptionEnum.Trim:
                    return " trim";
                case OptionEnum.LTrim:
                    return " ltrim";
                case OptionEnum.RTrim:
                    return " rtrim";
                case OptionEnum.OneEqualOne:
                    return "";
                case OptionEnum.IsNull:
                    return " is null ";
                case OptionEnum.IsNotNull:
                    return " is not null ";
                case OptionEnum.Asc:
                    return " asc ";
                case OptionEnum.Desc:
                    return " desc ";
            }
            return " ";
        }
        private static string ConditionCompare(CompareEnum compare)
        {
            switch (compare)
            {
                case CompareEnum.None:
                    return " ";
                case CompareEnum.Equal:
                    return "=";
                case CompareEnum.NotEqual:
                    return "<>";
                case CompareEnum.LessThan:
                    return "<";
                case CompareEnum.LessThanOrEqual:
                    return "<=";
                case CompareEnum.GreaterThan:
                    return ">";
                case CompareEnum.GreaterThanOrEqual:
                    return ">=";
                case CompareEnum.Like:
                    return " like ";
                case CompareEnum.In:
                    return " in ";
                case CompareEnum.NotIn:
                    return " not in ";
            }
            return " ";
        }
        private static string ConditionFunc(FuncEnum func)
        {
            switch (func)
            {
                case FuncEnum.None:
                    return "";
                case FuncEnum.Column:
                    return "";
                case FuncEnum.CharLength:
                    return " char_length";
            }
            return " ";
        }

        /****************************************************************************************************************/

        private string MultiCondition(DicParam db, bool isMulti)
        {
            if (db.Group != null)
            {
                var list = new List<string>();
                foreach (var item in db.Group)
                {
                    if (item.Group != null)
                    {
                        list.Add($"({MultiCondition(item, true)})");
                    }
                    else
                    {
                        list.Add(MultiCondition(item, isMulti));
                    }
                }
                return string.Join(MultiConditionAction(db.GroupAction), list);
            }
            else
            {
                if (db.Option == OptionEnum.Compare)
                {
                    return CompareProcess(db, isMulti);
                }
                else if (db.Option == OptionEnum.Like)
                {
                    return LikeProcess(db, isMulti);
                }
                else if (db.Option == OptionEnum.CharLength)
                {
                    return CharLengthProcess(db, isMulti);
                }
                else if (db.Option == OptionEnum.Trim || db.Option == OptionEnum.LTrim || db.Option == OptionEnum.RTrim)
                {
                    return TrimProcess(db, isMulti);
                }
                else if (db.Option == OptionEnum.OneEqualOne)
                {
                    return OneEqualOneProcess(db, isMulti);
                }
                else if (db.Option == OptionEnum.In || db.Option == OptionEnum.NotIn)
                {
                    return InProcess(db, isMulti);
                }
                else if (db.Option == OptionEnum.IsNull || db.Option == OptionEnum.IsNotNull)
                {
                    return IsNullProcess(db, isMulti);
                }
                return string.Empty;
            }
        }

        /****************************************************************************************************************/

        private string GetSingleValuePart()
        {
            var str = string.Empty;

            foreach (var item in DC.Parameters)
            {
                switch (item.Option)
                {
                    case OptionEnum.Count:
                        str = GetCountPart();
                        break;
                }
            }

            return str;
        }
        private string GetCountPart()
        {
            var str = string.Empty;

            var item = DC.Parameters.FirstOrDefault(it => it.Option == OptionEnum.Count);
            if (item.Crud == CrudTypeEnum.Query)
            {
                if ("*".Equals(item.ColumnOne, StringComparison.OrdinalIgnoreCase))
                {
                    str = $" {ConditionOption(item.Option)}({item.ColumnOne}) ";
                }
                else
                {
                    str = $" {ConditionOption(item.Option)}(`{item.ColumnOne}`) ";
                }
            }
            else if (item.Crud == CrudTypeEnum.Join)
            {
                if ("*".Equals(item.ColumnOne, StringComparison.OrdinalIgnoreCase))
                {
                    str = $" {ConditionOption(item.Option)}({item.ColumnOne}) ";
                }
                else
                {
                    str = $" {ConditionOption(item.Option)}({item.TableAliasOne}.`{item.ColumnOne}`) ";
                }
            }

            return str;
        }

        private string Columns()
        {
            var str = string.Empty;
            var list = new List<string>();

            foreach (var dic in DC.Parameters)
            {
                if (dic.Option != OptionEnum.Column
                    && dic.Option != OptionEnum.ColumnAs)
                {
                    continue;
                }

                switch (dic.Action)
                {
                    case ActionEnum.Select:
                        switch (dic.Crud)
                        {
                            case CrudTypeEnum.Join:
                                switch (dic.Option)
                                {
                                    case OptionEnum.Column:
                                        list.Add($" \t {dic.TableAliasOne}.`{dic.ColumnOne}` ");
                                        break;
                                    case OptionEnum.ColumnAs:
                                        list.Add($" \t {dic.TableAliasOne}.`{dic.ColumnOne}` as {dic.ColumnOneAlias} ");
                                        break;
                                }

                                break;
                            case CrudTypeEnum.Query:
                                switch (dic.Option)
                                {
                                    case OptionEnum.Column:
                                        list.Add($" \t `{dic.ColumnOne}` ");
                                        break;
                                    case OptionEnum.ColumnAs:
                                        list.Add($" \t `{dic.ColumnOne}` as {dic.ColumnOneAlias} ");
                                        break;
                                }
                                break;
                        }
                        break;
                }
            }

            if (list.Count > 0)
            {
                str = string.Join(",\r\n", list);
            }
            else
            {
                str = "*";
            }
            return str;
        }

        private string From()
        {
            return "\r\n from";
        }

        private string Table<M>()
        {
            var tableName = string.Empty;
            if (DC.Method != UiMethodEnum.JoinCountAsync
                && DC.Method != UiMethodEnum.JoinQueryFirstOrDefaultAsync
                && DC.Method != UiMethodEnum.JoinQueryListAsync
                && DC.Method != UiMethodEnum.JoinQueryPagingListAsync
                && DC.Method != UiMethodEnum.JoinTopAsync)
            {
                var key = DC.SC.GetModelKey(typeof(M).FullName);
                tableName = DC.SC.GetModelTableName(key);
            }

            return tableName;
        }

        private string Joins()
        {
            var str = string.Empty;

            foreach (var item in DC.Parameters)
            {
                if (item.Crud != CrudTypeEnum.Join)
                {
                    continue;
                }

                switch (item.Action)
                {
                    case ActionEnum.From:
                        str += $" {item.TableOne} as {item.TableAliasOne} ";
                        break;
                    case ActionEnum.InnerJoin:
                    case ActionEnum.LeftJoin:
                        str += $" \r\n \t {ConditionAction(item.Action)} {item.TableOne} as {item.TableAliasOne} ";
                        break;
                    case ActionEnum.On:
                        str += $" \r\n \t \t {ConditionAction(item.Action)} {item.TableAliasOne}.`{item.ColumnOne}`={item.TableAliasTwo}.`{item.ColumnTwo}` ";
                        break;
                }
            }

            return str;
        }

        private string Wheres()
        {
            var str = string.Empty;

            //
            foreach (var db in DC.Parameters)
            {
                if (DC.IsFilterCondition(db.Action))
                {
                    str += db.Group == null ? MultiCondition(db, false) : $" {ConditionAction(db.Action)} ({MultiCondition(db, true)}) ";
                }
            }

            if (!str.IsNullStr()
                && DC.Parameters.All(it => it.Action != ActionEnum.Where))
            {
                var aIdx = str.IndexOf(" and ", StringComparison.OrdinalIgnoreCase);
                var oIdx = str.IndexOf(" or ", StringComparison.OrdinalIgnoreCase);
                if (aIdx < oIdx
                    || oIdx == -1)
                {
                    str = $" {ConditionAction(ActionEnum.Where)} true {str} ";
                }
                else
                {
                    str = $" {ConditionAction(ActionEnum.Where)} false {str} ";
                }
            }

            return str;
        }

        private string GetUpdates()
        {
            if (!DC.Parameters.Any(it => it.Action == ActionEnum.Update))
            {
                throw new Exception("没有设置任何要更新的字段!");
            }

            var list = new List<string>();

            foreach (var item in DC.Parameters)
            {
                switch (item.Action)
                {
                    case ActionEnum.Update:
                        switch (item.Option)
                        {
                            case OptionEnum.ChangeAdd:
                            case OptionEnum.ChangeMinus:
                                list.Add($" `{item.ColumnOne}`=`{item.ColumnOne}`{ConditionOption(item.Option)}@{item.Param} ");
                                break;
                            case OptionEnum.Set:
                                list.Add($" `{item.ColumnOne}`{ConditionOption(item.Option)}@{item.Param} ");
                                break;
                        }
                        break;
                }
            }

            return string.Join(", \r\n\t", list);
        }

        private string GetColumns()
        {
            var list = new List<string>();
            foreach (var item in DC.Parameters)
            {
                if (item.TvpIndex == 0)
                {
                    list.Add($"`{item.ColumnOne}`");
                }
            }
            return $" \r\n ({ string.Join(",", list)}) ";
        }
        private string GetValues()
        {
            var list = new List<string>();
            for (var i = 0; i < DC.Parameters.Max(it => it.TvpIndex) + 1; i++)
            {
                var values = new List<string>();
                foreach (var item in DC.Parameters)
                {
                    if (item.TvpIndex == i)
                    {
                        values.Add($"@{item.Param}");
                    }
                }
                list.Add($" \r\n ({string.Join(",", values)})");
            }
            return string.Join(",", list);
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
                throw new Exception($"类 [[{typeof(M).FullName}]] 必须是与 DB Table 对应的实体类,并且要由 XTableAttribute 或 TableAttribute 指定对应的表名!");
            }
            return $"`{tableName}`";
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
                                        WHERE  table_schema='{DC.Conn.Database}'
                                            and  ( TABLE_NAME = '{tableName.TrimStart('`').TrimEnd('`').ToLower()}' or TABLE_NAME = '{tableName.TrimStart('`').TrimEnd('`')}' )
                                        ;
                                  "
            };
            return await DC.DS.ExecuteReaderMultiRowAsync<ColumnInfo>(null);
        }

        string ISqlProvider.GetTablePK(string fullName)
        {
            var key = DC.SC.GetModelKey(fullName);
            var col = DC.SC.GetColumnInfos(key).FirstOrDefault(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase));
            if (col == null)
            {
                throw new Exception($"类 [[{fullName}]] 对应的表 [[{col.TableName}]] 没有主键!");
            }
            return col.ColumnName;
        }

        void ISqlProvider.GetSQL<M>()
        {
            var list = new List<string>();

            //
            switch (DC.Method)
            {
                case UiMethodEnum.CreateAsync:
                    list.Add($" insert into {Table<M>()} {GetColumns()} \r\n values {GetValues()} ;");
                    break;
                case UiMethodEnum.CreateBatchAsync:
                    var tablex = Table<M>();
                    list.Add(
                                  $" LOCK TABLES {tablex} WRITE; " +
                                  $" \r\n /*!40000 ALTER TABLE {tablex} DISABLE KEYS */; " +
                                  $" \r\n insert into  {tablex} {GetColumns()} \r\n VALUES {GetValues()} ; " +
                                  $" \r\n /*!40000 ALTER TABLE {tablex} ENABLE KEYS */; " +
                                  $" \r\n UNLOCK TABLES; "
                                 );
                    break;
                case UiMethodEnum.DeleteAsync:
                    list.Add($" delete {From()} {Table<M>()} {Wheres()} ; ");
                    break;
                case UiMethodEnum.UpdateAsync:
                    list.Add($" update {Table<M>()} \r\n set {GetUpdates()} {Wheres()} ;");
                    break;
                case UiMethodEnum.QueryFirstOrDefaultAsync:
                    list.Add($"select {Columns()} {From()} {Table<M>()} {Wheres()} {GetOrderByPart<M>()} ; ");
                    break;
                case UiMethodEnum.JoinQueryFirstOrDefaultAsync:
                    list.Add($" select {Columns()} {From()} {Joins()} {Wheres()} {GetOrderByPart()} ; ");
                    break;
                case UiMethodEnum.QueryListAsync:
                case UiMethodEnum.TopAsync:
                    list.Add($"select {Columns()} {From()} {Table<M>()} {Wheres()} {GetOrderByPart<M>()} {Limit()} ; ");
                    break;
                case UiMethodEnum.JoinQueryListAsync:
                case UiMethodEnum.JoinTopAsync:
                    list.Add($" select {Columns()} {From()} {Joins()} {Wheres()} {GetOrderByPart()} {Limit()} ; ");
                    break;
                case UiMethodEnum.QueryPagingListAsync:
                    var wherePart8 = Wheres();
                    var table8 = Table<M>();
                    list.Add($"select count(*) {From()} {table8} {wherePart8} ; ");
                    list.Add($"select {Columns()} {From()} {table8} {wherePart8} {GetOrderByPart<M>()} {Limit()}  ; ");
                    break;
                case UiMethodEnum.JoinQueryPagingListAsync:
                    var wherePart9 = Wheres();
                    list.Add($"select count(*) {From()} {Joins()} {wherePart9} ; ");
                    list.Add($"select {Columns()} {From()} {Joins()} {wherePart9} {GetOrderByPart()} {Limit()}  ; ");
                    break;
                case UiMethodEnum.QueryAllAsync:
                    list.Add($" select {Columns()} {From()} {Table<M>()} {GetOrderByPart<M>()} ; ");
                    break;
                case UiMethodEnum.QueryAllPagingListAsync:
                    var table11 = Table<M>();
                    list.Add($"select count(*) {From()} {table11} ; ");
                    list.Add($"select * {From()} {table11} {GetOrderByPart<M>()} {Limit()}  ; ");
                    break;
                case UiMethodEnum.QuerySingleValueAsync:
                    list.Add($" select {GetSingleValuePart()} {From()} {Table<M>()} {Wheres()} ; ");
                    break;
                case UiMethodEnum.ExistAsync:
                case UiMethodEnum.CountAsync:
                    list.Add($" select {GetCountPart()} {From()} {Table<M>()} {Wheres()} ; ");
                    break;
                case UiMethodEnum.JoinCountAsync:
                    list.Add($" select {GetCountPart()} {From()} {Joins()} {Wheres()} ");
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
