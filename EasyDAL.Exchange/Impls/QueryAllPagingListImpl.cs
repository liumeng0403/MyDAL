using MyDAL.Common;
using MyDAL.Core;
using MyDAL.Enums;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class QueryAllPagingListImpl<M>
        : Impler, IQueryAllPagingList<M>
    {
        internal QueryAllPagingListImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<PagingList<M>> QueryAllPagingListAsync(int pageIndex, int pageSize)
        {
            return await QueryPagingListAsyncHandle<M, M>(pageIndex, pageSize, UiMethodEnum.QueryAllPagingListAsync);
        }

        public async Task<PagingList<VM>> QueryAllPagingListAsync<VM>(int pageIndex, int pageSize)
        {
            return await QueryPagingListAsyncHandle<M, VM>(pageIndex, pageSize, UiMethodEnum.QueryAllPagingListAsync);
        }
    }
}
