using EasyDAL.Exchange.Helper;
using System;
using System.Data;

namespace EasyDAL.Exchange.AdoNet
{
    /// <summary>
    /// This class represents a SQL string, it can be used if you need to denote your parameter is a Char vs VarChar vs nVarChar vs nChar
    /// </summary>
    public sealed class DbString 
    {
        /// <summary>
        /// Default value for IsAnsi.
        /// </summary>
        public static bool IsAnsiDefault { get; set; }

        /// <summary>
        /// A value to set the default value of strings
        /// going through Dapper. Default is 4000, any value larger than this
        /// field will not have the default value applied.
        /// </summary>
        public static int DefaultLength { get; } = 4000;

        /// <summary>
        /// Create a new DbString
        /// </summary>
        public DbString()
        {
            Length = -1;
            IsAnsi = IsAnsiDefault;
        }
        /// <summary>
        /// Ansi vs Unicode 
        /// </summary>
        public bool IsAnsi { get; set; }

        /// <summary>
        /// Length of the string -1 for max
        /// </summary>
        public int Length { get; set; }


    }
}
