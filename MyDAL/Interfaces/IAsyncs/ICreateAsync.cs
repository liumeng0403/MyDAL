using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces.IAsyncs
{
    internal interface ICreateAsync<M>
        where M : class
    {
        Task<int> CreateAsync(M m);
    }
}
