using EasyDAL.Exchange.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EasyDAL.Exchange.Handler
{
    internal sealed class DataTableHandler : ITypeHandler
    {
        public object Parse(Type destinationType, object value)
        {
            throw new NotImplementedException();
        }

        public void SetValue(IDbDataParameter parameter, object value)
        {
            TableValuedParameter.Set(parameter, value as DataTable, null);
        }
    }
}
