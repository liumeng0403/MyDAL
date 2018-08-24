using EasyDAL.Exchange.Attributes;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using EasyDAL.Exchange.Extensions;
using EasyDAL.Exchange.DynamicParameter;

namespace EasyDAL.Exchange.Base
{
    public abstract partial class DbOperation
    {
        protected IDbConnection Conn { get; private set; }

        protected AttributeHelper AH { get; private set; }

        protected GenericHelper GH { get; private set; }

        protected ExpressionHelper EH { get; private set; }

        protected List<DicModel<string, string>> Conditions { get; private set; }

        protected static readonly ConcurrentDictionary<Type, List<PropertyInfo>> ModelPropertiesCache = new ConcurrentDictionary<Type, List<PropertyInfo>>();

        protected List<string> GetProperties<M>(M m)
        {
            if (m == null)
            {
                return new List<string>();
            }

            if (m is ExpandoObject)
            {
                return ((IDictionary<string, object>)m).Keys.ToList();
            }


            var props = default(List<PropertyInfo>);
            if (!ModelPropertiesCache.TryGetValue(m.GetType(), out props))
            {
                props = GH.GetPropertyInfos(m);
                ModelPropertiesCache[m.GetType()] = props;
            }

            return props.Select(x => x.Name).ToList();
        }

        protected string GetWheres()
        {
            var str = string.Empty;

            foreach (var item in Conditions)
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
                            default:
                                throw new Exception("请联系 https://www.cnblogs.com/Meng-NET/ 博主!");
                        }
                        break;
                    default:
                        break;
                }
            }

            return str;
        }

        protected bool TryGetTableName<M>(M m, out string tableName)
        {

            tableName = AH.GetPropertyValue<M, TableAttribute>(m, a => a.Name);
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new Exception("DB Entity 缺少 TableAttribute 指定的表名!");
            }

            return true;

        }
        protected bool TryGetTableName<M>(out string tableName)
        {
            tableName = AH.GetPropertyValue<M, TableAttribute>(Activator.CreateInstance<M>(), a => a.Name);
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new Exception("DB Entity 缺少 TableAttribute 指定的表名!");
            }

            return true;

        }

        protected DynamicParameters GetParameters()
        {
            var paras = new DynamicParameters();
            foreach (var item in Conditions)
            {
                switch (item.Action)
                {
                    case ActionEnum.Set:
                    case ActionEnum.Where:
                    case ActionEnum.And:
                    case ActionEnum.Or:
                        paras.Add(item.key, item.Value);
                        break;
                }
            }
            return paras;
        }
    }
}
