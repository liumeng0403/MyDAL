using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Helper;
using MyDAL.Core.MySql.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace MyDAL.Cache
{
    internal class StaticCache : ClassInstance<StaticCache>
    {
        internal string GetKey(string classFullName, string dbName)
        {
            return $"{dbName}:{classFullName}";
        }
        internal string GetKey(string propName,string attrFullName,string classFullName, string dbName)
        {
            return $"{GetKey(classFullName, dbName)}:{attrFullName}:{propName}";
        }
        internal string GetAttrKey(string attrFullName,string propName,string classFullName,string dbName)
        {
            return $"{GetKey(classFullName, dbName)}:{propName}:{attrFullName}";
        }

        /*****************************************************************************************************************************************************/
        
        private static ConcurrentDictionary<string, XColumnAttribute> XColumnAttributeCache { get; } = new ConcurrentDictionary<string, XColumnAttribute>();
        internal XColumnAttribute GetXColumnAttribute(PropertyInfo info,string key)
        {
            var attr = default(XColumnAttribute);
            if (!XColumnAttributeCache.TryGetValue(key, out attr))
            {
                if (info.IsDefined(XConfig.XColumnAttribute, false))
                {
                    attr = (XColumnAttribute)info.GetCustomAttributes(XConfig.XColumnAttribute, false)[0];
                    XColumnAttributeCache[key] = attr;
                    return attr;
                }
                return null;
            }
            return attr;
        }

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, Assembly> AssemblyCache { get; } = new ConcurrentDictionary<string, Assembly>();
        internal Assembly GetAssembly(string key)
        {
            var ass = default(Assembly);
            if (!AssemblyCache.TryGetValue(key, out ass))
            {
                ass = GenericHelper.Instance.LoadAssembly(key.Split(':')[1]);
                AssemblyCache[key] = ass;
            }
            return ass;
        }

        /*****************************************************************************************************************************************************/

        /// <summary>
        /// Cache Data
        /// </summary>
        internal static ConcurrentDictionary<string, string> ModelAttributePropValCache { get; } = new ConcurrentDictionary<string, string>();

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, string> ModelTableNameCache { get; } = new ConcurrentDictionary<string, string>();

        internal string GetModelTableName(string key)
        {
            return ModelTableNameCache[key];
        }

        internal void SetModelTableName(string key, string tableName)
        {
            ModelTableNameCache.GetOrAdd(key, tableName);
        }

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, Type> ModelTypeCache { get; } = new ConcurrentDictionary<string, Type>();

        internal Type GetModelType<M>(string key)
        {
            return ModelTypeCache[key];
        }

        internal void SetModelType(string key, Type type)
        {
            if (!ModelTypeCache.ContainsKey(key))
            {
                ModelTypeCache[key] = type;
            }
        }

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, List<PropertyInfo>> ModelPropertiesCache { get; } = new ConcurrentDictionary<string, List<PropertyInfo>>();
        internal List<PropertyInfo> GetModelProperys(string key)
        {
            return ModelPropertiesCache[key];
        }
        internal void SetModelProperys(Type mType, Context dc)
        {
            var key = GetKey(mType.FullName, dc.Conn.Database);
            if (!ModelPropertiesCache.ContainsKey(key))
            {
                var props = dc.GH.GetPropertyInfos(mType);
                ModelPropertiesCache[key] = props;
            }
        }

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, List<ColumnInfo>> ModelColumnInfosCache { get; } = new ConcurrentDictionary<string, List<ColumnInfo>>();
        internal List<ColumnInfo> GetColumnInfos(string key)
        {
            return ModelColumnInfosCache[key];
        }
        internal async Task SetModelColumnInfos(string key, Context dc)
        {
            if (!ModelColumnInfosCache.ContainsKey(key))
            {
                var columns = await dc.SqlProvider.GetColumnsInfos(dc.SC.GetModelTableName(key));
                ModelColumnInfosCache[key] = columns;
            }
        }

    }
}
