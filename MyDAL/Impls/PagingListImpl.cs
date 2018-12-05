using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class PagingListImpl<M>
        : Impler, IPagingList<M>
            where M : class
    {
        internal PagingListImpl(Context dc)
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
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            return await PagingListAsyncHandle<M, VM>(UiMethodEnum.PagingListAsync, false, null);
        }

        public async Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<M, VM>> func)
            where VM : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            SelectMHandle(func);
            return await PagingListAsyncHandle<M, VM>(UiMethodEnum.PagingListAsync, false, null);
        }
    }

    internal class PagingListOImpl<M>
        : Impler, IPagingListO<M>
            where M : class
    {
        internal PagingListOImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingList<M>> PagingListAsync(PagingQueryOption option)
        {
            OrderByOptionHandle(option, typeof(M).FullName);
            return await PagingListAsyncHandle<M>(option.PageIndex, option.PageSize, UiMethodEnum.PagingListAsync);
        }

        public async Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option)
            where VM : class
        {
            DC.PageIndex = option.PageIndex;
            DC.PageSize = option.PageSize;
            SelectMHandle<M, VM>();
            OrderByOptionHandle(option, typeof(M).FullName);
            return await PagingListAsyncHandle<M, VM>(UiMethodEnum.PagingListAsync, false, null);
        }

        public async Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option, Expression<Func<M, VM>> func)
            where VM : class
        {
            DC.PageIndex = option.PageIndex;
            DC.PageSize = option.PageSize;
            SelectMHandle(func);
            OrderByOptionHandle(option, typeof(M).FullName);
            return await PagingListAsyncHandle<M, VM>(UiMethodEnum.PagingListAsync, false, null);
        }
    }

    internal class PagingListXImpl
        : Impler, IPagingListX
    {
        internal PagingListXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingList<M>> PagingListAsync<M>(int pageIndex, int pageSize)
            where M : class
        {
            SelectMHandle<M>();
            return await PagingListAsyncHandle<M>(pageIndex, pageSize, UiMethodEnum.PagingListAsync);
        }

        public async Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<VM>> func)
            where VM : class
        {
            SelectMHandle(func);
            return await PagingListAsyncHandle<VM>(pageIndex, pageSize, UiMethodEnum.PagingListAsync);
        }
    }

    internal class PagingListXOImpl
        : Impler, IPagingListXO
    {
        internal PagingListXOImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingList<M>> PagingListAsync<M>(PagingQueryOption option)
            where M : class
        {
            SelectMHandle<M>();
            OrderByOptionHandle(option, typeof(M).FullName);
            return await PagingListAsyncHandle<M>(option.PageIndex, option.PageSize, UiMethodEnum.PagingListAsync);
        }

        public async Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option, Expression<Func<VM>> func)
            where VM : class
        {
            SelectMHandle(func);
            OrderByOptionHandle(option, string.Empty);
            return await PagingListAsyncHandle<VM>(option.PageIndex, option.PageSize, UiMethodEnum.PagingListAsync);
        }
    }
}
