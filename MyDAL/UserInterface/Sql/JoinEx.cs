using HPC.DAL.Core.Enums;
using HPC.DAL.UserFacade.Join;
using System;
using System.Linq.Expressions;

namespace HPC.DAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class JoinEx
    {

        /***************************************************************************************************************************/

        public static JoinX InnerJoin<M>(this FromX from, Expression<Func<M>> modelFunc)
        {
            from.DC.Action = ActionEnum.InnerJoin;
            var dic = from.DC.XE.FuncTExpression(modelFunc);
            from.DC.DPH.AddParameter(dic);
            from.DC.SetTbMs<M>(dic.TbAlias);
            return new JoinX(from.DC);
        }

        public static JoinX LeftJoin<M>(this FromX from, Expression<Func<M>> modelFunc)
        {
            from.DC.Action = ActionEnum.LeftJoin;
            var dic = from.DC.XE.FuncTExpression(modelFunc);
            from.DC.DPH.AddParameter(dic);
            from.DC.SetTbMs<M>(dic.TbAlias);
            return new JoinX(from.DC);
        }

        /***************************************************************************************************************************/

        public static JoinX InnerJoin<M>(this OnX on, Expression<Func<M>> modelFunc)
        {
            on.DC.Action = ActionEnum.InnerJoin;
            var dic = on.DC.XE.FuncTExpression(modelFunc);
            on.DC.DPH.AddParameter(dic);
            on.DC.SetTbMs<M>(dic.TbAlias);
            return new JoinX(on.DC);
        }

        public static JoinX LeftJoin<M>(this OnX on, Expression<Func<M>> modelFunc)
        {
            on.DC.Action = ActionEnum.LeftJoin;
            var dic = on.DC.XE.FuncTExpression(modelFunc);
            on.DC.DPH.AddParameter(dic);
            on.DC.SetTbMs<M>(dic.TbAlias);
            return new JoinX(on.DC);
        }

    }
}
