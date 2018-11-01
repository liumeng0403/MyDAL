using System.Threading.Tasks;
using Yunyong.Core;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IQueryAllPagingList<M>
    {
        Task<PagingList<M>> QueryAllPagingListAsync(int pageIndex, int pageSize);
        Task<PagingList<VM>> QueryAllPagingListAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
    }
}
