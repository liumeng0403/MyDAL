using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class AllPagingListImpl<M>
        : Impler, IAllPagingList<M>
        where M:class
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
            where VM:class
        {
            return await PagingListAsyncHandle<M, VM>(pageIndex, pageSize, UiMethodEnum.PagingAllListAsync);
        }
    }
}
