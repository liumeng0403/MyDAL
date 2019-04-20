using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IIsExistAsync
    {
        Task<bool> IsExistAsync(IDbTransaction tran = null);
    }
    internal interface IIsExist
    {
        bool IsExist(IDbTransaction tran = null);
    }

    internal interface IIsExistXAsync
    {
        Task<bool> IsExistAsync(IDbTransaction tran = null);
    }
    internal interface IIsExistX
    {
        bool IsExist(IDbTransaction tran = null);
    }
}
