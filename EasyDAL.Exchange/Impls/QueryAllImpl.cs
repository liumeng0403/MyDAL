using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Yunyong.DataExchange.Impls
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
