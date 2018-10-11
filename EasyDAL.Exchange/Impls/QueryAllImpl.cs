using MyDAL.Core;
using MyDAL.Core.Common;
using MyDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Impls
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
