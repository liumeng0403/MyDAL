using System.Threading.Tasks;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface IQueryAllPagingList<M>
    {
        Task<PagingList<M>> QueryAllPagingListAsync(int pageIndex, int pageSize);
        Task<PagingList<VM>> QueryAllPagingListAsync<VM>(int pageIndex, int pageSize);
    }
}
