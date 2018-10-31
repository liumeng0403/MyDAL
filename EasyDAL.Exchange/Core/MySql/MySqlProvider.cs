
using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Core.MySql.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyDAL.Core.MySql
{
    internal class MySqlProvider
    {
        private Context DC { get; set; }

        private MySqlProvider() { }
        internal MySqlProvider(Context dc)
        {
            DC = dc;
            DC.SqlProvider = this;
        }

        /****************************************************************************************************************/

        private string LikeStrHandle(DicModelDB dic)
        {
            var name = dic.Param;
            var value = dic.DbValue.ToString(); // dic.CsValue;
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
        private string InStrHandle(DicModelDB dic)
        {
            var paras = DC.DbConditions.Where(it =>
            {
                if (!string.IsNullOrWhiteSpace(it.ParamRaw))
                {
                    return it.ParamRaw.Equals(dic.ParamRaw, StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    return false;
                }
            }).ToList();
            return $" {string.Join(",", paras.Select(it => $" @{it.Param} "))} ";
        }

        private string GetOrderByPart<M>()
        {
            var str = string.Empty;
            var key = DC.SC.GetModelKey(typeof(M).FullName);
            var cols = DC.SC.GetColumnInfos(key);
            var props = DC.SC.GetModelProperys(key);

            if (DC.DbConditions.Any(it => it.Action == ActionEnum.OrderBy))
            {
                var list = new List<string>();
                var orders = DC.DbConditions.Where(it => it.Action == ActionEnum.OrderBy);
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
            var dic = DC.DbConditions.First(it => !string.IsNullOrWhiteSpace(it.Key));
            var cols = DC.SC.GetColumnInfos(dic.Key);
            var props = DC.SC.GetModelProperys(dic.Key);

            if (DC.DbConditions.Any(it => it.Action == ActionEnum.OrderBy))
            {
                var list = new List<string>();
                var orders = DC.DbConditions.Where(it => it.Action == ActionEnum.OrderBy);
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

        private string Limit(int? pageIndex, int? pageSize)
        {
            if (pageIndex.HasValue
                && pageSize.HasValue)
            {
                var start = default(int);
                if (pageIndex > 0)
                {
                    start = ((pageIndex - 1) * pageSize).ToInt();
                }
                return $" \r\n limit {start},{pageSize}";
            }
            else
            {
                return string.Empty;
            }
        }

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
                    return " \r\n and ";
                case ActionEnum.Or:
                    return " \r\n or ";
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

        private string MultiCondition(DicModelDB db)
        {
            var list = new List<string>();
            foreach (var item in db.Group)
            {
                if (item.Group != null)
                {
                    list.Add(MultiCondition(item));
                }
                else
                {
                    list.Add($" `{item.ColumnOne}`{ConditionCompare(item.Compare)}@{item.Param} ");
                }
            }
            return string.Join(MultiConditionAction(db.GroupAction), list);
        }

        /****************************************************************************************************************/

        internal string GetSingleValuePart()
        {
            var str = string.Empty;

            foreach (var item in DC.DbConditions)
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
        internal string GetCountPart()
        {
            var str = string.Empty;

            var item = DC.DbConditions.FirstOrDefault(it => it.Option == OptionEnum.Count);
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

        internal string Columns()
        {
            var str = string.Empty;
            var list = new List<string>();

            foreach (var dic in DC.DbConditions)
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
                                        list.Add($" \t {dic.TableAliasOne}.`{dic.ColumnOne}` as {dic.ColumnAlias} ");
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
                                        list.Add($" \t `{dic.ColumnOne}` as {dic.ColumnAlias} ");
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

        private string Table<M>(UiMethodEnum type)
        {
            var tableName = string.Empty;
            if (type != UiMethodEnum.JoinQueryListAsync)
            {
                var key = DC.SC.GetModelKey(typeof(M).FullName);
                tableName = DC.SC.GetModelTableName(key);
            }

            return tableName;
        }

        internal string Joins()
        {
            var str = string.Empty;

            foreach (var item in DC.DbConditions)
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
                        str += $" \r\n \t \t {ConditionAction(item.Action)} {item.TableAliasOne}.`{item.ColumnOne}`={item.AliasTwo}.`{item.KeyTwo}` ";
                        break;
                }
            }

            return str;
        }

        internal string Wheres()
        {
            var str = string.Empty;

            //
            foreach (var db in DC.DbConditions)
            {
                if (DC.IsFilterCondition(db.Action))
                {
                    if (db.Group != null)
                    {
                        if (db.Crud == CrudTypeEnum.Join)
                        {
                            str += $" {ConditionAction(db.Action)} {db.TableAliasOne}.`{db.ColumnOne}`{ConditionCompare(db.Compare)}@{db.Param} ";
                        }
                        else if (DC.IsSingleTableOption(db.Crud))
                        {
                            str += $" {ConditionAction(db.Action)} ({MultiCondition(db)}) ";
                        }
                    }
                    else if (db.Option == OptionEnum.Compare)
                    {
                        if (db.Crud == CrudTypeEnum.Join)
                        {
                            str += $" {ConditionAction(db.Action)} {db.TableAliasOne}.`{db.ColumnOne}`{ConditionCompare(db.Compare)}@{db.Param} ";
                        }
                        else if (DC.IsSingleTableOption(db.Crud))
                        {
                            str += $" {ConditionAction(db.Action)} `{db.ColumnOne}`{ConditionCompare(db.Compare)}@{db.Param} ";
                        }
                    }
                    else if (db.Option == OptionEnum.Like)
                    {
                        if (db.Crud == CrudTypeEnum.Join)
                        {
                            str += $" {ConditionAction(db.Action)} {db.TableAliasOne}.`{db.ColumnOne}`{ConditionOption(db.Option)}{LikeStrHandle(db)} ";
                        }
                        else if (DC.IsSingleTableOption(db.Crud))
                        {
                            str += $" {ConditionAction(db.Action)} `{db.ColumnOne}`{ConditionOption(db.Option)}{LikeStrHandle(db)} ";
                        }
                    }
                    else if (db.Option == OptionEnum.CharLength)
                    {
                        if (db.Crud == CrudTypeEnum.Join)
                        {
                            str += $" {ConditionAction(db.Action)} {ConditionOption(db.Option)}({db.TableAliasOne}.`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                        }
                        else if (DC.IsSingleTableOption(db.Crud))
                        {
                            str += $" {ConditionAction(db.Action)} {ConditionOption(db.Option)}(`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                        }
                    }
                    else if (db.Option == OptionEnum.Trim || db.Option == OptionEnum.LTrim || db.Option == OptionEnum.RTrim)
                    {
                        if (db.Crud == CrudTypeEnum.Join)
                        {
                            str += $" {ConditionAction(db.Action)} {ConditionOption(db.Option)}({db.TableAliasOne}.`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                        }
                        else if (DC.IsSingleTableOption(db.Crud))
                        {
                            str += $" {ConditionAction(db.Action)} {ConditionOption(db.Option)}(`{db.ColumnOne}`){ConditionCompare(db.Compare)}@{db.Param} ";
                        }
                    }
                    else if (db.Option == OptionEnum.OneEqualOne)
                    {
                        str += $" {ConditionAction(db.Action)} @{db.Param} ";
                    }
                    else if (db.Option == OptionEnum.In || db.Option == OptionEnum.NotIn)
                    {
                        if (db.Crud == CrudTypeEnum.Join)
                        {
                            str += $" {ConditionAction(db.Action)} {db.TableAliasOne}.`{db.ColumnOne}` {ConditionOption(db.Option)}({InStrHandle(db)}) ";
                        }
                        else if (DC.IsSingleTableOption(db.Crud))
                        {
                            str += $" {ConditionAction(db.Action)} `{db.ColumnOne}` {ConditionOption(db.Option)}({InStrHandle(db)}) ";
                        }
                    }
                    else if (db.Option == OptionEnum.IsNull || db.Option == OptionEnum.IsNotNull)
                    {
                        str += $" {ConditionAction(db.Action)} `{db.ColumnOne}` {ConditionOption(db.Option)} ";
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(str)
                && DC.DbConditions.All(it => it.Action != ActionEnum.Where))
            {
                var aIdx = str.IndexOf(" and ", StringComparison.Ordinal);
                var oIdx = str.IndexOf(" or ", StringComparison.Ordinal);
                if (aIdx < oIdx)
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

        internal string GetUpdates()
        {
            if (!DC.DbConditions.Any(it => it.Action == ActionEnum.Update))
            {
                throw new Exception("没有设置任何要更新的字段!");
            }

            var list = new List<string>();

            foreach (var item in DC.DbConditions)
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

        internal async Task<List<ColumnInfo>> GetColumnsInfos(string tableName)
        {
            //TryGetTableName<M>(out var tableName);
            var sql = $@"
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
                                  ";
            return (await DC.DS.ExecuteReaderMultiRowAsync<ColumnInfo>(DC.Conn, sql, null)).ToList();
        }

        internal string GetColumns()
        {
            var list = new List<string>();
            foreach (var item in DC.DbConditions)
            {
                if (item.TvpIndex == 0)
                {
                    list.Add($"`{item.ColumnOne}`");
                }
            }
            return $" \r\n ({ string.Join(",", list)}) ";
        }
        internal string GetValues()
        {
            var list = new List<string>();
            for (var i = 0; i < DC.DbConditions.Max(it => it.TvpIndex) + 1; i++)
            {
                var values = new List<string>();
                foreach (var item in DC.DbConditions)
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

        private List<DicModelUI> FlatDics(List<DicModelUI> dics)
        {
            var ds = new List<DicModelUI>();

            //
            foreach (var d in dics)
            {
                if (DC.IsParameter(d.Action))
                {
                    if (d.Group != null)
                    {
                        ds.AddRange(FlatDics(d.Group));
                    }
                    else
                    {
                        ds.Add(d);
                    }
                }
            }

            //
            return ds;
        }
        private List<DicModelDB> FlatDics(List<DicModelDB> dics)
        {
            var ds = new List<DicModelDB>();

            //
            foreach (var d in dics)
            {
                if (DC.IsParameter(d.Action))
                {
                    if (d.Group != null)
                    {
                        ds.AddRange(FlatDics(d.Group));
                    }
                    else
                    {
                        ds.Add(d);
                    }
                }
            }

            //
            return ds;
        }
        internal DbParameters GetParameters(List<DicModelDB> dbs)
        {
            var paras = new DbParameters();

            //
            foreach (var db in dbs)
            {
                if (DC.IsParameter(db.Action))
                {
                    if (db.Group != null)
                    {
                        paras.Add(GetParameters(db.Group));
                    }
                    else
                    {
                        paras.Add(db.Param, db.DbValue, db.DbType);
                    }
                }
            }

            //
            if (XConfig.IsDebug)
            {
                lock (XDebug.Lock)
                {
                    XDebug.UIs = FlatDics(DC.UiConditions);
                    XDebug.DBs = FlatDics(DC.DbConditions);
                    XDebug.SetValue();
                }
            }

            //
            return paras;
        }

        internal string GetTableName<M>()
        {
            var tableName = string.Empty;
            tableName = DC.AH.GetAttributePropVal<M, XTableAttribute>(a => a.Name);
            if (tableName.IsNullStr())
            {
                tableName = DC.AH.GetAttributePropVal<M, TableAttribute>(a => a.Name);
            }
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new Exception($"类 [[{typeof(M).FullName}]] 必须是与 DB Table 对应的实体类,并且要由 TableAttribute 指定对应的表名!");
            }
            return $"`{tableName}`";
        }

        internal string GetTablePK(string fullName)
        {
            var key = DC.SC.GetModelKey(fullName);
            var col = DC.SC.GetColumnInfos(key).FirstOrDefault(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase));
            if (col == null)
            {
                throw new Exception($"类 [[{fullName}]] 对应的表 [[{col.TableName}]] 没有主键!");
            }
            return col.ColumnName;
        }

        internal List<string> GetSQL<M>(UiMethodEnum type, int? pageIndex = null, int? pageSize = null)
        {
            var list = new List<string>();

            //
            switch (type)
            {
                case UiMethodEnum.CreateAsync:
                    list.Add($" insert into {Table<M>(type)} {GetColumns()} \r\n values {GetValues()} ;");
                    break;
                case UiMethodEnum.CreateBatchAsync:
                    var tablex = Table<M>(type);
                    list.Add(
                                  $" LOCK TABLES {tablex} WRITE; " +
                                  $" \r\n /*!40000 ALTER TABLE {tablex} DISABLE KEYS */; " +
                                  $" \r\n insert into  {tablex} {GetColumns()} \r\n VALUES {GetValues()} ; " +
                                  $" \r\n /*!40000 ALTER TABLE {tablex} ENABLE KEYS */; " +
                                  $" \r\n UNLOCK TABLES; "
                                 );
                    break;
                case UiMethodEnum.DeleteAsync:
                    list.Add($" delete {From()} {Table<M>(type)} {Wheres()} ; ");
                    break;
                case UiMethodEnum.UpdateAsync:
                    list.Add($" update {Table<M>(type)} \r\n set {GetUpdates()} {Wheres()} ;");
                    break;
                case UiMethodEnum.QueryFirstOrDefaultAsync:
                    list.Add($"select {Columns()} {From()} {Table<M>(type)} {Wheres()} {GetOrderByPart<M>()} ; ");
                    break;
                case UiMethodEnum.JoinQueryFirstOrDefaultAsync:
                    list.Add($" select {Columns()} {From()} {Joins()} {Wheres()} {GetOrderByPart()} ; ");
                    break;
                case UiMethodEnum.QueryListAsync:
                case UiMethodEnum.TopAsync:
                    list.Add($"select {Columns()} {From()} {Table<M>(type)} {Wheres()} {GetOrderByPart<M>()} {Limit(pageIndex, pageSize)} ; ");
                    break;
                case UiMethodEnum.JoinQueryListAsync:
                case UiMethodEnum.JoinTopAsync:
                    list.Add($" select {Columns()} {From()} {Joins()} {Wheres()} {GetOrderByPart()} {Limit(pageIndex, pageSize)} ; ");
                    break;
                case UiMethodEnum.QueryPagingListAsync:
                    var wherePart8 = Wheres();
                    var table8 = Table<M>(type);
                    list.Add($"select count(*) {From()} {table8} {wherePart8} ; ");
                    list.Add($"select {Columns()} {From()} {table8} {wherePart8} {GetOrderByPart<M>()} {Limit(pageIndex, pageSize)}  ; ");
                    break;
                case UiMethodEnum.JoinQueryPagingListAsync:
                    var wherePart9 = Wheres();
                    list.Add($"select count(*) {From()} {Joins()} {wherePart9} ; ");
                    list.Add($"select {Columns()} {From()} {Joins()} {wherePart9} {GetOrderByPart()} {Limit(pageIndex, pageSize)}  ; ");
                    break;
                case UiMethodEnum.QueryAllAsync:
                    list.Add($" select {Columns()} {From()} {Table<M>(type)} {GetOrderByPart<M>()} ; ");
                    break;
                case UiMethodEnum.QueryAllPagingListAsync:
                    var table11 = Table<M>(type);
                    list.Add($"select count(*) {From()} {table11} ; ");
                    list.Add($"select * {From()} {table11} {GetOrderByPart<M>()} {Limit(pageIndex, pageSize)}  ; ");
                    break;
                case UiMethodEnum.QuerySingleValueAsync:
                    list.Add($" select {GetSingleValuePart()} {From()} {Table<M>(type)} {Wheres()} ; ");
                    break;
                case UiMethodEnum.ExistAsync:
                case UiMethodEnum.CountAsync:
                    list.Add($" select {GetCountPart()} {From()} {Table<M>(type)} {Wheres()} ; ");
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

            //
            return list;
        }
    }
}
