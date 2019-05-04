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

namespace MyDAL.Impls.ImplAsyncs
{
    internal sealed class SumAsyncImpl<M>
        : ImplerAsync
        , ISumAsync<M>
    where M : class
    {
        public SumAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<F> SumAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Sum;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.SumAsync);
            return await DSA.ExecuteScalarAsync<F>();
        }
        public async Task<Nullable<F>> SumAsync<F>(Expression<Func<M, Nullable<F>>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.SumNullable;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.SumAsync);
            return await DSA.ExecuteScalarAsync<F>();
        }

    }
}
