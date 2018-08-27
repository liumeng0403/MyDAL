using EasyDAL.Exchange.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace EasyDAL.Exchange.Core.Sql
{
    public class DbContext
    {

        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> _modelPropertiesCache = new ConcurrentDictionary<Type, List<PropertyInfo>>();
        public ConcurrentDictionary<Type, List<PropertyInfo>> ModelPropertiesCache
        {
            get
            {
                return _modelPropertiesCache;
            }
        }

        public List<DicModel<string, string>> Conditions { get; private set; }

        public IDbConnection Conn { get; private set; }

        public DbContext(IDbConnection conn)
        {
            Conn = conn;
            Conditions = new List<DicModel<string, string>>();
        }

    }
}
