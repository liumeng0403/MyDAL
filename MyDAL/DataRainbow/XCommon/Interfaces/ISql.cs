using MyDAL.Core.Common;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.DataRainbow.XCommon.Interfaces
{
    internal interface ISql
    {
        void Column(string tbAlias, string colName, StringBuilder sb);
        void TableX(string table, StringBuilder sb);
        ColumnInfo GetIndex(List<ColumnInfo> cols);
    }
}
