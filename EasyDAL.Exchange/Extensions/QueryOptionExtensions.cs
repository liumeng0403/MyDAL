using MyDAL.Common;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;

namespace MyDAL.Extensions
{
    /// <summary>
    ///     查询扩展
    /// </summary>
    public static class QueryOptionExtensions
    {
        /// <summary>
        ///     组装查询条件
        /// </summary>
        public static object GetCondition(this IQueryOption target)
        {
            if (target == null)
            {
                return new ExpandoObject();
            }

            IDictionary<string, object> dic = new ExpandoObject();

            var props = target.GetType()
                .GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
            foreach (var prop in props)
            {
                var val = prop.GetValue(target);
                if (val != null)
                {
                    dic[prop.Name] = val;
                }
            }

            return dic;
        }
    }
}
