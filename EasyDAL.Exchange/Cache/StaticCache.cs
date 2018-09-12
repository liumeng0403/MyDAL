using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;

namespace Yunyong.DataExchange.Cache
{
    internal class StaticCache : ClassInstance<StaticCache>
    {
        private static GenericHelper GH { get; } = GenericHelper.Instance; 

        private static ConcurrentDictionary<Type, List<PropertyInfo>> ModelPropertiesCache { get; } = new ConcurrentDictionary<Type, List<PropertyInfo>>();
        internal List<PropertyInfo> GetModelProperys(Type mType)
        {
            var props = default(List<PropertyInfo>);
            if (!ModelPropertiesCache.TryGetValue(mType, out props))
            {
                props = GH.GetPropertyInfos(mType);
                ModelPropertiesCache[mType] = props;
            }
            return props;
        }

        internal static ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>> EHCache { get; } = new ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>>();

        private static ConcurrentDictionary<string, Assembly> AssemblyCache { get; } = new ConcurrentDictionary<string, Assembly>();
        internal Assembly GetAssembly(string fullName)
        {
            var ass = default(Assembly);
            if(!AssemblyCache.TryGetValue(fullName,out ass))
            {
                ass = GH.LoadAssembly(fullName);
                AssemblyCache[fullName] = ass;
            }
            return ass;
        }


        private static ConcurrentDictionary<string, List<ColumnInfo>> TableColumnsCache { get; } = new ConcurrentDictionary<string, List<ColumnInfo>>();
        private string GetTCKey<M>(DbContext dc)
        {
            var key = string.Empty;
            key += dc.Conn.Database;
            dc.SqlProvider.TryGetTableName<M>(out var tableName);
            key += tableName;
            return key;
        }
        internal async Task<List<ColumnInfo>> GetColumnInfos<M>(DbContext dc)
        {
            var tcKey = GetTCKey<M>(dc);
            if (!TableColumnsCache.TryGetValue(tcKey, out var columns))
            {
                columns = await dc.SqlProvider.GetColumnsInfos<M>();
                TableColumnsCache[tcKey] = columns;
            }

            return columns;
        }

    }
}
