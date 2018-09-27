using MyDAL.Cache;
using System.Data;

namespace MyDAL.AdoNet.Interfaces
{
    /// <summary>
    /// Implement this interface to pass an arbitrary db specific set of parameters to Dapper
    /// </summary>
    public interface IDynamicParameters
    {
        /// <summary>
        /// Add all the parameters needed to the command just before it executes
        /// </summary>
        /// <param name="command">The raw command prior to execution</param>
        /// <param name="identity">Information about the query</param>
        void AddParameters(IDbCommand command, Identity identity);

        /// <summary>
        /// Get the value of the specified parameter (return null if not found)
        /// </summary>
        /// <param name="name">The name of the parameter to get.</param>
        object this[string name] { get; }


    }
}
