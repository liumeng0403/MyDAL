using MyDAL.Core.Enums;
using MyDAL.UserFacade.Join;
using System;
using System.Linq.Expressions;

namespace MyDAL
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public static class FromEx
    {

        public static FromX From<M>(this SelecterX join, Expression<Func<M>> tableModelFunc)
        {
            join.DC.Action = ActionEnum.From;
            var dic = join.DC.XE.FuncTExpression(tableModelFunc,ColFuncEnum.None);
            join.DC.DPH.AddParameter(dic);
            join.DC.SetTbMs<M>(dic.TbAlias);
            return new FromX(join.DC);
        }

    }
}
