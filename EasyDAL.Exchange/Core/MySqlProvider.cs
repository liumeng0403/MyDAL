using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.Extensions;
using Yunyong.DataExchange.Helper;

namespace Yunyong.DataExchange.Core
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

        private string LikeStrHandle(DicModel dic)
        {
            var name = dic.Param;
            var value = dic.CsValue;
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
        private string InStrHandle(DicModel dic)
        {
            var paras = DC.Conditions.Where(it =>
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
            var cols = DC.SC.GetColumnInfos(DC.SC.GetKey(typeof(M).FullName, DC.Conn.Database));

            if (DC.Conditions.Any(it => it.Action == ActionEnum.OrderBy))
            {
                str = string.Join(",", DC.Conditions.Where(it => it.Action == ActionEnum.OrderBy).Select(it => $" `{it.ColumnOne}` {it.Option.ToEnumDesc<OptionEnum>()} "));
            }
            else if (cols.Any(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)))
            {
                str = string.Join(",", cols.Where(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)).Select(it => $" `{it.ColumnName}` desc "));
            }
            else
            {
                str = $" `{DC.SC.GetModelProperys(DC.SC.GetKey(typeof(M).FullName, DC.Conn.Database)).First().Name}` desc ";
            }

            str = $" \r\n order by {str}";

            return str;
        }
        private string GetOrderByPart()
        {
            var str = string.Empty;
            var dic = DC.Conditions.First(it => !string.IsNullOrWhiteSpace(it.ClassFullName));
            var key = DC.SC.GetKey(dic.ClassFullName, DC.Conn.Database);
            var cols = DC.SC.GetColumnInfos(key);

            if (DC.Conditions.Any(it => it.Action == ActionEnum.OrderBy))
            {
                str = string.Join(",", DC.Conditions.Where(it => it.Action == ActionEnum.OrderBy).Select(it => $" {dic.TableAliasOne}.`{it.ColumnOne}` {it.Option.ToEnumDesc<OptionEnum>()} "));
            }
            else if (cols.Any(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)))
            {
                str = string.Join(",", cols.Where(it => "PRI".Equals(it.KeyType, StringComparison.OrdinalIgnoreCase)).Select(it => $" {dic.TableAliasOne}.`{it.ColumnName}` desc "));
            }
            else
            {
                str = $" {dic.TableAliasOne}.`{DC.SC.GetModelProperys(key).First().Name}` desc ";
            }

            str = $" \r\n order by {str}";

            return str;
        }

        private string Limit(int? pageIndex, int? pageSize)
        {
            return $" \r\n limit {(pageIndex - 1) * pageSize},{/*pageIndex * */pageSize}";
        }

        internal string GetSingleValuePart()
        {
            var str = string.Empty;

            foreach (var item in DC.Conditions)
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

            var item = DC.Conditions.FirstOrDefault(it => it.Option == OptionEnum.Count);
            if (item != null)
            {
                str = $" {item.Option.ToEnumDesc<OptionEnum>()}(`{item.ColumnOne}`) ";
            }
            else
            {
                str = " count(*) ";
            }

            return str;
        }

        internal string Columns()
        {
            var str = string.Empty;
            var list = new List<string>();

            foreach (var dic in DC.Conditions)
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
                var key = DC.SC.GetKey(typeof(M).FullName, DC.Conn.Database);
                tableName = DC.SC.GetModelTableName(key);
            }

            return tableName;
        }

        internal string Joins()
        {
            var str = string.Empty;

            foreach (var item in DC.Conditions)
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
                        str += $" \r\n \t {item.Action.ToEnumDesc<ActionEnum>()} {item.TableOne} as {item.TableAliasOne} ";
                        break;
                    case ActionEnum.On:
                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} {item.TableAliasOne}.`{item.ColumnOne}`={item.AliasTwo}.`{item.KeyTwo}` ";
                        break;
                }
            }

            return str;
        }

        internal string Wheres()
        {
            /* 可能有问题*/
            //if (!DC.Conditions.Any(it => it.Action == ActionEnum.Where)
            //    && !DC.Conditions.Any(it => it.Action == ActionEnum.And)
            //    && !DC.Conditions.Any(it => it.Action == ActionEnum.Or))
            //{
            //    throw new Exception("没有设置任何条件!");
            //}

            var str = string.Empty;

            foreach (var item in DC.Conditions)
            {
                switch (item.Action)
                {
                    case ActionEnum.Where:
                    case ActionEnum.And:
                    case ActionEnum.Or:
                        switch (item.Option)
                        {
                            case OptionEnum.Compare:
                                switch (item.Crud)
                                {
                                    case CrudTypeEnum.Join:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} {item.TableAliasOne}.`{item.ColumnOne}`{item.Compare.ToEnumDesc<CompareEnum>()}@{item.Param} ";
                                        break;
                                    case CrudTypeEnum.Delete:
                                    case CrudTypeEnum.Update:
                                    case CrudTypeEnum.Query:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} `{item.ColumnOne}`{item.Compare.ToEnumDesc<CompareEnum>()}@{item.Param} ";
                                        break;
                                }
                                break;
                            case OptionEnum.Like:
                                switch (item.Crud)
                                {
                                    case CrudTypeEnum.Join:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} {item.TableAliasOne}.`{item.ColumnOne}`{item.Option.ToEnumDesc<OptionEnum>()}{LikeStrHandle(item)} ";
                                        break;
                                    case CrudTypeEnum.Delete:
                                    case CrudTypeEnum.Update:
                                    case CrudTypeEnum.Query:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} `{item.ColumnOne}`{item.Option.ToEnumDesc<OptionEnum>()}{LikeStrHandle(item)} ";
                                        break;
                                }
                                break;
                            case OptionEnum.CharLength:
                                switch (item.Crud)
                                {
                                    case CrudTypeEnum.Join:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} {item.Option.ToEnumDesc<OptionEnum>()}({item.TableAliasOne}.`{item.ColumnOne}`){item.Compare.ToEnumDesc<CompareEnum>()}@{item.Param} ";
                                        break;
                                    case CrudTypeEnum.Delete:
                                    case CrudTypeEnum.Update:
                                    case CrudTypeEnum.Query:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} {item.Option.ToEnumDesc<OptionEnum>()}(`{item.ColumnOne}`){item.Compare.ToEnumDesc<CompareEnum>()}@{item.Param} ";
                                        break;
                                }
                                break;
                            case OptionEnum.OneEqualOne:
                                str += $" {item.Action.ToEnumDesc<ActionEnum>()} @{item.Param} ";
                                break;
                            case OptionEnum.In:
                                switch (item.Crud)
                                {
                                    case CrudTypeEnum.Join:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} {item.TableAliasOne}.`{item.ColumnOne}` {item.Option.ToEnumDesc<OptionEnum>()}({InStrHandle(item)}) ";
                                        break;
                                    case CrudTypeEnum.Delete:
                                    case CrudTypeEnum.Update:
                                    case CrudTypeEnum.Query:
                                        str += $" {item.Action.ToEnumDesc<ActionEnum>()} `{item.ColumnOne}` {item.Option.ToEnumDesc<OptionEnum>()}({InStrHandle(item)}) ";
                                        break;
                                }
                                break;
                            case OptionEnum.IsNull:
                            case OptionEnum.IsNotNull:
                                str += $" {item.Action.ToEnumDesc<ActionEnum>()} `{item.ColumnOne}` {item.Option.ToEnumDesc<OptionEnum>()} ";
                                break;
                        }
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(str)
                && DC.Conditions.All(it => it.Action != ActionEnum.Where))
            {
                var aIdx = str.IndexOf(" and ", StringComparison.Ordinal);
                var oIdx = str.IndexOf(" or ", StringComparison.Ordinal);
                if (aIdx < oIdx)
                {
                    str = $" {ActionEnum.Where.ToEnumDesc<ActionEnum>()} true {str} ";
                }
                else
                {
                    str = $" {ActionEnum.Where.ToEnumDesc<ActionEnum>()} false {str} ";
                }
            }

            return str;
        }

        internal string GetUpdates()
        {
            if (//!DC.Conditions.Any(it => it.Action == ActionEnum.Set)
            //    && !DC.Conditions.Any(it => it.Action == ActionEnum.Change)
                !DC.Conditions.Any(it => it.Action == ActionEnum.Update))
            {
                throw new Exception("没有设置任何要更新的字段!");
            }

            var list = new List<string>();

            foreach (var item in DC.Conditions)
            {
                switch (item.Action)
                {
                    //case ActionEnum.Set:
                    //case ActionEnum.Change:
                    case ActionEnum.Update:
                        switch (item.Option)
                        {
                            case OptionEnum.ChangeAdd:
                            case OptionEnum.ChangeMinus:
                                list.Add($" `{item.ColumnOne}`=`{item.ColumnOne}`{item.Option.ToEnumDesc<OptionEnum>()}@{item.Param} ");
                                break;
                            case OptionEnum.Set:
                                list.Add($" `{item.ColumnOne}`{item.Option.ToEnumDesc<OptionEnum>()}@{item.Param} ");
                                break;
                        }
                        break;
                }
            }

            return string.Join(",", list);
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
                                            and  TABLE_NAME = '{tableName.TrimStart('`').TrimEnd('`').ToLower()}'
                                        ;
                                  ";
            return (await SqlHelper.QueryAsync<ColumnInfo>(DC.Conn, sql, new DynamicParameters())).ToList();
        }

        internal string GetColumns()
        {
            var list = new List<string>();
            foreach (var item in DC.Conditions)
            {
                if (item.TvpIndex == 0)
                {
                    list.Add($"`{item.ColumnOne}`");
                }
            }
            return $" ({ string.Join(",", list)}) ";
        }
        internal string GetValues()
        {
            var list = new List<string>();
            for (var i = 0; i < DC.Conditions.Max(it => it.TvpIndex) + 1; i++)
            {
                var values = new List<string>();
                foreach (var item in DC.Conditions)
                {
                    if (item.TvpIndex == i)
                    {
                        values.Add($"@{item.Param}");
                    }
                }
                list.Add($"({string.Join(",", values)})");
            }
            return string.Join(", \r\n ", list);
        }


        internal string GetTableName(Type mType)
        {
            var tableName = string.Empty;
            tableName = DC.AH.GetAttributePropVal<TableAttribute>(mType, a => a.Name);
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new Exception("DB Entity 缺少 TableAttribute 指定的表名!");
            }
            return $"`{tableName}`";
            //return $"`{DC.TableAttributeName(mType)}`";
        }

        internal OptionEnum GetChangeOption(ChangeEnum change)
        {
            switch (change)
            {
                case ChangeEnum.Add:
                    return OptionEnum.ChangeAdd;
                case ChangeEnum.Minus:
                    return OptionEnum.ChangeMinus;
                default:
                    return OptionEnum.ChangeAdd;
            }
        }

        internal List<string> GetSQL<M>(UiMethodEnum type, int? pageIndex = null, int? pageSize = null)
        {
            var list = new List<string>();

            //
            switch (type)
            {
                case UiMethodEnum.CreateAsync:
                    list.Add($" insert into {Table<M>(type)} {GetColumns()} values {GetValues()} ;");
                    break;
                case UiMethodEnum.CreateBatchAsync:
                    list.Add(
                                  $" LOCK TABLES {Table<M>(type)} WRITE; " +
                                  $" \r\n /*!40000 ALTER TABLE {Table<M>(type)} DISABLE KEYS */; " +
                                  $" \r\n insert into  {Table<M>(type)} {GetColumns()} \r\n VALUES {GetValues()} ; " +
                                  $" \r\n /*!40000 ALTER TABLE {Table<M>(type)} ENABLE KEYS */; " +
                                  $" \r\n UNLOCK TABLES; "
                                 );
                    break;
                case UiMethodEnum.DeleteAsync:
                    list.Add($" delete {From()} {Table<M>(type)} {Wheres()} ; ");
                    break;
                case UiMethodEnum.UpdateAsync:
                    list.Add($" update {Table<M>(type)} \r\n set {DC.SqlProvider.GetUpdates()} {Wheres()} ;");
                    break;
                case UiMethodEnum.QueryFirstOrDefaultAsync:
                    list.Add($"select {Columns()} {From()} {Table<M>(type)} {Wheres()} ; ");
                    break;
                case UiMethodEnum.JoinQueryFirstOrDefaultAsync:
                    list.Add($" select {Columns()} {From()} {Joins()} {Wheres()} ; ");
                    break;
                case UiMethodEnum.QueryListAsync:
                    list.Add($"select {Columns()} {From()} {Table<M>(type)} {Wheres()} {GetOrderByPart<M>()} ; ");
                    break;
                case UiMethodEnum.JoinQueryListAsync:
                    list.Add($" select {Columns()} {From()} {Joins()} {Wheres()} {GetOrderByPart()} ; ");
                    break;
                case UiMethodEnum.QueryPagingListAsync:
                    var wherePart8 = Wheres();
                    list.Add($"select count(*) {From()} {Table<M>(type)} {wherePart8} ; ");
                    list.Add($"select {Columns()} {From()} {Table<M>(type)} {wherePart8} {GetOrderByPart<M>()} {Limit(pageIndex,pageSize)}  ; ");
                    break;
                case UiMethodEnum.JoinQueryPagingListAsync:
                    var wherePart9 = Wheres();
                    list.Add($"select count(*) {From()} {Joins()} {wherePart9} ; ");
                    list.Add($"select {Columns()} {From()} {Joins()} {wherePart9} {GetOrderByPart()} {Limit(pageIndex,pageSize)}  ; ");
                    break;
                case UiMethodEnum.QueryAllPagingListAsync:
                    list.Add($"select count(*) {From()} {Table<M>(type)} ; ");
                    list.Add($"select * {From()} {Table<M>(type)} {GetOrderByPart<M>()} {Limit(pageIndex,pageSize)}  ; ");
                    break;
                case UiMethodEnum.QuerySingleValueAsync:
                    list.Add($" select {GetSingleValuePart()} {From()} {Table<M>(type)} {Wheres()} ; ");
                    break;
                case UiMethodEnum.ExistAsync:
                case UiMethodEnum.CountAsync:
                    list.Add($" select {GetCountPart()} {From()} {Table<M>(type)} {Wheres()} ; ");
                    break;
                case UiMethodEnum.QueryAllAsync:
                    list.Add($" select * {From()} {Table<M>(type)} {GetOrderByPart<M>()} ; ");
                    break;
            }

            //
            if (XDebug.Hint)
            {
                XDebug.SQL = list;
                var paras = DC.GetParameters();
                XDebug.Parameters = DC
                    .Conditions
                    .Where(it => DC.IsParameter(it))
                    .Select(it =>
                    {
                        return $"key:【{it.Param}】;val:【{it.CsValue}】;param【{it.DbValue}】.";
                    })
                    .ToList();
            }

            //
            return list;
        }
    }
}
