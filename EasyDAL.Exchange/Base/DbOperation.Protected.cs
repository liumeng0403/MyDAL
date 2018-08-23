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

namespace EasyDAL.Exchange.Base
{
    public abstract partial class DbOperation
    {
        protected IDbConnection Conn { get; private set; }

        protected AttributeHelper AH { get; private set; }

        protected GenericHelper GH { get; private set; }

        protected ExpressionHelper EH { get; private set; }

        protected List<DicModel<string, string, OptionEnum>> Conditions { get; private set; }

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

        protected List<string> GetWheres()
        {
            var str = new List<string>();

            foreach (var item in Conditions)
            {
                switch (item.Other)
                {
                    case OptionEnum.Equal:
                        str.Add($" `{item.key}`{item.Other.ToEnumDesc<OptionEnum>()}@{item.key} ");
                        break;
                    case OptionEnum.Like:
                        str.Add($" `{item.key}`{item.Other.ToEnumDesc<OptionEnum>()}'%{item.Value}%' ");
                        break;
                    default:
                        throw new Exception("请联系 https://www.cnblogs.com/Meng-NET/ 博主!");
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
    }
}
