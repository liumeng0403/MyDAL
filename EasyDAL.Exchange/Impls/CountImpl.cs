using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.ExpressionX;
using Yunyong.DataExchange.Helper;
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
