using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.ISyncs;
using System;
using System.Data;
using System.Linq.Expressions;

namespace HPC.DAL.Impls.ImplSyncs
{
    internal sealed class SumImpl<M>
        : ImplerSync
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
            DC.Func = FuncEnum.Sum;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.SumAsync);
            return DSS.ExecuteScalar<F>();
        }
        public Nullable<F> Sum<F>(Expression<Func<M, Nullable<F>>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.SumNullable;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.SumAsync);
            return DSS.ExecuteScalar<F>();
        }
    }
}
