using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IDeleteAsync
    {
        Task<int> DeleteAsync(IDbTransaction tran = null);
    }
    internal interface IDelete
    {
        int Delete(IDbTransaction tran = null);
    }
}
