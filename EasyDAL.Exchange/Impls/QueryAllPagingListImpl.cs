using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Impls
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
