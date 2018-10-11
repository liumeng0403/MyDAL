using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Common;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Helper;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class QueryFirstOrDefaultImpl<M>
        : Impler, IQueryFirstOrDefault<M>
    {
        internal QueryFirstOrDefaultImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<M> QueryFirstOrDefaultAsync()
        {
            return await QueryFirstOrDefaultAsyncHandle<M, M>();
        }

        public async Task<VM> QueryFirstOrDefaultAsync<VM>()
        {
            return await QueryFirstOrDefaultAsyncHandle<M, VM>();
        }

        public async Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<M, VM>> func)
        {
            SelectMHandle(func);
            DC.IP.ConvertDic();
            return await QueryFirstOrDefaultAsyncHandle<M, VM>();
        }
    }

    internal class QueryFirstOrDefaultXImpl
        : Impler, IQueryFirstOrDefaultX
    {
        internal QueryFirstOrDefaultXImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<M> QueryFirstOrDefaultAsync<M>()
        {
            SelectMHandle<M>();
            DC.IP.ConvertDic();
            return await SqlHelper.QueryFirstOrDefaultAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters());
        }

        public async Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<VM>> func)
        {
            SelectMHandle(func);           
            DC.IP.ConvertDic();
            return await SqlHelper.QueryFirstOrDefaultAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters());
        }
    }
}
