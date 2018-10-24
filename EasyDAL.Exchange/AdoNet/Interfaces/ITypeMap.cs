using System;
using System.Reflection;

namespace MyDAL.AdoNet.Interfaces
{
    /// <summary>
    /// Implement this interface to change default mapping of reader columns to type members
    /// </summary>
    internal interface ITypeMap
    {
        /// <summary>
        /// Finds best constructor
        /// </summary>
        /// <param name="names">DataReader column names</param>
        /// <param name="types">DataReader column types</param>
        /// <returns>Matching constructor or default one</returns>
        ConstructorInfo FindConstructor(string[] names, Type[] types);


        ///// <summary>
        ///// Gets mapping for constructor parameter
        ///// </summary>
        ///// <param name="constructor">Constructor to resolve</param>
        ///// <param name="columnName">DataReader column name</param>
        ///// <returns>Mapping implementation</returns>
        //IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName);

        /// <summary>
        /// Gets member mapping for column
        /// </summary>
        /// <param name="columnName">DataReader column name</param>
        /// <returns>Mapping implementation</returns>
        IMemberMap GetMember(string columnName);
    }
}
