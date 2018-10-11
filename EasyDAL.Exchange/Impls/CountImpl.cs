using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.ExpressionX;
using Yunyong.DataExchange.Core.Helper;
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
            DC.AddConditions(DicHandle.ConditionCountHandle(CrudTypeEnum.Query,typeof(M).FullName,"*"));
            DC.IP.ConvertDic();
            return await SqlHelper.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.CountAsync)[0],
                DC.SqlProvider.GetParameters());
        }

        public async Task<long> CountAsync<F>(Expression<Func<M, F>> func)
        {
            var keyDic = DC.EH.ExpressionHandle(func)[0];
            var key = keyDic.ColumnOne;
            DC.AddConditions(DicHandle.ConditionCountHandle(CrudTypeEnum.Query,typeof(M).FullName,key));
            DC.IP.ConvertDic();
            return await SqlHelper.ExecuteScalarAsync<long>(
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
            DC.AddConditions(DicHandle.ConditionCountHandle(CrudTypeEnum.Join,string.Empty, "*", string.Empty));
            DC.IP.ConvertDic();
            return await SqlHelper.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<None>(UiMethodEnum.JoinCountAsync)[0],
                DC.SqlProvider.GetParameters());
        }

        public async Task<long> CountAsync<F>(Expression<Func<F>> func)
        {
            //CountMHandle<M>("*");
            var dic = DC.EH.ExpressionHandle(func)[0];
            DC.AddConditions(DicHandle.ConditionCountHandle(CrudTypeEnum.Join,dic.ClassFullName, dic.ColumnOne, dic.TableAliasOne));
            DC.IP.ConvertDic();
            return await SqlHelper.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<None>(UiMethodEnum.JoinCountAsync)[0],
                DC.SqlProvider.GetParameters());
        }
    }
}
