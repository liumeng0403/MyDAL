using System;
using System.Collections.Generic;
using System.Data;
using Yunyong.DataExchange.Cache;
using Yunyong.DataExchange.Core;

namespace Yunyong.DataExchange.AdoNet
{
    /// <summary>
    /// A bag of parameters that can be passed to the Dapper Query and Execute methods
    /// </summary>
    internal class DbParameters
    {
        /// <summary>
        /// 清理 key str 中的特殊字符
        /// </summary>
        private static string CleanKeyStr(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                switch (name[0])
                {
                    case '@':
                    case ':':
                    case '?':
                        return name.Substring(1);
                }
            }
            return name;
        }
        private static DbType LookupDbType(Type type, string name)
        {
            //
            var nullUnderlyingType = Nullable.GetUnderlyingType(type);
            if (nullUnderlyingType != null)
            {
                type = nullUnderlyingType;
            }
            if (type.IsEnum && !XConfig.TypeDBTypeDic.ContainsKey(type))
            {
                type = Enum.GetUnderlyingType(type);
            }

            //
            if (XConfig.TypeDBTypeDic.TryGetValue(type, out DbType dbType))
            {
                return dbType;
            }
            if (type.FullName == XConfig.LinqBinary)
            {
                return DbType.Binary;
            }
            //if (typeof(IEnumerable).IsAssignableFrom(type))
            if (typeof(IEnumerable<>).IsAssignableFrom(type))
            {
                return EnumerableMultiParameter;
            }

            //
            throw new NotSupportedException($"The member {name} of type {type.FullName} cannot be used as a parameter value");
        }
        private static DbType EnumerableMultiParameter { get; } = (DbType)(-1);
        private static object SanitizeParameterValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }

        private Dictionary<string, ParamInfo> parameters { get; } = new Dictionary<string, ParamInfo>();

        /// <summary>
        /// Add a parameter to this dynamic parameter list.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="dbType">The type of the parameter.</param>
        /// <param name="direction">The in or out direction of the parameter.</param>
        /// <param name="size">The size of the parameter.</param>
        /// <param name="precision">The precision of the parameter.</param>
        /// <param name="scale">The scale of the parameter.</param>
        internal void Add(string name, object value, DbType dbType)
        {
            parameters[CleanKeyStr(name)] = new ParamInfo
            {
                Name = name,
                Value = value,
                DbType = dbType,
                ParameterDirection = ParameterDirection.Input,  // direction ?? ParameterDirection.Input,
                Size =null,  // size,
                Precision =null , // precision,
                Scale = null //scale
            };
        }

        /// <summary>
        /// Add all the parameters needed to the command just before it executes
        /// </summary>
        /// <param name="command">The raw command prior to execution</param>
        /// <param name="identity">Information about the query</param>
        internal void AddParameters(IDbCommand command, Identity identity)
        {
            foreach (var param in parameters.Values)
            {
                var dbType = param.DbType;
                var val = param.Value;
                string name = CleanKeyStr(param.Name);
                
                if ( val != null)
                {
                    dbType = LookupDbType(val.GetType(), name);
                }

                bool add = !command.Parameters.Contains(name);
                IDbDataParameter p;
                if (add)
                {
                    p = command.CreateParameter();
                    p.ParameterName = name;
                }
                else
                {
                    p = (IDbDataParameter)command.Parameters[name];
                }

                p.Direction = param.ParameterDirection;

                p.Value = SanitizeParameterValue(val);

                if ( p.DbType != dbType)
                {
                    p.DbType = dbType;
                }
                var s = val as string;
                if (s?.Length <= XConfig.StringDefaultLength)
                {
                    p.Size = XConfig.StringDefaultLength;
                }
                if (param.Size != null) p.Size = param.Size.Value;
                if (param.Precision != null) p.Precision = param.Precision.Value;
                if (param.Scale != null) p.Scale = param.Scale.Value;

                if (add)
                {
                    command.Parameters.Add(p);
                }
                param.AttachedParam = p;
            }
        }

    }
}
