using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class AllPagingListImpl<M>
        : Impler, IAllPagingList<M>
        where M : class
    {
        internal AllPagingListImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<PagingList<M>> PagingAllListAsync(int pageIndex, int pageSize)
        {
            return await PagingListAsyncHandle<M>(pageIndex, pageSize, UiMethodEnum.PagingAllListAsync);
        }

        public async Task<PagingList<VM>> PagingAllListAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            DC.PageIndex = pageIndex;
            DC.PageSize = pageSize;
            return await PagingListAsyncHandle<M, VM>(UiMethodEnum.PagingAllListAsync, false, null);
        }

        public async Task<PagingList<T>> PagingAllListAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
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
            return await PagingListAsyncHandle<M, T>(UiMethodEnum.PagingAllListAsync, single, columnMapFunc.Compile());
        }
    }
}
