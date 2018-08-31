
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace EasyDAL.Exchange.Core.Sql
{
    internal class DbContext
    {

        private static ConcurrentDictionary<Type, List<PropertyInfo>> ModelPropertiesCache { get; } = new ConcurrentDictionary<Type, List<PropertyInfo>>();
        internal AttributeHelper AH { get; private set; }

        internal GenericHelper GH { get; private set; }
        internal Hints Hint { get; set; }
        internal ExpressionHelper EH { get; private set; }

        internal List<DicModel> Conditions { get; private set; }

        internal IDbConnection Conn { get; private set; }

        internal MySqlProvider SqlProvider { get; set; }

        internal void AddConditions(DicModel dic)
        {
            if (!string.IsNullOrWhiteSpace(dic.Param)
                && Conditions.Any(it => it.Param.Equals(dic.Param, StringComparison.OrdinalIgnoreCase)))
            {
                dic.Param += "R";
                AddConditions(dic);
            }
            else
            {
                Conditions.Add(dic);
            }
        }

        internal void GetProperties<M>(M m)
        {
            var props = default(List<PropertyInfo>);
            if (!ModelPropertiesCache.TryGetValue(m.GetType(), out props))
            {
                props = GH.GetPropertyInfos(m);
                ModelPropertiesCache[m.GetType()] = props;
            }

            foreach (var prop in props)
            {
                AddConditions(new DicModel
                {
                    KeyOne = prop.Name,
                    Param=prop.Name,
                    Value = GH.GetTypeValue(prop.PropertyType, prop, m),
                    Action = ActionEnum.Insert,
                    Option = OptionEnum.Insert
                });
            }
        }


        internal DbContext(IDbConnection conn)
        {
            Conn = conn;
            Conditions = new List<DicModel>();
            AH = AttributeHelper.Instance;
            GH = GenericHelper.Instance;
            EH = ExpressionHelper.Instance;
            SqlProvider = new MySqlProvider(this);
        }

    }
}
