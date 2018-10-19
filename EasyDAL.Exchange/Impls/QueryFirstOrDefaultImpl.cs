using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class QueryFirstOrDefaultImpl<M>
        : Impler, IQueryFirstOrDefault<M>
        where M : class
    {
        internal QueryFirstOrDefaultImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<M> QueryFirstOrDefaultAsync()
        {
            //return await QueryFirstOrDefaultAsyncHandle<M, M>();
            return await DC.DS.ExecuteReaderSingleRowAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters());
        }

        public async Task<VM> QueryFirstOrDefaultAsync<VM>()
        {
            SelectMHandle<M, VM>();
            DC.IP.ConvertDic();
            //return await QueryFirstOrDefaultAsyncHandle<M, VM>();
            return await DC.DS.ExecuteReaderSingleRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters());
        }

        public async Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<M, VM>> func)
        {
            SelectMHandle(func);
            DC.IP.ConvertDic();
            //return await QueryFirstOrDefaultAsyncHandle<M, VM>();
            return await DC.DS.ExecuteReaderSingleRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters());
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
            return await DC.DS.ExecuteReaderSingleRowAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters());
        }

        public async Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<VM>> func)
        {
            SelectMHandle(func);
            DC.IP.ConvertDic();
            return await DC.DS.ExecuteReaderSingleRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters());
        }
    }
}
