using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using System.Collections.Generic;
using System.Text;

namespace MyDAL.DataRainbow.XCommon.Interfaces
{
    internal interface ISql
    {
        void ObjLeftSymbol(StringBuilder sb);
        void ObjRightSymbol(StringBuilder sb);
        void ParamSymbol(StringBuilder sb);

        /*************************************************************************************************************************************************************/

        void Top(Context dc, StringBuilder sb);
        void Column(string tbAlias, string colName, StringBuilder sb);
        void ColumnAlias(string tbAlias, string colAlias, StringBuilder sb);
        void ColumnReplaceNullValueForSum(string tbAlias, string colName, StringBuilder sb);
        void TableX(string tbName, StringBuilder sb);
        void TableXAlias(string tbAlias, StringBuilder sb);
        void MultiAction(ActionEnum action, StringBuilder sb, Context dc);
        void DbParam(string param, StringBuilder sb);
        void OneEqualOneProcess(DicParam p, StringBuilder sb);
        void WhereTrueOrFalse(Context dc, bool flag, StringBuilder sb);
        ColumnInfo GetIndex(List<ColumnInfo> cols);
        void Pager(Context dc, StringBuilder sb);
    }
}
