using System;
using System.Reflection;

namespace HPC.DAL.AdoNet
{
    internal sealed class MapType
    {
        //
        internal MapType(PropertyInfo property)
        {
            Property = property;
        }
        internal MapType(FieldInfo field)
        {
            Field = field;
        }
        
        //
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

        //
        internal PropertyInfo Property { get; }
        internal FieldInfo Field { get; }
    }
}
