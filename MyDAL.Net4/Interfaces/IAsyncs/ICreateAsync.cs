using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface ICreateAsync<M>
        where M : class
    {
        Task<int> CreateAsync(M m);
    }
}
