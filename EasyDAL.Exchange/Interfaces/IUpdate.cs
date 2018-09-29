using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IUpdate<M>
    {
        Task<int> UpdateAsync();
    }
}
