using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IUpdateAsync<M>
        where M : class
    {
        Task<int> UpdateAsync(IDbTransaction tran = null,SetEnum set = SetEnum.AllowedNull);
    }
    internal interface IUpdate<M>
    where M : class
    {
        int Update(IDbTransaction tran = null,SetEnum set = SetEnum.AllowedNull);
    }
}
