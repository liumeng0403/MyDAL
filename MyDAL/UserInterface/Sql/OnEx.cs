using MyDAL.Core.Enums;
using MyDAL.UserFacade.Join;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class OnEx
    {

        public static OnX On(this JoinX join, Expression<Func<bool>> compareFunc)
        {
            join.DC.Action = ActionEnum.On;
            var field = join.DC.XE.FuncBoolExpression(compareFunc,ColFuncEnum.None);
            join.DC.DPH.AddParameter(field);
            return new OnX(join.DC);
        }

    }
}
