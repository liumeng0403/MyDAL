using MyDAL.Core;
using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class CountImpl<M>
        : Impler, ICount<M>
        where M : class
    {
        internal CountImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<long> CountAsync()
        {
            DC.Action = ActionEnum.Select;

            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.DPH.AddParameter(DC.DPH.CountDic(typeof(M).FullName, "*"));
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.CountAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteScalarAsync<long>();
        }

        public async Task<long> CountAsync<F>(Expression<Func<M, F>> func)
        {
            DC.Action = ActionEnum.Select;
            var keyDic = DC.EH.FuncMFExpression(func)[0];
            var key = keyDic.ColumnOne;
            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.DPH.AddParameter(DC.DPH.CountDic(typeof(M).FullName, key));
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.CountAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteScalarAsync<long>();
        }
    }

    internal class CountXImpl
        : Impler, ICountX
    {
        public CountXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<long> CountAsync()
        {
            DC.Action = ActionEnum.Select;

            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.DPH.AddParameter(DC.DPH.CountDic(string.Empty, "*", string.Empty));
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.JoinCountAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteScalarAsync<long>();
        }

        public async Task<long> CountAsync<F>(Expression<Func<F>> func)
        {
            DC.Action = ActionEnum.Select;
            var dic = DC.EH.FuncTExpression(func)[0];
            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.DPH.AddParameter(DC.DPH.CountDic(dic.ClassFullName, dic.ColumnOne, dic.TableAliasOne));
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.JoinCountAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteScalarAsync<long>();
        }
    }
}
