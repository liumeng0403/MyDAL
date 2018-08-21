using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasyDAL.Exchange.DataBase
{
    internal class PropertyInfoByNameComparer : IComparer<PropertyInfo>
    {
        public int Compare(PropertyInfo x, PropertyInfo y) => string.CompareOrdinal(x.Name, y.Name);
    }
}
