using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls.ImplAsyncs
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
            PreExecuteHandle(UiMethodEnum.Count);
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
            PreExecuteHandle(UiMethodEnum.Count);            
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
            PreExecuteHandle(UiMethodEnum.Count);
            return await DSA.ExecuteScalarAsync<int>();
        }
        public async Task<int> CountAsync<F>(Expression<Func<F>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.XE.FuncTExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.Count);            
            return await DSA.ExecuteScalarAsync<int>();
        }

    }
}
