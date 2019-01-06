using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class PagingListImpl<M>
        : Impler, IPagingList<M>
            where M : class
    {
        internal PagingListImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingResult<M>> PagingListAsync(int pageIndex, int pageSize)
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            return await PagingListAsyncHandle<M>(UiMethodEnum.PagingListAsync, false);
        }

        public async Task<PagingResult<VM>> PagingListAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            return await PagingListAsyncHandle<M, VM>(UiMethodEnum.PagingListAsync, false, null);
        }

        public async Task<PagingResult<T>> PagingListAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            var single = typeof(T).IsSingleColumn();
            if (single)
            {
                SingleColumnHandle(columnMapFunc);
            }
            else
            {
                SelectMHandle(columnMapFunc);
            }
            return await PagingListAsyncHandle<M, T>(UiMethodEnum.PagingListAsync, single, columnMapFunc.Compile());
        }
    }

    internal class PagingListOImpl<M>
        : Impler, IPagingListO<M>
            where M : class
    {
        internal PagingListOImpl(Context dc)
            : base(dc) { }

        public async Task<PagingResult<M>> PagingListAsync()
        {
            return await PagingListAsyncHandle<M>(UiMethodEnum.PagingListAsync, false);
        }

        public async Task<PagingResult<VM>> PagingListAsync<VM>()
            where VM : class
        {
            SelectMQ<M, VM>();
            return await PagingListAsyncHandle<M, VM>(UiMethodEnum.PagingListAsync, false, null);
        }

        public async Task<PagingResult<T>> PagingListAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            var single = typeof(T).IsSingleColumn();
            if (single)
            {
                SingleColumnHandle(columnMapFunc);
            }
            else
            {
                SelectMHandle(columnMapFunc);
            }
            return await PagingListAsyncHandle(UiMethodEnum.PagingListAsync, single, columnMapFunc.Compile());
        }
    }

    internal class PagingListXImpl
        : Impler, IPagingListX
    {
        internal PagingListXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingResult<M>> PagingListAsync<M>(int pageIndex, int pageSize)
            where M : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            SelectMHandle<M>();
            return await PagingListAsyncHandle<M>(UiMethodEnum.PagingListAsync, false);
        }

        public async Task<PagingResult<T>> PagingListAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            var single = typeof(T).IsSingleColumn();
            if (single)
            {
                SingleColumnHandle(columnMapFunc);
            }
            else
            {
                SelectMHandle(columnMapFunc);
            }
            return await PagingListAsyncHandle<T>(UiMethodEnum.PagingListAsync, single);
        }
    }

    internal class PagingListXOImpl
        : Impler, IPagingListXO
    {
        internal PagingListXOImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingResult<M>> PagingListAsync<M>(PagingOption option)
            where M : class
        {
            DC.PageIndex = option.PageIndex;
            DC.PageSize = option.PageSize;
            SelectMHandle<M>();
            return await PagingListAsyncHandle<M>(UiMethodEnum.PagingListAsync, false);
        }

        public async Task<PagingResult<T>> PagingListAsync<T>(PagingOption option, Expression<Func<T>> columnMapFunc)
        {
            DC.PageIndex = option.PageIndex;
            DC.PageSize = option.PageSize;
            var single = typeof(T).IsSingleColumn();
            if (single)
            {
                SingleColumnHandle(columnMapFunc);
            }
            else
            {
                SelectMHandle(columnMapFunc);
            }
            return await PagingListAsyncHandle<T>(UiMethodEnum.PagingListAsync, single);
        }
    }
}
