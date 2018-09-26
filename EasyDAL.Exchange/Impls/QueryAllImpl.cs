using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Impls
{
    internal class QueryAllImpl<M>
        : Impler, IQueryAll<M>
    {
        internal QueryAllImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<List<M>> QueryAllAsync()
        {
            return await QueryAllAsyncHandle<M, M>();
        }

        public async Task<List<VM>> QueryAllAsync<VM>()
        {
            return await QueryAllAsyncHandle<M, VM>();
        }
    }
}
