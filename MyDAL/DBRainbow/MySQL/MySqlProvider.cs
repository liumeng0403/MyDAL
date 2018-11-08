using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Core.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
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

        private void GetOrderByPart<M>(StringBuilder sb)
        {
            Spacing(sb);
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
                        list.Add($" `{o.ColumnOne}` {AdoNetHelper.ConditionOption(o.Option)} ");
                    }
                    else if (o.Func == FuncEnum.CharLength)
                    {
                        list.Add($" {AdoNetHelper.ConditionFunc(o.Func)}(`{o.ColumnOne}`) {AdoNetHelper.ConditionOption(o.Option)} ");
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

            sb.Append(str);
        }
        private void GetOrderByPart(StringBuilder sb)
        {
            Spacing(sb);
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
                        list.Add($" {o.TableAliasOne}.`{o.ColumnOne}` {AdoNetHelper.ConditionOption(o.Option)} ");
                    }
                    else if (o.Func == FuncEnum.CharLength)
                    {
                        list.Add($" {AdoNetHelper.ConditionFunc(o.Func)}({o.TableAliasOne}.`{o.ColumnOne}`) {AdoNetHelper.ConditionOption(o.Option)} ");
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
                sb.Append($" \r\n limit {start},{DC.PageSize}");
            }
            else
            {
                sb.Append(string.Empty);
            }
        }

        /****************************************************************************************************************/

        private string CompareProcess(DicParam db, bool isMulti)
        {
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {db.TableAliasOne}.`{db.ColumnOne}`{AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" `{db.ColumnOne}`{AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {AdoNetHelper.ConditionAction(db.Action)} {db.TableAliasOne}.`{db.ColumnOne}`{AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {AdoNetHelper.ConditionAction(db.Action)} `{db.ColumnOne}`{AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
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
                    return $" {db.TableAliasOne}.`{db.ColumnOne}`{AdoNetHelper.ConditionOption(db.Option)}{LikeStrHandle(db)} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" `{db.ColumnOne}`{AdoNetHelper.ConditionOption(db.Option)}{LikeStrHandle(db)} ";
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {AdoNetHelper.ConditionAction(db.Action)} {db.TableAliasOne}.`{db.ColumnOne}`{AdoNetHelper.ConditionOption(db.Option)}{LikeStrHandle(db)} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {AdoNetHelper.ConditionAction(db.Action)} `{db.ColumnOne}`{AdoNetHelper.ConditionOption(db.Option)}{LikeStrHandle(db)} ";
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
                    return $" {AdoNetHelper.ConditionOption(db.Option)}({db.TableAliasOne}.`{db.ColumnOne}`){AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {AdoNetHelper.ConditionOption(db.Option)}(`{db.ColumnOne}`){AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {AdoNetHelper.ConditionAction(db.Action)} {AdoNetHelper.ConditionOption(db.Option)}({db.TableAliasOne}.`{db.ColumnOne}`){AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {AdoNetHelper.ConditionAction(db.Action)} {AdoNetHelper.ConditionOption(db.Option)}(`{db.ColumnOne}`){AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
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
                    return $" {AdoNetHelper.ConditionOption(db.Option)}({db.TableAliasOne}.`{db.ColumnOne}`){AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {AdoNetHelper.ConditionOption(db.Option)}(`{db.ColumnOne}`){AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {AdoNetHelper.ConditionAction(db.Action)} {AdoNetHelper.ConditionOption(db.Option)}({db.TableAliasOne}.`{db.ColumnOne}`){AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {AdoNetHelper.ConditionAction(db.Action)} {AdoNetHelper.ConditionOption(db.Option)}(`{db.ColumnOne}`){AdoNetHelper.ConditionCompare(db.Compare)}@{db.Param} ";
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
                return $" {AdoNetHelper.ConditionAction(db.Action)} @{db.Param} ";
            }
            throw new Exception("OneEqualOneProcess 未能处理!!!");
        }
        private string InProcess(DicParam db, bool isMulti)
        {
            if (isMulti)
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {db.TableAliasOne}.`{db.ColumnOne}` {AdoNetHelper.ConditionOption(db.Option)}({InStrHandle(db.InItems)}) ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" `{db.ColumnOne}` {AdoNetHelper.ConditionOption(db.Option)}({InStrHandle(db.InItems)}) ";
                }
            }
            else
            {
                if (db.Crud == CrudTypeEnum.Join)
                {
                    return $" {AdoNetHelper.ConditionAction(db.Action)} {db.TableAliasOne}.`{db.ColumnOne}` {AdoNetHelper.ConditionOption(db.Option)}({InStrHandle(db.InItems)}) ";
                }
                else if (DC.IsSingleTableOption(db.Crud))
                {
                    return $" {AdoNetHelper.ConditionAction(db.Action)} `{db.ColumnOne}` {AdoNetHelper.ConditionOption(db.Option)}({InStrHandle(db.InItems)}) ";
                }
            }
            throw new Exception("InProcess 未能处理!!!");
        }
        private string IsNullProcess(DicParam db, bool isMulti)
        {
            if (isMulti)
            {
                return $" `{db.ColumnOne}` {AdoNetHelper.ConditionOption(db.Option)} ";
            }
            else
            {
                return $" {AdoNetHelper.ConditionAction(db.Action)} `{db.ColumnOne}` {AdoNetHelper.ConditionOption(db.Option)} ";
            }
            throw new Exception("IsNullProcess 未能处理!!!");
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
                return string.Join(AdoNetHelper.MultiConditionAction(db.GroupAction), list);
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

        private void Spacing(StringBuilder sb)
        {
            sb.Append(" ");
        }
        private void Backquote(StringBuilder sb)
        {
            sb.Append("`");
        }
        private void Comma(StringBuilder sb)
        {
            sb.Append(",");
        }
        private void ConcatWithComma(StringBuilder sb, IEnumerable<string> ss, Action<StringBuilder> a1, Action<StringBuilder> a2)
        {
            var n = ss.Count();
            var i = 0;
            foreach (var s in ss)
            {
                i++;
                a1?.Invoke(sb);
                sb.Append(s);
                a2?.Invoke(sb);
                if (i != n)
                {
                    Comma(sb);
                }
            }
        }
        private void CRLF(StringBuilder sb)
        {
            sb.Append("\r\n");
        }
        private void AT(StringBuilder sb)
        {
            sb.Append("@");
        }

        /****************************************************************************************************************/

        private void LockTables(StringBuilder sb)
        {
            sb.Append("LOCK TABLES");
        }
        private void Write(StringBuilder sb)
        {
            Spacing(sb);
            sb.Append("WRITE");
        }
        private void UnlockTables(StringBuilder sb)
        {
            CRLF(sb);
            sb.Append("UNLOCK TABLES");
        }
        private void DisableKeysS(StringBuilder sb)
        {
            CRLF(sb);
            sb.Append("/*!40000 ALTER TABLE");
        }
        private void DisableKeysE(StringBuilder sb)
        {
            Spacing(sb);
            sb.Append("DISABLE KEYS */");
        }
        private void EnableKeysS(StringBuilder sb)
        {
            DisableKeysS(sb);
        }
        private void EnableKeysE(StringBuilder sb)
        {
            Spacing(sb);
            sb.Append("ENABLE KEYS */");
        }

        private void InsertInto(StringBuilder sb)
        {
            CRLF(sb);
            sb.Append("insert into");
        }
        private void Table<M>(StringBuilder sb)
        {
            Spacing(sb);
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

            sb.Append(tableName);
        }
        private void InsertColumns(StringBuilder sb)
        {
            Spacing(sb);
            var ps = DC.Parameters.Where(it => it.TvpIndex == 0);
            if (ps != null)
            {
                sb.Append("\r\n (");
                ConcatWithComma(sb, ps.Select(it => it.ColumnOne), Backquote, Backquote);
                sb.Append(") ");
            }
        }
        private void Values(StringBuilder sb)
        {
            CRLF(sb);
            sb.Append("values");
        }
        private void ValueParams(StringBuilder sb)
        {
            Spacing(sb);
            var n = DC.Parameters.Max(it => it.TvpIndex) + 1;
            for (var i = 0; i < n; i++)
            {
                var ps = DC.Parameters.Where(it => it.TvpIndex == i);
                if (ps != null)
                {
                    sb.Append(" \r\n (");
                    ConcatWithComma(sb, ps.Select(it => it.Param), AT, null);
                    sb.Append(")");
                }
                if (i != n - 1)
                {
                    Comma(sb);
                }
            }
        }

        private void GetCountPart(StringBuilder sb)
        {
            Spacing(sb);
            var str = string.Empty;

            var item = DC.Parameters.FirstOrDefault(it => it.Option == OptionEnum.Count);
            if (item.Crud == CrudTypeEnum.Query)
            {
                if ("*".Equals(item.ColumnOne, StringComparison.OrdinalIgnoreCase))
                {
                    str = $" {AdoNetHelper.ConditionOption(item.Option)}({item.ColumnOne}) ";
                }
                else
                {
                    str = $" {AdoNetHelper.ConditionOption(item.Option)}(`{item.ColumnOne}`) ";
                }
            }
            else if (item.Crud == CrudTypeEnum.Join)
            {
                if ("*".Equals(item.ColumnOne, StringComparison.OrdinalIgnoreCase))
                {
                    str = $" {AdoNetHelper.ConditionOption(item.Option)}({item.ColumnOne}) ";
                }
                else
                {
                    str = $" {AdoNetHelper.ConditionOption(item.Option)}({item.TableAliasOne}.`{item.ColumnOne}`) ";
                }
            }

            sb.Append(str);
        }

        private void Columns(StringBuilder sb)
        {
            Spacing(sb);
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
            sb.Append(str);
        }

        private void From(StringBuilder sb)
        {
            Spacing(sb);
            sb.Append("\r\n from");
        }

        private void Joins(StringBuilder sb)
        {
            Spacing(sb);
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
                        str += $" \r\n \t {AdoNetHelper.ConditionAction(item.Action)} {item.TableOne} as {item.TableAliasOne} ";
                        break;
                    case ActionEnum.On:
                        str += $" \r\n \t \t {AdoNetHelper.ConditionAction(item.Action)} {item.TableAliasOne}.`{item.ColumnOne}`={item.TableAliasTwo}.`{item.ColumnTwo}` ";
                        break;
                }
            }

            sb.Append(str);
        }

        private void Wheres(StringBuilder sb)
        {
            Spacing(sb);
            var str = string.Empty;

            //
            foreach (var db in DC.Parameters)
            {
                if (DC.IsFilterCondition(db.Action))
                {
                    str += db.Group == null ? MultiCondition(db, false) : $" {AdoNetHelper.ConditionAction(db.Action)} ({MultiCondition(db, true)}) ";
                }
            }

            //
            if (!str.IsNullStr()
                && DC.Parameters.All(it => it.Action != ActionEnum.Where))
            {
                var aIdx = str.IndexOf(" and ", StringComparison.OrdinalIgnoreCase);
                var oIdx = str.IndexOf(" or ", StringComparison.OrdinalIgnoreCase);
                if (aIdx < oIdx
                    || oIdx == -1)
                {
                    str = $" {AdoNetHelper.ConditionAction(ActionEnum.Where)} true {str} ";
                }
                else
                {
                    str = $" {AdoNetHelper.ConditionAction(ActionEnum.Where)} false {str} ";
                }
            }

            //
            sb.Append(str);
        }

        private void GetUpdates(StringBuilder sb)
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
                                list.Add($" `{item.ColumnOne}`=`{item.ColumnOne}`{AdoNetHelper.ConditionOption(item.Option)}@{item.Param} ");
                                break;
                            case OptionEnum.Set:
                                list.Add($" `{item.ColumnOne}`{AdoNetHelper.ConditionOption(item.Option)}@{item.Param} ");
                                break;
                        }
                        break;
                }
            }

            sb.Append(string.Join(", \r\n\t", list));
        }
        private void End(StringBuilder sb)
        {
            sb.Append(";");
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
                                        WHERE  ( table_schema='{DC.Conn.Database.ToLower()}' or table_schema='{DC.Conn.Database}' )
                                            and  ( TABLE_NAME = '{tableName.TrimStart('`').TrimEnd('`').ToLower()}' or TABLE_NAME = '{tableName.TrimStart('`').TrimEnd('`')}' )
                                        ;
                                  "
            };
            return await DC.DS.ExecuteReaderMultiRowAsync<ColumnInfo>();
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
            var sb = new StringBuilder();
            switch (DC.Method)
            {
                case UiMethodEnum.CreateAsync:
                    InsertInto(sb); Table<M>(sb); InsertColumns(sb); Values(sb); ValueParams(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.CreateBatchAsync:
                    LockTables(sb); Table<M>(sb); Write(sb); End(sb);
                    DisableKeysS(sb); Table<M>(sb); DisableKeysE(sb); End(sb);
                    InsertInto(sb); Table<M>(sb); InsertColumns(sb); Values(sb); ValueParams(sb); End(sb);
                    EnableKeysS(sb); Table<M>(sb); EnableKeysE(sb); End(sb);
                    UnlockTables(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.DeleteAsync:
                    sb.Append(" delete "); From(sb); Table<M>(sb); Wheres(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.UpdateAsync:
                    sb.Append(" update "); Table<M>(sb); sb.Append(" \r\n set "); GetUpdates(sb); Wheres(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.QueryFirstOrDefaultAsync:
                    sb.Append("select "); Columns(sb); From(sb); Table<M>(sb); Wheres(sb); GetOrderByPart<M>(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.JoinQueryFirstOrDefaultAsync:
                    sb.Append(" select "); Columns(sb); From(sb); Joins(sb); Wheres(sb); GetOrderByPart(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.QueryListAsync:
                case UiMethodEnum.TopAsync:
                    sb.Append("select "); Columns(sb); From(sb); Table<M>(sb); Wheres(sb); GetOrderByPart<M>(sb); Limit(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.JoinQueryListAsync:
                case UiMethodEnum.JoinTopAsync:
                    sb.Append(" select "); Columns(sb); From(sb); Joins(sb); Wheres(sb); GetOrderByPart(sb); Limit(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.QueryPagingListAsync:
                    sb.Append("select count(*) "); From(sb); Table<M>(sb); Wheres(sb); End(sb);
                    list.Add(sb.ToString());
                    sb.Clear();
                    sb.Append("select "); Columns(sb); From(sb); Table<M>(sb); Wheres(sb); GetOrderByPart<M>(sb); Limit(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.JoinQueryPagingListAsync:
                    sb.Append("select count(*) "); From(sb); Joins(sb); Wheres(sb); End(sb);
                    list.Add(sb.ToString());
                    sb.Clear();
                    sb.Append("select "); Columns(sb); From(sb); Joins(sb); Wheres(sb); GetOrderByPart(sb); Limit(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.QueryAllAsync:
                    sb.Append(" select "); Columns(sb); From(sb); Table<M>(sb); GetOrderByPart<M>(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.QueryAllPagingListAsync:
                    sb.Append("select count(*) "); From(sb); Table<M>(sb); End(sb);
                    list.Add(sb.ToString());
                    sb.Clear();
                    sb.Append("select * "); From(sb); Table<M>(sb); GetOrderByPart<M>(sb); Limit(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.ExistAsync:
                case UiMethodEnum.CountAsync:
                    sb.Append(" select "); GetCountPart(sb); From(sb); Table<M>(sb); Wheres(sb); End(sb);
                    list.Add(sb.ToString());
                    break;
                case UiMethodEnum.JoinCountAsync:
                    sb.Append(" select "); GetCountPart(sb); From(sb); Joins(sb); Wheres(sb); End(sb);
                    list.Add(sb.ToString());
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
