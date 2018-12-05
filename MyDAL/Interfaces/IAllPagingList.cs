using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IAllPagingList<M>
    {
        Task<PagingList<M>> PagingAllListAsync(int pageIndex, int pageSize);
        Task<PagingList<VM>> PagingAllListAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
        Task<PagingList<T>> PagingAllListAsync<T>(int pageIndex, int pageSize,Expression<Func<M, T>> columnMapFunc);
    }
}
