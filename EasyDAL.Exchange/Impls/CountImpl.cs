using MyDAL.Core;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Core.ExpressionX;
using MyDAL.Core.Helper;
using MyDAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
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
            DC.AddConditions(DicHandle.ConditionCountHandle(CrudTypeEnum.Query,"*"));
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
            DC.AddConditions(DicHandle.ConditionCountHandle(CrudTypeEnum.Query,key));
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
            DC.AddConditions(DicHandle.ConditionCountHandle(CrudTypeEnum.Join, "*", string.Empty));
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
            DC.AddConditions(DicHandle.ConditionCountHandle(CrudTypeEnum.Join, dic.ColumnOne, dic.TableAliasOne));
            DC.IP.ConvertDic();
            return await SqlHelper.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<None>(UiMethodEnum.JoinCountAsync)[0],
                DC.SqlProvider.GetParameters());
        }
    }
}
