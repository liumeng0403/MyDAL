using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IQueryAllPagingList<M>
    {
        Task<PagingList<M>> QueryAllPagingListAsync(int pageIndex, int pageSize);
        Task<PagingList<VM>> QueryAllPagingListAsync<VM>(int pageIndex, int pageSize);
    }
}
