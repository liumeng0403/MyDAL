using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.DataBase;
using EasyDAL.Exchange.Handler;
using EasyDAL.Exchange.MapperX;
using EasyDAL.Exchange.Parameter;

namespace EasyDAL.Exchange.DynamicParameter
{
    /// <summary>
    /// A bag of parameters that can be passed to the Dapper Query and Execute methods
    /// </summary>
    public partial class DynamicParameters : IDynamicParameters, IParameterLookup, IParameterCallbacks
    {
        internal const DbType EnumerableMultiParameter = (DbType)(-1);
        private static readonly Dictionary<Identity, Action<IDbCommand, object>> paramReaderCache = new Dictionary<Identity, Action<IDbCommand, object>>();
        private readonly Dictionary<string, ParamInfo> parameters = new Dictionary<string, ParamInfo>();
        private List<object> templates;

        /// <summary>
        /// If true, the command-text is inspected and only values that are clearly used are included on the connection
        /// </summary>
        public bool RemoveUnused { get; set; }

        object IParameterLookup.this[string name] =>
            parameters.TryGetValue(name, out ParamInfo param) ? param.Value : null;
        
        /// <summary>
        /// constructer
        /// </summary>
        public DynamicParameters()
        {
            RemoveUnused = true;
        }

        /// <summary>
        /// constructer
        /// </summary>
        /// <param name="template">匿名对象 or a DynamicParameters</param>
        public DynamicParameters(object template)
        {
            RemoveUnused = true;
            AddDynamicParams(template);
        }

        /// <summary>
        /// Append a whole object full of params to the dynamic
        /// EG: AddDynamicParams(new {A = 1, B = 2}) // will add property A and B to the dynamic
        /// </summary>
        public void AddDynamicParams(object param)
        {
            var obj = param;
            if (obj != null)
            {
                var subDynamic = obj as DynamicParameters;
                if (subDynamic == null)
                {
                    var dictionary = obj as IEnumerable<KeyValuePair<string, object>>;
                    if (dictionary == null)
                    {
                        templates = templates ?? new List<object>();
                        templates.Add(obj);
                    }
                    else
                    {
                        foreach (var kvp in dictionary)
                        {
                            Add(kvp.Key, kvp.Value, null, null, null);
                        }
                    }
                }
                else
                {
                    if (subDynamic.parameters != null)
                    {
                        foreach (var kvp in subDynamic.parameters)
                        {
                            parameters.Add(kvp.Key, kvp.Value);
                        }
                    }

                    if (subDynamic.templates != null)
                    {
                        templates = templates ?? new List<object>();
                        foreach (var t in subDynamic.templates)
                        {
                            templates.Add(t);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add a parameter to this dynamic parameter list.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <param name="dbType">The type of the parameter.</param>
        /// <param name="direction">The in or out direction of the parameter.</param>
        /// <param name="size">The size of the parameter.</param>
        public void Add(string name, object value, DbType? dbType, ParameterDirection? direction, int? size)
        {
            parameters[CleanKeyStr(name)] = new ParamInfo
            {
                Name = name,
                Value = value,
                ParameterDirection = direction ?? ParameterDirection.Input,
                DbType = dbType,
                Size = size
            };
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
        protected void AddParameters(IDbCommand command, Identity identity)
        {
            var literals = SqlMapper.GetLiteralTokens(identity.sql);

            if (templates != null)
            {
                foreach (var template in templates)
                {
                    var newIdent = identity.ForDynamicParameters(template.GetType());
                    Action<IDbCommand, object> appender;

                    lock (paramReaderCache)
                    {
                        if (!paramReaderCache.TryGetValue(newIdent, out appender))
                        {
                            appender = SqlMapper.CreateParamInfoGenerator(newIdent, true, RemoveUnused, literals);
                            paramReaderCache[newIdent] = appender;
                        }
                    }

                    appender(command, template);
                }

                // The parameters were added to the command, but not the
                // DynamicParameters until now.
                foreach (IDbDataParameter param in command.Parameters)
                {
                    // If someone makes a DynamicParameters with a template,
                    // then explicitly adds a parameter of a matching name,
                    // it will already exist in 'parameters'.
                    if (!parameters.ContainsKey(param.ParameterName))
                    {
                        parameters.Add(param.ParameterName, new ParamInfo
                        {
                            AttachedParam = param,
                            CameFromTemplate = true,
                            DbType = param.DbType,
                            Name = param.ParameterName,
                            ParameterDirection = param.Direction,
                            Size = param.Size,
                            Value = param.Value
                        });
                    }
                }

                // Now that the parameters are added to the command, let's place our output callbacks
                var tmp = outputCallbacks;
                if (tmp != null)
                {
                    foreach (var generator in tmp)
                    {
                        generator();
                    }
                }
            }

            foreach (var param in parameters.Values)
            {
                if (param.CameFromTemplate) continue;

                var dbType = param.DbType;
                var val = param.Value;
                string name = CleanKeyStr(param.Name);
                var isCustomQueryParameter = val is ICustomQueryParameter;

                ITypeHandler handler = null;
                if (dbType == null && val != null && !isCustomQueryParameter)
                {
#pragma warning disable 618
                    dbType = SqlMapper.LookupDbType(val.GetType(), name, true, out handler);
#pragma warning disable 618
                }
                if (isCustomQueryParameter)
                {
                    ((ICustomQueryParameter)val).AddParameter(command, name);
                }
                else if (dbType == EnumerableMultiParameter)
                {
#pragma warning disable 612, 618
                    SqlMapper.PackListParameters(command, name, val);
#pragma warning restore 612, 618
                }
                else
                {
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
                    if (handler == null)
                    {
#pragma warning disable 0618
                        p.Value = SqlMapper.SanitizeParameterValue(val);
#pragma warning restore 0618
                        if (dbType != null && p.DbType != dbType)
                        {
                            p.DbType = dbType.Value;
                        }
                        var s = val as string;
                        if (s?.Length <= DbString.DefaultLength)
                        {
                            p.Size = DbString.DefaultLength;
                        }
                        if (param.Size != null) p.Size = param.Size.Value;
                        if (param.Precision != null) p.Precision = param.Precision.Value;
                        if (param.Scale != null) p.Scale = param.Scale.Value;
                    }
                    else
                    {
                        if (dbType != null) p.DbType = dbType.Value;
                        if (param.Size != null) p.Size = param.Size.Value;
                        if (param.Precision != null) p.Precision = param.Precision.Value;
                        if (param.Scale != null) p.Scale = param.Scale.Value;
                        handler.SetValue(p, val ?? DBNull.Value);
                    }

                    if (add)
                    {
                        command.Parameters.Add(p);
                    }
                    param.AttachedParam = p;
                }
            }

            // note: most non-priveleged implementations would use: this.ReplaceLiterals(command);
            if (literals.Count != 0) SqlMapper.ReplaceLiterals(this, command, literals);
        }

        private List<Action> outputCallbacks;

        void IParameterCallbacks.OnCompleted()
        {
            foreach (var param in from p in parameters select p.Value)
            {
                param.OutputCallback?.Invoke(param.OutputTarget, this);
            }
        }
    }
}
