using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces.IAsyncs
{
    internal interface IIsExistAsync
    {
        Task<bool> IsExistAsync(IDbTransaction tran = null);
    }

    internal interface IIsExistXAsync
    {
        Task<bool> IsExistAsync(IDbTransaction tran = null);
    }
}
