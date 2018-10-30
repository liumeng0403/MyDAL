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
                DC.SqlProvider.GetParameters(DC.DbConditions));
        }

        public async Task<VM> QueryFirstOrDefaultAsync<VM>()
            where VM:class
        {
            SelectMHandle<M, VM>();
            DC.DH.UiToDbCopy();
            return await DC.DS.ExecuteReaderSingleRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions));
        }

        public async Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<M, VM>> func)
            where VM:class
        {
            SelectMHandle(func);
            DC.DH.UiToDbCopy();
            return await DC.DS.ExecuteReaderSingleRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.QueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions));
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
            where M:class
        {
            SelectMHandle<M>();
            DC.DH.UiToDbCopy();
            return await DC.DS.ExecuteReaderSingleRowAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.JoinQueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions));
        }

        public async Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<VM>> func)
            where VM:class
        {
            SelectMHandle(func);
            DC.DH.UiToDbCopy();
            return await DC.DS.ExecuteReaderSingleRowAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters(DC.DbConditions));
        }
    }
}
