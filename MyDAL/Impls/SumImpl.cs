using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class SumAsyncImpl<M>
    : ImplerAsync
    , ISumAsync<M>
    where M : class
    {
        public SumAsyncImpl(Context dc)
            : base(dc)
        {   }

        public async Task<F> SumAsync<F>(Expression<Func<M, F>> propertyFunc, IDbTransaction tran = null)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Sum;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.SumAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteScalarAsync<F>();
        }
        public async Task<Nullable<F>> SumAsync<F>(Expression<Func<M, Nullable<F>>> propertyFunc, IDbTransaction tran = null)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.SumNullable;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.SumAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteScalarAsync<F>();
        }

    }
    internal sealed class SumImpl<M>
        : ImplerSync
        , ISum<M>
        where M : class
    {
        public SumImpl(Context dc)
            : base(dc)
        {
        }

        public F Sum<F>(Expression<Func<M, F>> propertyFunc, IDbTransaction tran = null)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Sum;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.SumAsync);
            DSS.Tran = tran;
            return DSS.ExecuteScalar<F>();
        }
        public Nullable<F> Sum<F>(Expression<Func<M, Nullable<F>>> propertyFunc, IDbTransaction tran = null)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.SumNullable;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.SumAsync);
            DSS.Tran = tran;
            return DSS.ExecuteScalar<F>();
        }
    }
}
