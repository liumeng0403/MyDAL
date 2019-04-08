using HPC.DAL.Core.Bases;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace HPC.DAL.Core.Helper
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

        internal Attribute GetAttribute<A>(Type mType)
        {
            try
            {
                return mType
                    .GetCustomAttribute(typeof(A), false);
            }
            catch (Exception ex)
            {
                throw new Exception("方法 Attribute GetAttribute<A>(Type mType) 出错:" + ex.Message);
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
