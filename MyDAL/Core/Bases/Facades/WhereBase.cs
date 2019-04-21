using MyDAL.Core.Enums;
using System;
using System.Linq.Expressions;

namespace MyDAL.Core.Bases.Facades
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public abstract class WhereBase
        : Operator
    {
        internal WhereBase(Context dc)
            : base(dc)
        { }

        internal void AndHandle<M>(Expression<Func<M, bool>> func)
            where M : class
        {
            DC.Action = ActionEnum.And;
            var field = DC.XE.FuncMBoolExpression(func);
            DC.DPH.AddParameter(field);
        }

        internal void OrHandle<M>(Expression<Func<M, bool>> func)
            where M : class
        {
            DC.Action = ActionEnum.Or;
            var field = DC.XE.FuncMBoolExpression(func);
            DC.DPH.AddParameter(field);
        }

    }
}
