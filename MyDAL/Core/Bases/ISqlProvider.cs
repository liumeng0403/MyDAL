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
        void GetSQL();
    }
}
