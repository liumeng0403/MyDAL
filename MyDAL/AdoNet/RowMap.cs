using MyDAL.Core.Helper;
using MyDAL.ModelTools;
using MyDAL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyDAL.AdoNet
{
    internal sealed class RowMap
    {
        private List<FieldInfo> Fields { get; }
        private List<PropertyInfo> Properties { get; }
        private Type Type { get; }

        internal static MethodInfo GetPropertySetter(PropertyInfo propertyInfo, Type mType)
        {
            if (propertyInfo.DeclaringType == mType)
            {
                return propertyInfo.GetSetMethod(true);
            }
            return propertyInfo
                .DeclaringType
                .GetProperty(propertyInfo.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder, propertyInfo.PropertyType, propertyInfo.GetIndexParameters().Select(p => p.ParameterType).ToArray(), null)
                .GetSetMethod(true);
        }

        internal RowMap(Type mType)
        {
            Fields = mType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToList();
            Properties = mType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => GetPropertySetter(p, mType) != null).ToList();
            Type = mType;
        }
        internal ConstructorInfo DefaultConstructor()
        {
            var con = Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).FirstOrDefault(it => it.GetParameters().Length == 0);
            if (con==null)
            {
                throw new Exception($"[Table-Model]类[[{Type.FullName}]]必须有默认的构造函数!!!");
            }
            return con;
        }
        internal MapType GetMember(string colName)
        {
            var property = default(PropertyInfo);
            foreach (var prop in Properties)
            {
                var xca = new AttributeHelper().GetAttribute<XColumnAttribute>(Type, prop) as XColumnAttribute;
                if(xca!=null
                    && !xca.Name.IsNullStr()
                    && colName.Equals(xca.Name,StringComparison.OrdinalIgnoreCase))
                {
                    property = prop;
                    break;
                }
                else if(colName.Equals(prop.Name,StringComparison.OrdinalIgnoreCase))
                {
                    property = prop;
                    break;
                }
            }

            //
            if (property != null)
            {
                return new MapType(property);
            }

            // get-only prop
            var backingFieldName = "<" + colName + ">k__BackingField";
            var field = default(FieldInfo);
            foreach (var fld in Fields)
            {
                var xca = new AttributeHelper().GetAttribute<XColumnAttribute>(Type, fld) as XColumnAttribute;
                if (xca != null
                    && !xca.Name.IsNullStr()
                    && (colName.Equals(xca.Name, StringComparison.OrdinalIgnoreCase) || backingFieldName.Equals(xca.Name,StringComparison.OrdinalIgnoreCase)))
                {
                    field = fld;
                    break;
                }
                else if (colName.Equals(fld.Name, StringComparison.OrdinalIgnoreCase)
                    || backingFieldName.Equals(fld.Name,StringComparison.OrdinalIgnoreCase))
                {
                    field = fld;
                    break;
                }
            }

            //
            if (field != null)
            {
                return new MapType(field);
            }

            return null;
        }

    }
}
