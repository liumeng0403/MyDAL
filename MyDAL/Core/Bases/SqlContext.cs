using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.DataRainbow;

namespace Yunyong.DataExchange.Core.Bases
{
    internal abstract class SqlContext
        : XSQL
    {
        protected static bool IsOrderByParam(DicParam param)
        {
            if (param.Action == ActionEnum.OrderBy
                && (param.Func == FuncEnum.None
                        || param.Func == FuncEnum.Column
                        || param.Func == FuncEnum.CharLength))
            {
                return true;
            }
            return false;
        }
    }
}
