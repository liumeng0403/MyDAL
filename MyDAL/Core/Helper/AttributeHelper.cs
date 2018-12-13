using MyDAL.Core.Bases;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MyDAL.Core.Helper
{
    internal class AttributeHelper
    {

        private Context DC { get; set; }

        internal AttributeHelper()
        {
        }
        internal AttributeHelper(Context dc)
        {
            DC = dc;
        }

        /*************************************************************************************************************************************/

        internal string GetAttributePropVal<M, A>(Expression<Func<A, string>> attrPropFunc)
            where A : Attribute
        {
            var dic = DC.EH.FuncMFExpression(attrPropFunc)[0];
            var mType = typeof(M);
            var key = DC.SC.GetAttrPropKey(dic.ColumnOne, typeof(A).FullName, mType.FullName);
            if (!XCache.ModelAttributePropValCache.ContainsKey(key))
            {
                var attr = mType.GetCustomAttributes(typeof(A), false).FirstOrDefault();
                var value = attr == null ? string.Empty : attrPropFunc.Compile()((A)attr);
                if (!XCache.ModelAttributePropValCache.ContainsKey(key)
                    && !value.IsNullStr())
                {
                    XCache.ModelAttributePropValCache[key] = value;
                }
                return value;
            }
            return XCache.ModelAttributePropValCache[key];
        }
        internal Attribute GetAttribute<A>(Type mType)
        {
            try
            {
                return mType
                    .GetCustomAttribute(typeof(A), false);
            }
            catch (Exception ex)
            {
                throw new Exception("方法 Attribute GetAttribute<M,A>(M m, PropertyInfo prop) 出错:" + ex.Message);
            }
        }
        internal Attribute GetAttribute<A>(Type mType, PropertyInfo prop)
        {
            try
            {
                return mType
                    .GetMember(prop.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)[0]
                    .GetCustomAttribute(typeof(A), false);
            }
            catch (Exception ex)
            {
                throw new Exception("方法 Attribute GetAttribute<M,A>(M m, PropertyInfo prop) 出错:" + ex.Message);
            }
        }
        internal Attribute GetAttribute<A>(Type mType, FieldInfo field)
        {
            try
            {
                return mType
                    .GetMember(field.Name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)[0]
                    .GetCustomAttribute(typeof(A), false);
            }
            catch (Exception ex)
            {
                throw new Exception("方法 Attribute GetAttribute<M,A>(M m, PropertyInfo prop) 出错:" + ex.Message);
            }
        }

    }
}
