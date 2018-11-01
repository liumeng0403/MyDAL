using MyDAL.Core.Enums;
using MyDAL.DBRainbow;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Core.Bases
{
    internal interface ISqlProvider
    {
        string GetTableName<M>();
        Task<List<ColumnInfo>> GetColumnsInfos(string tableName);
        string GetTablePK(string fullName);
        List<string> GetSQL<M>(UiMethodEnum type, int? pageIndex = null, int? pageSize = null);
    }
}
