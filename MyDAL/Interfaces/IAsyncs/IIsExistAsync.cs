using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces.IAsyncs
{
    internal interface IIsExistAsync
    {
        Task<bool> IsExistAsync();
    }

    internal interface IIsExistXAsync
    {
        Task<bool> IsExistAsync();
    }
}
