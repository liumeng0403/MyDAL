﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ICreateBatchAsync<M>
        where M:class
    {
        Task<int> CreateBatchAsync(IEnumerable<M> mList, IDbTransaction tran = null);
    }
    internal interface ICreateBatch<M>
    where M : class
    {
        int CreateBatch(IEnumerable<M> mList, IDbTransaction tran = null);
    }
}
