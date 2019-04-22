using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
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
