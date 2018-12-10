using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Threading.Tasks;
using Yunyong.DataExchange.AdoNet;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Helper;
using Yunyong.DataExchange.DataRainbow;

namespace Yunyong.DataExchange.Core
{
    internal class XCache
    {
        private int GetColumnHash(IDataReader reader)
        {
            unchecked
            {
                int max = reader.FieldCount;
                int hash = max;
                for (int i = 0; i < max; i++)
                {
                    var col = reader.GetName(i);
                    hash = (-79 * ((hash * 31) + (col?.GetHashCode() ?? 0))) + (reader.GetFieldType(i)?.GetHashCode() ?? 0);
                }
                return hash;
            }
        }
        private static ConcurrentDictionary<string, XColumnAttribute> XColumnAttributeCache { get; } = new ConcurrentDictionary<string, XColumnAttribute>();
        private static ConcurrentDictionary<string, Assembly> AssemblyCache { get; } = new ConcurrentDictionary<string, Assembly>();
        private static ConcurrentDictionary<string, string> ModelTableNameCache { get; } = new ConcurrentDictionary<string, string>();
        private static ConcurrentDictionary<string, Type> ModelTypeCache { get; } = new ConcurrentDictionary<string, Type>();
        private static ConcurrentDictionary<string, List<PropertyInfo>> ModelPropertiesCache { get; } = new ConcurrentDictionary<string, List<PropertyInfo>>();
        private static ConcurrentDictionary<string, List<ColumnInfo>> ModelColumnInfosCache { get; } = new ConcurrentDictionary<string, List<ColumnInfo>>();
        private static ConcurrentDictionary<string, IRow> ModelRowCache { get; } = new ConcurrentDictionary<string, IRow>();

        /*****************************************************************************************************************************************************/

        private Context DC { get; set; }
        internal XCache(Context dc)
        {
            DC = dc;
        }

        /*****************************************************************************************************************************************************/

        internal string GetAssemblyKey(string mFullNameOrNamespace)
        {
            return $"{mFullNameOrNamespace}:{DC.Conn.Database}";
        }
        internal string GetModelKey(string mFullName)
        {
            return $"{mFullName}:{DC.Conn.Database}";
        }
        internal string GetAttrPropKey(string propName, string attrFullName, string mFullName)
        {
            return $"{propName}:{attrFullName}:{GetModelKey(mFullName)}";
        }
        internal string GetAttrKey(string attrFullName, string propName, string mFullName)
        {
            return $"{attrFullName}:{propName}:{GetModelKey(mFullName)}";
        }
        internal string GetHandleKey(int sqlHash, int colHash, string mFullName)
        {
            return $"{sqlHash}:{colHash}:{GetModelKey(mFullName)}";
        }

        /*****************************************************************************************************************************************************/

        internal bool ExistTableName(string key)
        {
            return ModelTableNameCache.ContainsKey(key);
        }
        internal string GetTableName(string key)
        {
            return DC.AR.Invoke(key, p => ModelTableNameCache[p]);
        }
        internal void SetTableName(string key, string tableName)
        {
            ModelTableNameCache.GetOrAdd(key, tableName);
        }

        internal XColumnAttribute GetXColumnAttribute(PropertyInfo info, string key)
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
        internal Assembly GetAssembly(string key)
        {
            if (!AssemblyCache.TryGetValue(key, out var ass))
            {
                ass = new GenericHelper(DC).LoadAssembly(key.Split(':')[1]);
                AssemblyCache[key] = ass;
            }
            return ass;
        }
        internal static ConcurrentDictionary<string, string> ModelAttributePropValCache { get; } = new ConcurrentDictionary<string, string>();
        internal Type GetModelType(string key)
        {
            return DC.AR.Invoke(key, p => ModelTypeCache[p]);
        }
        internal void SetModelType(string key, Type type)
        {
            if (!ModelTypeCache.ContainsKey(key))
            {
                ModelTypeCache[key] = type;
            }
        }
        internal List<PropertyInfo> GetModelProperys(string key)
        {
            return DC.AR.Invoke(key, p => ModelPropertiesCache[p]);
        }
        internal void SetModelProperys(Type mType, Context dc)
        {
            var key = GetModelKey(mType.FullName);
            if (!ModelPropertiesCache.ContainsKey(key))
            {
                var props = dc.GH.GetPropertyInfos(mType);
                ModelPropertiesCache[key] = props;
            }
        }
        internal List<ColumnInfo> GetColumnInfos(string key)
        {
            return DC.AR.Invoke(key, p => ModelColumnInfosCache[p]);
        }
        internal async Task SetModelColumnInfos(string key, Context dc)
        {
            if (!ModelColumnInfosCache.ContainsKey(key))
            {
                var columns = await dc.SqlProvider.GetColumnsInfos(dc.SC.GetTableName(key));
                ModelColumnInfosCache[key] = columns;
            }
        }
        internal static ConcurrentDictionary<Type, RowMap> TypeMaps { get; } = new ConcurrentDictionary<Type, RowMap>();
        internal Func<IDataReader, M> GetHandle<M>(string sql, IDataReader reader)
        {
            var key = GetHandleKey(sql.GetHashCode(), GetColumnHash(reader), typeof(M).FullName);
            if (!ModelRowCache.TryGetValue(key, out var row))
            {
                ModelRowCache[key] = row = IL<M>.Row(reader);
            }
            return ((Row<M>)row).Handle;
        }
        internal Func<IDataReader, object> GetHandle(string sql, IDataReader reader, Type mType)
        {
            var key = GetHandleKey(sql.GetHashCode(), GetColumnHash(reader), mType.FullName);
            if (!ModelRowCache.TryGetValue(key, out var row))
            {
                ModelRowCache[key] = row = IL.Row(reader, mType);
            }
            return ((Row)row).Handle;
        }

    }
}
