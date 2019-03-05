using MyDAL.Core.Common;
using MyDAL.Core.Enums;

namespace MyDAL.Core.Bases
{
    internal abstract class SqlContext
        : XSQL
    {
        protected static bool IsPaging(Context dc)
        {
            if (dc.Method == UiMethodEnum.QueryPagingAsync)
            {
                return true;
            }
            return false;
        }

        /****************************************************************************************************************************/

        protected static bool IsDistinctParam(DicParam param)
        {
            if (param.Action == ActionEnum.Select
                && param.Option == OptionEnum.ColumnOther
                && param.Compare == CompareXEnum.Distinct)
            {
                return true;
            }
            return false;
        }
        protected static bool IsSelectColumnParam(DicParam param)
        {
            if (param.Action == ActionEnum.Select
                && (param.Option == OptionEnum.Column || param.Option == OptionEnum.ColumnAs)
                && param.Columns != null)
            {
                return true;
            }
            return false;
        }
        protected static bool IsOrderByParam(DicParam param)
        {
            if (param.Action == ActionEnum.OrderBy
                && (param.Func == FuncEnum.None
                        || param.Func == FuncEnum.CharLength))
            {
                return true;
            }
            return false;
        }
        protected static bool IsCountParam(DicParam param)
        {
            if (param.Option == OptionEnum.Column
                && param.Func == FuncEnum.Count)
            {
                return true;
            }
            return false;
        }
    }
}
