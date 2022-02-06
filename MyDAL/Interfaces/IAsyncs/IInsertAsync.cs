using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface ICreateAsync<M>
        where M : class
    {
        Task<int> InsertAsync(M m);
    }
}
