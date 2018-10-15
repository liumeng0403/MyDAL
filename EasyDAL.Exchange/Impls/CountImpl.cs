using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.ExpressionX;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class CountImpl<M>
        : Impler, ICount<M>
    {
        internal CountImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<long> CountAsync()
        {
            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.AddConditions(DC.DH.CountDic(typeof(M).FullName,"*"));
            DC.IP.ConvertDic();
            return await DC.DS.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.CountAsync)[0],
                DC.SqlProvider.GetParameters());
        }

        public async Task<long> CountAsync<F>(Expression<Func<M, F>> func)
        {
            var keyDic = DC.EH.ExpressionHandle(ActionEnum.Select,func)[0];
            var key = keyDic.ColumnOne;
            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.AddConditions(DC.DH.CountDic(typeof(M).FullName,key));
            DC.IP.ConvertDic();
            return await DC.DS.ExecuteScalarAsync<long>(
                 DC.Conn,
                 DC.SqlProvider.GetSQL<M>(UiMethodEnum.CountAsync)[0],
                 DC.SqlProvider.GetParameters());
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
            //CountMHandle<M>("*");
            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.AddConditions(DC.DH.CountDic(string.Empty, "*", string.Empty));
            DC.IP.ConvertDic();
            return await DC.DS.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<None>(UiMethodEnum.JoinCountAsync)[0],
                DC.SqlProvider.GetParameters());
        }

        public async Task<long> CountAsync<F>(Expression<Func<F>> func)
        {
            //CountMHandle<M>("*");
            var dic = DC.EH.ExpressionHandle(ActionEnum.Select,func)[0];
            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.AddConditions(DC.DH.CountDic(dic.ClassFullName, dic.ColumnOne, dic.TableAliasOne));
            DC.IP.ConvertDic();
            return await DC.DS.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<None>(UiMethodEnum.JoinCountAsync)[0],
                DC.SqlProvider.GetParameters());
        }
    }
}
