using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IUpdate<M>
        where M:class
    {
        Task<int> UpdateAsync(SetEnum set = SetEnum.AllowedNull);
    }
}
