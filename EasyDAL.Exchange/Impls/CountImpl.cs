using MyDAL.Common;
using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.ExpressionX;
using MyDAL.Helper;
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
            return await SqlHelper.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.CountAsync)[0],
                DC.GetParameters());
        }

        public async Task<long> CountAsync<F>(Expression<Func<M, F>> func)
        {
            var keyDic = DC.EH.ExpressionHandle(func)[0];
            var key = keyDic.ColumnOne;
            DC.AddConditions(DicHandle.ConditionCountHandle(key));
            return await SqlHelper.ExecuteScalarAsync<long>(
                 DC.Conn,
                 DC.SqlProvider.GetSQL<M>(UiMethodEnum.CountAsync)[0],
                 DC.GetParameters());
        }
    }
}
