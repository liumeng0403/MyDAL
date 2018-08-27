using EasyDAL.Exchange.Parameter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace EasyDAL.Exchange.MapperX
{
    /// <summary>
    /// Used to pass a IEnumerable&lt;SqlDataRecord&gt; as a SqlDataRecordListTVPParameter
    /// </summary>
    internal sealed class SqlDataRecordListTVPParameter : ICustomQueryParameter
    {
        private readonly IEnumerable<Microsoft.SqlServer.Server.SqlDataRecord> data;
        private readonly string typeName;


        void ICustomQueryParameter.AddParameter(IDbCommand command, string name)
        {
            var param = command.CreateParameter();
            param.ParameterName = name;
            Set(param, data, typeName);
            command.Parameters.Add(param);
        }

        internal static void Set(IDbDataParameter parameter, IEnumerable<Microsoft.SqlServer.Server.SqlDataRecord> data, string typeName)
        {
            parameter.Value = data != null && data.Any() ? data : null;
            if (parameter is System.Data.SqlClient.SqlParameter sqlParam)
            {
                sqlParam.SqlDbType = SqlDbType.Structured;
                sqlParam.TypeName = typeName;
            }
        }
    }
}
