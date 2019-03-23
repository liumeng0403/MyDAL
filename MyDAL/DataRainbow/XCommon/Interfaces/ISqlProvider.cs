using MyDAL.Core.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.DataRainbow.XCommon.Interfaces
{
    internal interface ISqlProvider
    {
        Task<List<ColumnInfo>> GetColumnsInfos(string tableName);
        void GetSQL();
    }
}
