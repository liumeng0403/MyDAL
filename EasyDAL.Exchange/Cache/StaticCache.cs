using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;

namespace Yunyong.DataExchange.Cache
{
    internal class StaticCache : ClassInstance<StaticCache>
    {
        internal string GetKey(string classFullName,string dbName)
        {
            //var key = string.Empty;
            //key += dc.Conn.Database;
            //dc.SqlProvider.TryGetTableName<M>(out var tableName);
            //key += tableName;
            //return key;
            return $"{dbName}:{classFullName}";
        }

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<Type, List<PropertyInfo>> ModelPropertiesCache { get; } = new ConcurrentDictionary<Type, List<PropertyInfo>>();
        internal List<PropertyInfo> GetModelProperys(Type mType,Context dc)
        {
            var props = default(List<PropertyInfo>);
            if (!ModelPropertiesCache.TryGetValue(mType, out props))
            {
                props = dc.GH.GetPropertyInfos(mType);
                ModelPropertiesCache[mType] = props;
            }
            return props;
        }

        internal static ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>> EHCache { get; } = new ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>>();

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, Assembly> AssemblyCache { get; } = new ConcurrentDictionary<string, Assembly>();
        internal Assembly GetAssembly(string fullName,Context dc)
        {
            var ass = default(Assembly);
            if(!AssemblyCache.TryGetValue(fullName,out ass))
            {
                ass = dc.GH.LoadAssembly(fullName);
                AssemblyCache[fullName] = ass;
            }
            return ass;
        }

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, List<ColumnInfo>> TableColumnsCache { get; } = new ConcurrentDictionary<string, List<ColumnInfo>>();
        internal async Task<List<ColumnInfo>> GetColumnInfos(string key,Context dc)
        {
            //var tcKey = GetTCKey<M>(dc);
            //var key = GetTCKey(classFullName, dc.Conn.Database);
            if (!TableColumnsCache.TryGetValue(key, out var columns))
            {
                columns = await dc.SqlProvider.GetColumnsInfos(dc.SC.GetModelTable(key));
                TableColumnsCache[key] = columns;
            }

            return columns;
        }

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, Type> ModelTypeCache { get; } = new ConcurrentDictionary<string, Type>();

        internal Type GetModelType<M>(string key)
        {
            return null;
        }

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, string> ModelTableCache { get; } = new ConcurrentDictionary<string, string>();

        internal string GetModelTable(string key)
        {
            return ModelTableCache[key];
        }

        internal void SetModelTable(string key, string tableName)
        {
            ModelTableCache.GetOrAdd(key,tableName);
        }

    }
}
