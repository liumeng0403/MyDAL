
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using EasyDAL.Exchange.Extensions;
using EasyDAL.Exchange.DynamicParameter;
using EasyDAL.Exchange.Core.Sql;
using System.Data;
using EasyDAL.Exchange.Common;

namespace EasyDAL.Exchange.Core
{
    internal class MySqlProvider
    {
        private MySqlProvider() { }

        internal MySqlProvider(DbContext dc)
        {
            DC = dc;
            DC.SqlProvider = this;
        }

        internal DbContext DC { get; private set; }

        internal void GetProperties<M>(M m)
        {
            var props = default(List<PropertyInfo>);
            if (!DC.ModelPropertiesCache.TryGetValue(m.GetType(), out props))
            {
                props = DC.GH.GetPropertyInfos(m);
                DC.ModelPropertiesCache[m.GetType()] = props;
            }

            foreach (var prop in props)
            {
                DC.Conditions.Add(new DicModel<string, string>
                {
                    key = prop.Name,
                    Value = DC.GH.GetTypeValue(prop.PropertyType, prop, m),
                    Action = ActionEnum.Insert,
                    Option = OptionEnum.Insert
                });
            }
        }

        internal string GetWheres()
        {
            if (!DC.Conditions.Any(it => it.Action == ActionEnum.Where)
                && !DC.Conditions.Any(it => it.Action == ActionEnum.And)
                && !DC.Conditions.Any(it => it.Action == ActionEnum.Or))
            {
                throw new Exception("没有设置任何条件!");
            }

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
                            case OptionEnum.Equal:
                            case OptionEnum.LessThan:
                            case OptionEnum.LessThanOrEqual:
                            case OptionEnum.GreaterThan:
                            case OptionEnum.GreaterThanOrEqual:
                                str += $" {item.Action.ToEnumDesc<ActionEnum>()} `{item.key}`{item.Option.ToEnumDesc<OptionEnum>()}@{item.key} ";
                                break;
                            case OptionEnum.Like:
                                str += $" {item.Action.ToEnumDesc<ActionEnum>()} `{item.key}`{item.Option.ToEnumDesc<OptionEnum>()}CONCAT('%',@{item.key},'%') ";
                                break;
                        }
                        break;
                }
            }

            return str;
        }

        internal string GetUpdates()
        {
            if (!DC.Conditions.Any(it => it.Action == ActionEnum.Set)
                && !DC.Conditions.Any(it => it.Action == ActionEnum.Change))
            {
                throw new Exception("没有设置任何要更新的字段!");
            }

            var list = new List<string>();

            foreach (var item in DC.Conditions)
            {
                switch (item.Action)
                {
                    case ActionEnum.Set:
                    case ActionEnum.Change:
                        switch (item.Option)
                        {
                            case OptionEnum.ChangeAdd:
                            case OptionEnum.ChangeMinus:
                                list.Add($" `{item.key}`=`{item.key}`{item.Option.ToEnumDesc<OptionEnum>()}@{item.key} ");
                                break;
                            case OptionEnum.Set:
                                list.Add($" `{item.key}`{item.Option.ToEnumDesc<OptionEnum>()}@{item.key} ");
                                break;
                        }
                        break;
                }
            }

            return string.Join(",", list);
        }

        internal string GetColumns()
        {
            return $" ({ string.Join(",", DC.Conditions.Select(it => $"`{it.key}`"))}) ";
        }
        internal string GetValues()
        {
            return $" ({ string.Join(",", DC.Conditions.Select(it => $"@{it.key}"))}) ";
        }

        internal bool TryGetTableName<M>(M m, out string tableName)
        {

            tableName = DC.AH.GetPropertyValue<M, TableAttribute>(m, a => a.Name);
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new Exception("DB Entity 缺少 TableAttribute 指定的表名!");
            }

            return true;

        }
        internal bool TryGetTableName<M>(out string tableName)
        {
            tableName = DC.AH.GetPropertyValue<M, TableAttribute>(Activator.CreateInstance<M>(), a => a.Name);
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new Exception("DB Entity 缺少 TableAttribute 指定的表名!");
            }

            return true;

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

        internal DynamicParameters GetParameters()
        {
            var paras = new DynamicParameters();
            foreach (var item in DC.Conditions)
            {
                switch (item.Action)
                {
                    case ActionEnum.Insert:
                    case ActionEnum.Set:
                    case ActionEnum.Change:
                    case ActionEnum.Where:
                    case ActionEnum.And:
                    case ActionEnum.Or:
                        paras.Add(item.key, item.Value);
                        break;
                }
            }
            return paras;
        }

        internal List<string> GetSQL<M>(SqlTypeEnum type, M m = default(M), PagingList<M> pager=default(PagingList<M>))
        {
            var list = new List<string>();

            //
            var tableName = string.Empty;
            if (!default(M).Equals(m))
            {
                DC.SqlProvider.TryGetTableName(m, out tableName);
            }
            else
            {
                DC.SqlProvider.TryGetTableName<M>(out tableName);
            }

            //
            switch (type)
            {
                case SqlTypeEnum.CreateAsync:
                    DC.SqlProvider.GetProperties(m);
                    list.Add($" insert into `{tableName}` {DC.SqlProvider.GetColumns()} values {DC.SqlProvider.GetValues()} ;");
                    break;
                case SqlTypeEnum.DeleteAsync:
                    list.Add($" delete from `{tableName}` where {DC.SqlProvider.GetWheres()} ; ");
                    break;
                case SqlTypeEnum.UpdateAsync:
                    list.Add($" update `{tableName}` set {DC.SqlProvider.GetUpdates()} where {DC.SqlProvider.GetWheres()} ;");
                    break;
                case SqlTypeEnum.QueryFirstOrDefaultAsync:
                    list.Add($"SELECT * FROM `{tableName}` WHERE {DC.SqlProvider.GetWheres()} ; ");
                    break;
                case SqlTypeEnum.QueryListAsync:
                    list.Add($"SELECT * FROM `{tableName}` WHERE {DC.SqlProvider.GetWheres()} ; ");
                    break;
                case SqlTypeEnum.QueryPagingListAsync:
                    var wherePart = DC.SqlProvider.GetWheres();
                    list.Add( $"SELECT count(*) FROM `{tableName}` WHERE {wherePart} ; ");
                    list.Add($"SELECT * FROM `{tableName}` WHERE {wherePart} limit {(pager.PageIndex - 1) * pager.PageSize},{pager.PageIndex * pager.PageSize}  ; ");
                    break;
            }

            //
            if (Hints.Hint)
            {
                Hints.SQL = list;
                Hints.Parameters = DC.Conditions.Select(it => $"key:{it.key};val:{it.Value}.").ToList();
            }

            //
            return list;
        }
    }
}
