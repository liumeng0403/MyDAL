using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces.IAsyncs
{
    internal interface IUpdateAsync<M>
        where M : class
    {
        Task<int> UpdateAsync();
    }
}
