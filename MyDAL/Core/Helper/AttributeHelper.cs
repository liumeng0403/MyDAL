using MyDAL.Core.Bases;
using System;
using System.Reflection;

namespace MyDAL.Core.Helper
{
    internal class AttributeHelper
    {

        private Context DC { get; set; }
        internal AttributeHelper()
        { }
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
                throw XConfig.EC.Exception(XConfig.EC._074, $"方法 Attribute GetAttribute<A>(Type mType) 出错:{ex.Message}");
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
                throw XConfig.EC.Exception(XConfig.EC._075, $"方法 Attribute GetAttribute<M,A>(M m, PropertyInfo prop) 出错:{ ex.Message}");
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
                throw XConfig.EC.Exception(XConfig.EC._076, $"方法 Attribute GetAttribute<M,A>(M m, PropertyInfo prop) 出错:{ex.Message}");
            }
        }

    }
}
