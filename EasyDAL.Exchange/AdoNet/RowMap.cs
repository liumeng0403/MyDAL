using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yunyong.DataExchange.AdoNet.Interfaces;
using Yunyong.DataExchange.Core.Extensions;
using Yunyong.DataExchange.Core.Helper;

namespace Yunyong.DataExchange.AdoNet
{
    internal sealed class RowMap
    {
        private List<FieldInfo> Fields { get; }
        private List<PropertyInfo> Properties { get; }
        private Type Type { get; }

        internal RowMap(Type mType)
        {
            Fields = mType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).ToList();
            Properties = mType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => AdoNetHelper.GetPropertySetter(p, mType) != null).ToList();
            Type = mType;
        }
        internal ConstructorInfo FindConstructor()
        {
            var cons = Type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (cons.Length != 1
                || cons[0].GetParameters().Length != 0)
            {
                throw new Exception($"请对Model类[[{Type.FullName}]]使用默认的构造函数!!!");
            }
            return cons[0];
        }
        internal IMemberMap GetMember(string colName)
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
                return new SimpleMemberMap(colName, property);
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
                return new SimpleMemberMap(colName, field);
            }

            return null;
        }

    }
}
