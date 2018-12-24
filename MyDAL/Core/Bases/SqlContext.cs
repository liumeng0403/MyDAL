using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;

namespace Yunyong.DataExchange.Core.Bases
{
    internal abstract class SqlContext
        : XSQL
    {
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
    }
}
