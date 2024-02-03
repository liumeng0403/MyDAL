using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using System;
using System.Linq.Expressions;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers.Base;

namespace MyDAL.Impls.Implers
{
    internal sealed class SumImpl<M>
        : ImplerBase
        , ISum<M>
        where M : class
    {
        public SumImpl(Context dc)
            : base(dc)
        { }

        public F Sum<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            var dic = DC.XE.FuncMFExpression(propertyFunc,ColFuncEnum.Sum);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.Sum);
            return DSS.ExecuteScalar<F>();
        }
        public F? Sum<F>(Expression<Func<M, F?>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            var dic = DC.XE.FuncMFExpression(propertyFunc,ColFuncEnum.SumNullable);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.Sum);
            return DSS.ExecuteScalar<F>();
        }
    }

    internal sealed class SumXImpl
        : ImplerBase
        , ISumX
    {
        public SumXImpl(Context dc)
            : base(dc)
        { }

        public F Sum<F>(Expression<Func<F>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            var dic = DC.XE.FuncTExpression(propertyFunc,ColFuncEnum.Sum);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.Sum);
            return DSS.ExecuteScalar<F>();
        }
        public F? Sum<F>(Expression<Func<F?>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            var dic = DC.XE.FuncTExpression(propertyFunc,ColFuncEnum.SumNullable);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.Sum);
            return DSS.ExecuteScalar<F>();
        }
    }
}
