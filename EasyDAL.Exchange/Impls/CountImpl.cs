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

        public async Task<long> CountAsync()
        {
            DC.Action = ActionEnum.Select;

            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.AddConditions(DC.DH.CountDic(typeof(M).FullName, "*"));
            DC.DH.UiToDbCopy();
            return await DC.DS.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.CountAsync)[0],
                DC.GetParameters(DC.DbConditions));
        }

        public async Task<long> CountAsync<F>(Expression<Func<M, F>> func)
        {
            DC.Action = ActionEnum.Select;
            var keyDic = DC.EH.FuncMFExpression(func)[0];
            var key = keyDic.ColumnOne;
            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.AddConditions(DC.DH.CountDic(typeof(M).FullName, key));
            DC.DH.UiToDbCopy();
            return await DC.DS.ExecuteScalarAsync<long>(
                 DC.Conn,
                 DC.SqlProvider.GetSQL<M>(UiMethodEnum.CountAsync)[0],
                 DC.GetParameters(DC.DbConditions));
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
            DC.AddConditions(DC.DH.CountDic(string.Empty, "*", string.Empty));
            DC.DH.UiToDbCopy();
            return await DC.DS.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<None>(UiMethodEnum.JoinCountAsync)[0],
                DC.GetParameters(DC.DbConditions));
        }

        public async Task<long> CountAsync<F>(Expression<Func<F>> func)
        {
            DC.Action = ActionEnum.Select;
            var dic = DC.EH.FuncTExpression(func)[0];
            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.AddConditions(DC.DH.CountDic(dic.ClassFullName, dic.ColumnOne, dic.TableAliasOne));
            DC.DH.UiToDbCopy();
            return await DC.DS.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<None>(UiMethodEnum.JoinCountAsync)[0],
                DC.GetParameters(DC.DbConditions));
        }
    }
}
