using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace EasyDAL.Exchange.Map
{
    /// <summary>
    /// Implements custom property mapping by user provided criteria (usually presence of some custom attribute with column to member mapping)
    /// </summary>
    public sealed class CustomPropertyTypeMap : ITypeMap
    {
        private readonly Type _type;
        private readonly Func<Type, string, PropertyInfo> _propertySelector;



        /// <summary>
        /// Always returns default constructor
        /// </summary>
        /// <param name="names">DataReader column names</param>
        /// <param name="types">DataReader column types</param>
        /// <returns>Default constructor</returns>
        public ConstructorInfo FindConstructor(string[] names, Type[] types) =>
            _type.GetConstructor(new Type[0]);
        
        /// <summary>
        /// Not implemented as far as default constructor used for all cases
        /// </summary>
        /// <param name="constructor"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns property based on selector strategy
        /// </summary>
        /// <param name="columnName">DataReader column name</param>
        /// <returns>Poperty member map</returns>
        public IMemberMap GetMember(string columnName)
        {
            var prop = _propertySelector(_type, columnName);
            return prop != null ? new SimpleMemberMap(columnName, prop) : null;
        }
    }
}
