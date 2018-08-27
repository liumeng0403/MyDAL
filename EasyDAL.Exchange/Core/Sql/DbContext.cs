
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace EasyDAL.Exchange.Core.Sql
{
    internal class DbContext
    {

        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> _modelPropertiesCache = new ConcurrentDictionary<Type, List<PropertyInfo>>();
        internal AttributeHelper AH { get; private set; }

        internal GenericHelper GH { get; private set; }

        internal ExpressionHelper EH { get; private set; }
        public ConcurrentDictionary<Type, List<PropertyInfo>> ModelPropertiesCache
        {
            get
            {
                return _modelPropertiesCache;
            }
        }

        internal List<DicModel<string, string>> Conditions { get; private set; }

        internal IDbConnection Conn { get; private set; }

        internal DbOperation OP { get; set; }

        internal DbContext(IDbConnection conn)
        {
            Conn = conn;
            Conditions = new List<DicModel<string, string>>();
            AH = AttributeHelper.Instance;
            GH = GenericHelper.Instance;
            EH = ExpressionHelper.Instance;
            OP = new DbOperation(this);
        }

    }
}
