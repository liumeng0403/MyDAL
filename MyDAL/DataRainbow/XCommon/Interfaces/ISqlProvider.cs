using HPC.DAL.Core.Common;
using System.Collections.Generic;

namespace HPC.DAL.DataRainbow.XCommon.Interfaces
{
    internal interface ISqlProvider
    {
        List<ColumnInfo> GetColumnsInfos(string tableName);
        void GetSQL();
    }
}
