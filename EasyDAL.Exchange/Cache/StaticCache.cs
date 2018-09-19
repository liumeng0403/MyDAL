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
        internal string GetKey(string classFullName, string dbName)
        {
            return $"{dbName}:{classFullName}";
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

        internal static ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>> EHCache { get; } = new ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>>();

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, Assembly> AssemblyCache { get; } = new ConcurrentDictionary<string, Assembly>();
        internal Assembly GetAssembly(string fullName, Context dc)
        {
            var ass = default(Assembly);
            if (!AssemblyCache.TryGetValue(fullName, out ass))
            {
                ass = dc.GH.LoadAssembly(fullName);
                AssemblyCache[fullName] = ass;
            }
            return ass;
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
                var columns = await dc.SqlProvider.GetColumnsInfos(dc.SC.GetModelTable(key));
                ModelColumnInfosCache[key] = columns;
            }
        }

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, Type> ModelTypeCache { get; } = new ConcurrentDictionary<string, Type>();

        internal Type GetModelType<M>(string key)
        {
            return ModelTypeCache[key];
        }

        internal void SetModelType(string key,Type type)
        {
            if (!ModelTypeCache.ContainsKey(key))
            {
                ModelTypeCache[key] = type;
            }
        }

        /*****************************************************************************************************************************************************/

        private static ConcurrentDictionary<string, string> ModelTableCache { get; } = new ConcurrentDictionary<string, string>();

        internal string GetModelTable(string key)
        {
            return ModelTableCache[key];
        }

        internal void SetModelTable(string key, string tableName)
        {
            ModelTableCache.GetOrAdd(key, tableName);
        }

        /*****************************************************************************************************************************************************/

        /// <summary>
        /// Cache Data
        /// </summary>
        internal static ConcurrentDictionary<string, string> Cache { get; } = new ConcurrentDictionary<string, string>();

    }
}
