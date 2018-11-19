using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class CountImpl<M>
        : Impler, ICount<M>
        where M : class
    {
        internal CountImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<int> CountAsync()
        {
            DC.Action = ActionEnum.Select;

            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.DPH.AddParameter(DC.DPH.CountDic(typeof(M).FullName, "*"));
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return await DC.DS.ExecuteScalarAsync<int>();
        }

        public async Task<int> CountAsync<F>(Expression<Func<M, F>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            var keyDic = DC.EH.FuncMFExpression(propertyFunc)[0];
            var key = keyDic.ColumnOne;
            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.DPH.AddParameter(DC.DPH.CountDic(typeof(M).FullName, key));
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return await DC.DS.ExecuteScalarAsync<int>();
        }
    }

    internal class CountXImpl
        : Impler, ICountX
    {
        public CountXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<int> CountAsync()
        {
            DC.Action = ActionEnum.Select;

            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.DPH.AddParameter(DC.DPH.CountDic(string.Empty, "*", string.Empty));
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return await DC.DS.ExecuteScalarAsync<int>();
        }

        public async Task<int> CountAsync<F>(Expression<Func<F>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            var dic = DC.EH.FuncTExpression(propertyFunc)[0];
            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.DPH.AddParameter(DC.DPH.CountDic(dic.ClassFullName, dic.ColumnOne, dic.TableAliasOne));
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return await DC.DS.ExecuteScalarAsync<int>();
        }
    }
}
