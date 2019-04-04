using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IUpdate<M>
        where M : class
    {
        Task<int> UpdateAsync(SetEnum set = SetEnum.AllowedNull);
    }
    internal interface IUpdateSync<M>
    where M : class
    {
        int Update(SetEnum set = SetEnum.AllowedNull);
    }
}
