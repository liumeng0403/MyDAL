using MyDAL.Core.Common;
using MyDAL.Core.Enums;

namespace MyDAL.Core.Bases
{
    internal abstract class SqlContext
        : XSQL
    {
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
