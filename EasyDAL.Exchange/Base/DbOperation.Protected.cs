using EasyDAL.Exchange.Attributes;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace EasyDAL.Exchange.Base
{
    public abstract partial class DbOperation
    {
        protected IDbConnection Conn { get; private set; }

        protected AttributeHelper AH { get; private set; }

        protected GenericHelper GH { get; private set; }

        protected ExpressionHelper EH { get; private set; }
        
        protected List<string> Conditions { get;private set; }

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

        protected bool TryGetTableName<M>(M m, out string tableName)
        {

            tableName = AH.GetPropertyValue<M, TableAttribute>(m, a => a.Name);
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new Exception("DB Entity 缺少 TableAttribute 指定的表名!");
            }

            return true;

        }

    }
}
