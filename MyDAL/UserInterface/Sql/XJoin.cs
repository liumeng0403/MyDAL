using System;
using System.Linq.Expressions;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.UserFacade.Join;

namespace Yunyong.DataExchange
{
    public static class XJoin
    {

        /***************************************************************************************************************************/

        public static JoinX InnerJoin<M>(this FromX from, Expression<Func<M>> tableModelFunc)
        {
            from.DC.Action = ActionEnum.InnerJoin;
            var dic = from.DC.EH.FuncTExpression(tableModelFunc)[0];
            from.DC.DPH.AddParameter(dic);
            return new JoinX(from.DC);
        }

        public static JoinX LeftJoin<M>(this FromX from, Expression<Func<M>> tableModelFunc)
        {
            from.DC.Action = ActionEnum.LeftJoin;
            var dic = from.DC.EH.FuncTExpression(tableModelFunc)[0];
            from.DC.DPH.AddParameter(dic);
            return new JoinX(from.DC);
        }

        /***************************************************************************************************************************/

        public static JoinX InnerJoin<M>(this OnX on, Expression<Func<M>> tableModelFunc)
        {
            on.DC.Action = ActionEnum.InnerJoin;
            var dic = on.DC.EH.FuncTExpression(tableModelFunc)[0];
            on.DC.DPH.AddParameter(dic);
            return new JoinX(on.DC);
        }

        public static JoinX LeftJoin<M>(this OnX on, Expression<Func<M>> tableModelFunc)
        {
            on.DC.Action = ActionEnum.LeftJoin;
            var dic = on.DC.EH.FuncTExpression(tableModelFunc)[0];
            on.DC.DPH.AddParameter(dic);
            return new JoinX(on.DC);
        }

    }
}
