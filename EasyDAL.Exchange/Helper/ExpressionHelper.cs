using EasyDAL.Exchange.Attributes;
using EasyDAL.Exchange.Common;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.Helper
{
    public class ExpressionHelper: ClassInstance<ExpressionHelper>
    {
        private static ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>> Cache =new ConcurrentDictionary<string, ConcurrentDictionary<Int32, String>>();

        /// <summary>
        /// Get the field name in table according to the name property in ColumnAttibute
        /// </summary>
        /// <typeparam name="T">Field</typeparam>
        /// <param name="func">lambda expression like t=>t.colname</param>
        public String GetFieldName<M,T>(Expression<Func<M, T>> func)
        {
            if (func == null || func.Parameters == null || func.Parameters.Count == 0)
            {
                throw new Exception("Lambda expression is invalid.");
            }

            var parameter = func.Parameters[0];
            if (parameter == null)
            {
                throw new Exception($"Lambda expression[{func.ToString()}] is invalid.");
            }

            var member = func.Body as MemberExpression;
            if (member == null)
            {
                throw new Exception($"Lambda expression[{func.ToString()}] is invalid.");
            }

            var info = parameter.Type.GetProperty(member.Member.Name);
            var fieldName = Cache
                .GetOrAdd($"{parameter.GetType().FullName}:{info.Module.GetHashCode()}", moduleKey => new ConcurrentDictionary<Int32, String>())
                .GetOrAdd(info.MetadataToken, innnerKey =>
                {
                    if (info.IsDefined(typeof(ColumnAttribute), false))
                    {
                        var attr = (ColumnAttribute)info.GetCustomAttributes(typeof(ColumnAttribute), false)[0];
                        return attr.Name;
                    }
                    return info.Name;
                });

            return fieldName;
        }
    }
}
