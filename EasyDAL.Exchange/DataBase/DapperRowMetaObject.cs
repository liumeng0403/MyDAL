using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using static EasyDAL.Exchange.AdoNet.SqlMapper;

namespace EasyDAL.Exchange.DataBase
{
    internal sealed class DapperRowMetaObject : System.Dynamic.DynamicMetaObject
    {
        private static readonly MethodInfo getValueMethod = typeof(IDictionary<string, object>).GetProperty("Item").GetGetMethod();
        private static readonly MethodInfo setValueMethod = typeof(DapperRow).GetMethod("SetValue", new Type[] { typeof(string), typeof(object) });

        public DapperRowMetaObject(
            System.Linq.Expressions.Expression expression,
            System.Dynamic.BindingRestrictions restrictions
            )
            : base(expression, restrictions)
        {
        }

        public DapperRowMetaObject(
            System.Linq.Expressions.Expression expression,
            System.Dynamic.BindingRestrictions restrictions,
            object value
            )
            : base(expression, restrictions, value)
        {
        }

        

    }
}
