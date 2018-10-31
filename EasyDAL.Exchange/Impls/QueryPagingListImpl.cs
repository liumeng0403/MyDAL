using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class QueryPagingListImpl<M>
        : Impler, IQueryPagingList<M>
            where M : class
    {
        internal QueryPagingListImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingList<M>> QueryPagingListAsync(int pageIndex, int pageSize)
        {
            return await QueryPagingListAsyncHandle<M>(pageIndex, pageSize, UiMethodEnum.QueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return await QueryPagingListAsyncHandle<M, VM>(pageIndex, pageSize, UiMethodEnum.QueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<M, VM>> func)
            where VM : class
        {
            SelectMHandle(func);
            DC.DH.UiToDbCopy();
            return await QueryPagingListAsyncHandle<M, VM>(pageIndex, pageSize, UiMethodEnum.QueryPagingListAsync);
        }
    }

    internal class QueryPagingListOImpl<M>
        : Impler, IQueryPagingListO<M>
            where M : class
    {
        internal QueryPagingListOImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingList<M>> QueryPagingListAsync(PagingQueryOption option)
        {
            OrderByOptionHandle(option, typeof(M).FullName);
            DC.DH.UiToDbCopy();
            return await QueryPagingListAsyncHandle<M>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option)
            where VM : class
        {
            SelectMHandle<M, VM>();
            OrderByOptionHandle(option, typeof(M).FullName);
            DC.DH.UiToDbCopy();
            return await QueryPagingListAsyncHandle<M, VM>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<M, VM>> func)
            where VM : class
        {
            SelectMHandle(func);
            OrderByOptionHandle(option, typeof(M).FullName);
            DC.DH.UiToDbCopy();
            return await QueryPagingListAsyncHandle<M, VM>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }
    }

    internal class QueryPagingListXImpl
        : Impler, IQueryPagingListX
    {
        internal QueryPagingListXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingList<M>> QueryPagingListAsync<M>(int pageIndex, int pageSize)
            where M : class
        {
            SelectMHandle<M>();
            DC.DH.UiToDbCopy();
            return await QueryPagingListAsyncHandle<M>(pageIndex, pageSize, UiMethodEnum.JoinQueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<VM>> func)
            where VM : class
        {
            SelectMHandle(func);
            DC.DH.UiToDbCopy();
            return await QueryPagingListAsyncHandle<VM>(pageIndex, pageSize, UiMethodEnum.JoinQueryPagingListAsync);
        }
    }

    internal class QueryPagingListXOImpl
        : Impler, IQueryPagingListXO
    {
        internal QueryPagingListXOImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingList<M>> QueryPagingListAsync<M>(PagingQueryOption option)
            where M : class
        {
            SelectMHandle<M>();
            OrderByOptionHandle(option, typeof(M).FullName);
            DC.DH.UiToDbCopy();
            return await QueryPagingListAsyncHandle<M>(option.PageIndex, option.PageSize, UiMethodEnum.JoinQueryPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<VM>> func)
            where VM : class
        {
            SelectMHandle(func);
            OrderByOptionHandle(option, string.Empty);
            DC.DH.UiToDbCopy();
            return await QueryPagingListAsyncHandle<VM>(option.PageIndex, option.PageSize, UiMethodEnum.JoinQueryPagingListAsync);
        }
    }
}
