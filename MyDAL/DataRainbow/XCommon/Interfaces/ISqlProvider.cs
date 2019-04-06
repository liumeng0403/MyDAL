using MyDAL.Core.Common;
using System.Collections.Generic;

namespace MyDAL.DataRainbow.XCommon.Interfaces
{
    internal interface ISqlProvider
    {
        List<ColumnInfo> GetColumnsInfos(string tableName);
        void GetSQL();
    }
}
