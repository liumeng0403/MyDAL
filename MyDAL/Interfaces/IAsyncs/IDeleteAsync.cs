using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces.IAsyncs
{
    internal interface IDeleteAsync
    {
        Task<int> DeleteAsync();
    }
}
