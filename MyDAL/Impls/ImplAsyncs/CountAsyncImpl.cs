using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.IAsyncs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Impls.ImplAsyncs
{
    internal sealed class CountAsyncImpl<M>
        : ImplerAsync
        , ICountAsync<M>
    where M : class
    {
        internal CountAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<int> CountAsync()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(typeof(M), "*") }));
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return await DSA.ExecuteScalarAsync<int>();
        }
        public async Task<int> CountAsync<F>(Expression<Func<M, F>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.CountAsync);            
            return await DSA.ExecuteScalarAsync<int>();
        }

    }

    internal sealed class CountXAsyncImpl
        : ImplerAsync
        , ICountXAsync
    {
        public CountXAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<int> CountAsync()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(default(Type), "*", string.Empty) }));
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return await DSA.ExecuteScalarAsync<int>();
        }
        public async Task<int> CountAsync<F>(Expression<Func<F>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.XE.FuncTExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.CountAsync);            
            return await DSA.ExecuteScalarAsync<int>();
        }

    }
}
