using System.Threading.Tasks;
using Yunyong.Core;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IAllPagingList<M>
    {
        Task<PagingList<M>> PagingAllListAsync(int pageIndex, int pageSize);
        Task<PagingList<VM>> PagingAllListAsync<VM>(int pageIndex, int pageSize)
            where VM : class;
    }
}
