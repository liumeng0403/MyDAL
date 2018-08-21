using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dapper.Handler
{
    internal sealed class SqlDataRecordHandler : ITypeHandler
    {
        public object Parse(Type destinationType, object value)
        {
            throw new NotSupportedException();
        }

        public void SetValue(IDbDataParameter parameter, object value)
        {
            SqlDataRecordListTVPParameter.Set(parameter, value as IEnumerable<Microsoft.SqlServer.Server.SqlDataRecord>, null);
        }
    }
}
