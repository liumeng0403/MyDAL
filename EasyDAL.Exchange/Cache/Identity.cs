using System;
using System.Collections.Generic;
using System.Data;

namespace Yunyong.DataExchange.Cache
{
    /// <summary>
    /// Identity of a cached query in Dapper, used for extensibility.
    /// </summary>
    public class Identity : IEquatable<Identity>
    {
        /// <summary>
        /// 
        /// </summary>
        public static IEqualityComparer<string> ConnectionStringComparer { get; } = StringComparer.Ordinal;
        
        internal Identity(string sql, CommandType commandType, IDbConnection connection, Type type, Type parametersType)
            : this(sql, commandType, connection.ConnectionString, type, parametersType, 0)
        {  }

        private Identity(string sql, CommandType commandType, string connectionString, Type type, Type parametersType, int gridIndex)
        {
            this.sql = sql;
            this.commandType = commandType;
            this.connectionString = connectionString;
            this.type = type;
            this.parametersType = parametersType;
            this.gridIndex = gridIndex;
            unchecked
            {
                hashCode = 17; // we *know* we are using this in a dictionary, so pre-compute this
                hashCode = (hashCode * 23) + commandType.GetHashCode();
                hashCode = (hashCode * 23) + gridIndex.GetHashCode();
                hashCode = (hashCode * 23) + (sql?.GetHashCode() ?? 0);
                hashCode = (hashCode * 23) + (type?.GetHashCode() ?? 0);
                hashCode = (hashCode * 23) + (connectionString == null ? 0 : Identity.ConnectionStringComparer.GetHashCode(connectionString));
                hashCode = (hashCode * 23) + (parametersType?.GetHashCode() ?? 0);
            }
        }

        /// <summary>
        /// Whether this <see cref="Identity"/> equals another.
        /// </summary>
        /// <param name="obj">The other <see cref="object"/> to compare to.</param>
        public override bool Equals(object obj) => Equals(obj as Identity);

        /// <summary>
        /// The raw SQL command.
        /// </summary>
        public string sql { get; }

        /// <summary>
        /// The SQL command type.
        /// </summary>
        public CommandType commandType { get; }

        /// <summary>
        /// The hash code of this Identity.
        /// </summary>
        public int hashCode { get; }

        /// <summary>
        /// The grid index (position in the reader) of this Identity.
        /// </summary>
        public int gridIndex { get; }

        /// <summary>
        /// This <see cref="Type"/> of this Identity.
        /// </summary>
        public Type type { get; }

        /// <summary>
        /// The connection string for this Identity.
        /// </summary>
        public string connectionString { get; }

        /// <summary>
        /// The type of the parameters object for this Identity.
        /// </summary>
        public  Type parametersType { get; }

        /// <summary>
        /// Gets the hash code for this identity.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => hashCode;

        /// <summary>
        /// Compare 2 Identity objects
        /// </summary>
        /// <param name="other">The other <see cref="Identity"/> object to compare.</param>
        /// <returns>Whether the two are equal</returns>
        public bool Equals(Identity other)
        {
            return other != null
                && gridIndex == other.gridIndex
                && type == other.type
                && sql == other.sql
                && commandType == other.commandType
                && Identity.ConnectionStringComparer.Equals(connectionString, other.connectionString)
                && parametersType == other.parametersType;
        }
    }
}
