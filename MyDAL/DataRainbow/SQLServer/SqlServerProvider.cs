using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyDAL.DataRainbow.SQLServer
{
    internal sealed class SqlServerProvider
       : SqlServer, ISqlProvider
    {
        async Task<List<ColumnInfo>> ISqlProvider.GetColumnsInfos(string tableName)
        {
            throw new NotImplementedException();
        }

        void ISqlProvider.GetSQL()
        {
            throw new NotImplementedException();
        }
    }
}
