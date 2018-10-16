using MyDAL.Core.Enums;
using MyDAL.UserFacade.Join;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    public static class JoinExtension
    {

        /***************************************************************************************************************************/

        public static JoinX InnerJoin<M>(this FromX from, Expression<Func<M>> func)
        {
            from.DC.Action = ActionEnum.InnerJoin;
            var dic = from.DC.EH.ExpressionHandle( func)[0];
            from.DC.AddConditions(dic);
            return new JoinX(from.DC);
        }

        public static JoinX LeftJoin<M>(this FromX from, Expression<Func<M>> func)
        {
            from.DC.Action = ActionEnum.LeftJoin;
            var dic = from.DC.EH.ExpressionHandle( func)[0];
            from.DC.AddConditions(dic);
            return new JoinX(from.DC);
        }

        /***************************************************************************************************************************/

        public static JoinX InnerJoin<M>(this OnX on, Expression<Func<M>> func)
        {
            on.DC.Action = ActionEnum.InnerJoin;
            var dic = on.DC.EH.ExpressionHandle( func)[0];
            on.DC.AddConditions(dic);
            return new JoinX(on.DC);
        }

        public static JoinX LeftJoin<M>(this OnX on, Expression<Func<M>> func)
        {
            on.DC.Action = ActionEnum.LeftJoin;
            var dic = on.DC.EH.ExpressionHandle( func)[0];
            on.DC.AddConditions(dic);
            return new JoinX(on.DC);
        }

    }
}
