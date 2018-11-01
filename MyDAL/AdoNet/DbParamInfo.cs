using MyDAL.Cache;
using System;
using System.Collections.Generic;
using System.Data;

namespace MyDAL.AdoNet
{
    internal class DbParamInfo
    {
        private static object DbNull(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }
        private Dictionary<string, ParamInfo> Params { get; } = new Dictionary<string, ParamInfo>();

        internal void Add(ParamInfo para)
        {
            Params[para.Name] = para;
        }
        internal void Add(DbParamInfo paras)
        {
            foreach (var p in paras.Params)
            {
                Params.Add(p.Key, p.Value);
            }
        }
        internal void AddParameters(IDbCommand cmd, Identity identity)
        {
            foreach (var param in Params.Values)
            {
                string name = param.Name;
                var needAdd = !cmd.Parameters.Contains(name);
                var dbPara = default(IDbDataParameter);
                if (needAdd)
                {
                    dbPara = cmd.CreateParameter();
                    dbPara.ParameterName = name;
                }
                else
                {
                    dbPara = (IDbDataParameter)cmd.Parameters[name];
                }

                //
                dbPara.Direction = param.ParameterDirection;
                dbPara.Value = DbNull(param.Value);
                dbPara.DbType = param.Type;
                if (param.Size != null)
                {
                    dbPara.Size = param.Size.Value;
                }
                if (param.Precision != null)
                {
                    dbPara.Precision = param.Precision.Value;
                }
                if (param.Scale != null)
                {
                    dbPara.Scale = param.Scale.Value;
                }

                //
                if (needAdd)
                {
                    cmd.Parameters.Add(dbPara);
                }
            }
        }

    }
}
