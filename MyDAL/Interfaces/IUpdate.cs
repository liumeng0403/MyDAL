using System.Threading.Tasks;

namespace HPC.DAL.Interfaces
{
    internal interface IUpdateAsync<M>
        where M : class
    {
        Task<int> UpdateAsync(SetEnum set = SetEnum.AllowedNull);
    }
    internal interface IUpdate<M>
    where M : class
    {
        int Update(SetEnum set = SetEnum.AllowedNull);
    }
}
