using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasyDAL.Exchange.Cache
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
    }
}
