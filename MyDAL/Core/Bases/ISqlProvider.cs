using System.Collections.Generic;
using System.Threading.Tasks;
using Yunyong.DataExchange.DataRainbow;

namespace Yunyong.DataExchange.Core.Bases
{
    internal interface ISqlProvider
    {
        string GetTableName<M>();
        Task<List<ColumnInfo>> GetColumnsInfos(string tableName);
        string GetTablePK(string fullName);
        void GetSQL();
    }
}
