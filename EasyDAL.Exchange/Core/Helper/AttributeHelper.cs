using MyDAL.Cache;
using MyDAL.Core.Bases;
using MyDAL.Core.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace MyDAL.Core.Helper
{
    internal class AttributeHelper 
    {

        private Context DC { get; set; }

        internal AttributeHelper(Context dc)
        {
            DC = dc;
        }

        /*************************************************************************************************************************************/
        
        internal string GetAttributePropVal<M,A>(Expression<Func<A, string>> attrPropFunc)
            where A : Attribute
        {
            var dic = DC.EH.FuncMFExpression(attrPropFunc)[0];
            var mType = typeof(M);
            var key = DC.SC.GetAttrPropKey(dic.ColumnOne, typeof(A).FullName, mType.FullName);
            if (!StaticCache.ModelAttributePropValCache.ContainsKey(key))
            {
                var attr = mType.GetCustomAttributes(typeof(A), false).FirstOrDefault();
                var  value = attr == null ? string.Empty : attrPropFunc.Compile()((A)attr);
                if (!StaticCache.ModelAttributePropValCache.ContainsKey(key)
                    && !value.IsNullStr())
                {
                    StaticCache.ModelAttributePropValCache[key] = value;
                }
                return value;
            }
            return StaticCache.ModelAttributePropValCache[key];
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
                    .GetMember(prop.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)[0]
                    .GetCustomAttribute(typeof(A), false);
            }
            catch (Exception ex)
            {
                throw new Exception("方法 Attribute GetAttribute<M,A>(M m, PropertyInfo prop) 出错:" + ex.Message);
            }
        }

    }
}
