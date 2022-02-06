using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface IInsertAsync<M>
        where M : class
    {
        Task<int> InsertAsync(M m);
    }
}
