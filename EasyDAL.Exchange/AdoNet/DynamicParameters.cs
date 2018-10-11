using System.Collections.Generic;
using System.Data;
using Yunyong.DataExchange.AdoNet.Interfaces;
using Yunyong.DataExchange.Cache;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Helper;

namespace Yunyong.DataExchange.AdoNet
{
    /// <summary>
    /// A bag of parameters that can be passed to the Dapper Query and Execute methods
    /// </summary>
    internal class DynamicParameters : IDynamicParameters
    {
        internal static DbType EnumerableMultiParameter {get;} = (DbType)(-1);
        private Dictionary<string, ParamInfo> parameters { get; } = new Dictionary<string, ParamInfo>();

        /// <summary>
        /// If true, the command-text is inspected and only values that are clearly used are included on the connection
        /// </summary>
        public bool RemoveUnused { get; set; }

        object IDynamicParameters.this[string name] =>
            parameters.TryGetValue(name, out ParamInfo param) ? param.Value : null;

        // used
        public DynamicParameters()
        {
            RemoveUnused = true;
        }


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
        public void Add(string name, object value = null, DbType? dbType = null, ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
        {
            parameters[CleanKeyStr(name)] = new ParamInfo
            {
                Name = name,
                Value = value,
                ParameterDirection = direction ?? ParameterDirection.Input,
                DbType = dbType,
                Size = size,
                Precision = precision,
                Scale = scale
            };
        }
        public void Add(ParamInfo param)
        {
            parameters[CleanKeyStr(param.Name)] = param;
        }

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

        void IDynamicParameters.AddParameters(IDbCommand command, Identity identity)
        {
            AddParameters(command, identity);
        }

        /// <summary>
        /// Add all the parameters needed to the command just before it executes
        /// </summary>
        /// <param name="command">The raw command prior to execution</param>
        /// <param name="identity">Information about the query</param>
        public void AddParameters(IDbCommand command, Identity identity)
        {
            foreach (var param in parameters.Values)
            {
                var dbType = param.DbType;
                var val = param.Value;
                string name = CleanKeyStr(param.Name);

                //ITypeHandler handler = null;
                if (dbType == null && val != null)
                {
#pragma warning disable 618
                    dbType = SqlHelper.LookupDbType(val.GetType(), name, true/*, out handler*/);
#pragma warning disable 618
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
                //if (handler == null)
                //{
#pragma warning disable 0618
                    p.Value = SqlHelper.SanitizeParameterValue(val);
#pragma warning restore 0618
                    if (dbType != null && p.DbType != dbType)
                    {
                        p.DbType = dbType.Value;
                    }
                    var s = val as string;
                    if (s?.Length <= Configs.StringDefaultLength)
                    {
                        p.Size = Configs.StringDefaultLength;
                    }
                    if (param.Size != null) p.Size = param.Size.Value;
                    if (param.Precision != null) p.Precision = param.Precision.Value;
                    if (param.Scale != null) p.Scale = param.Scale.Value;
                //}

                if (add)
                {
                    command.Parameters.Add(p);
                }
                param.AttachedParam = p;
            }
        }
        
    }
}
