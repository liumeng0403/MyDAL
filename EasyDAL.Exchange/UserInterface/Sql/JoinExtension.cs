using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.UserFacade.Join;

namespace Yunyong.DataExchange
{
    public static class JoinExtension
    {

        public static JoinX InnerJoin<M>(this FromX from, Expression<Func<M>> func)
        {
            var dic = from.DC.EH.ExpressionHandle(func)[0];
            dic.Action = ActionEnum.InnerJoin;
            dic.Crud = CrudTypeEnum.Join;
            from.DC.AddConditions(dic);
            return new JoinX(from.DC);
        }

        public static JoinX LeftJoin<M>(this FromX from, Expression<Func<M>> func)
        {
            var dic = from.DC.EH.ExpressionHandle(func)[0];
            dic.Action = ActionEnum.LeftJoin;
            dic.Crud = CrudTypeEnum.Join;
            from.DC.AddConditions(dic);
            return new JoinX(from.DC);
        }

    }
}
