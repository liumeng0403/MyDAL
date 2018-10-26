using System;
using System.Reflection;

namespace MyDAL.AdoNet
{
    internal sealed class ColMemMap
    {
        /// <summary>
        /// Creates instance for simple property mapping
        /// </summary>
        /// <param name="columnName">DataReader column name</param>
        /// <param name="property">Target property</param>
        internal ColMemMap(PropertyInfo property)
        {
            Property = property;
        }

        /// <summary>
        /// Creates instance for simple field mapping
        /// </summary>
        /// <param name="columnName">DataReader column name</param>
        /// <param name="field">Target property</param>
        internal ColMemMap(FieldInfo field)
        {
            Field = field;
        }
        
        internal Type MemberType()
        {
            if(Property!=null)
            {
                return Property.PropertyType;
            }
            if(Field!=null)
            {
                return Field.FieldType;
            }
            return null;
        }
        internal PropertyInfo Property { get; }
        internal FieldInfo Field { get; }
    }
}
