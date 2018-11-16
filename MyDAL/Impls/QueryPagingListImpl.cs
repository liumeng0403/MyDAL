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

        public async Task<PagingList<M>> PagingListAsync(int pageIndex, int pageSize)
        {
            return await PagingListAsyncHandle<M>(pageIndex, pageSize, UiMethodEnum.PagingListAsync);
        }

        public async Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return await PagingListAsyncHandle<M, VM>(pageIndex, pageSize, UiMethodEnum.PagingListAsync);
        }

        public async Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<M, VM>> func)
            where VM : class
        {
            SelectMHandle(func);
            DC.DPH.SetParameter();
            return await PagingListAsyncHandle<M, VM>(pageIndex, pageSize, UiMethodEnum.PagingListAsync);
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

        public async Task<PagingList<M>> PagingListAsync(PagingQueryOption option)
        {
            OrderByOptionHandle(option, typeof(M).FullName);
            DC.DPH.SetParameter();
            return await PagingListAsyncHandle<M>(option.PageIndex, option.PageSize, UiMethodEnum.PagingListAsync);
        }

        public async Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option)
            where VM : class
        {
            SelectMHandle<M, VM>();
            OrderByOptionHandle(option, typeof(M).FullName);
            DC.DPH.SetParameter();
            return await PagingListAsyncHandle<M, VM>(option.PageIndex, option.PageSize, UiMethodEnum.PagingListAsync);
        }

        public async Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option, Expression<Func<M, VM>> func)
            where VM : class
        {
            SelectMHandle(func);
            OrderByOptionHandle(option, typeof(M).FullName);
            DC.DPH.SetParameter();
            return await PagingListAsyncHandle<M, VM>(option.PageIndex, option.PageSize, UiMethodEnum.PagingListAsync);
        }
    }

    internal class QueryPagingListXImpl
        : Impler, IQueryPagingListX
    {
        internal QueryPagingListXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingList<M>> PagingListAsync<M>(int pageIndex, int pageSize)
            where M : class
        {
            SelectMHandle<M>();
            DC.DPH.SetParameter();
            return await PagingListAsyncHandle<M>(pageIndex, pageSize, UiMethodEnum.JoinPagingListAsync);
        }

        public async Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<VM>> func)
            where VM : class
        {
            SelectMHandle(func);
            DC.DPH.SetParameter();
            return await PagingListAsyncHandle<VM>(pageIndex, pageSize, UiMethodEnum.JoinPagingListAsync);
        }
    }

    internal class QueryPagingListXOImpl
        : Impler, IQueryPagingListXO
    {
        internal QueryPagingListXOImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingList<M>> PagingListAsync<M>(PagingQueryOption option)
            where M : class
        {
            SelectMHandle<M>();
            OrderByOptionHandle(option, typeof(M).FullName);
            DC.DPH.SetParameter();
            return await PagingListAsyncHandle<M>(option.PageIndex, option.PageSize, UiMethodEnum.JoinPagingListAsync);
        }

        public async Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option, Expression<Func<VM>> func)
            where VM : class
        {
            SelectMHandle(func);
            OrderByOptionHandle(option, string.Empty);
            DC.DPH.SetParameter();
            return await PagingListAsyncHandle<VM>(option.PageIndex, option.PageSize, UiMethodEnum.JoinPagingListAsync);
        }
    }
}
