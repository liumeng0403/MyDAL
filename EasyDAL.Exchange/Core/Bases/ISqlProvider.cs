using System.Collections.Generic;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.MySql.Models;

namespace Yunyong.DataExchange.Core.Bases
{
    internal interface ISqlProvider
    {
        string GetTableName<M>();
        Task<List<ColumnInfo>> GetColumnsInfos(string tableName);
        string GetTablePK(string fullName);
        List<string> GetSQL<M>(UiMethodEnum type, int? pageIndex = null, int? pageSize = null);
    }
}
