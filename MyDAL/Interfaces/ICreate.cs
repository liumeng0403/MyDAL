using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ICreateAsync<M>
        where M:class
    {
        Task<int> CreateAsync(M m, IDbTransaction tran = null);
    }
    internal interface ICreate<M>
        where M : class
    {
        int Create(M m, IDbTransaction tran = null);
    }
}
