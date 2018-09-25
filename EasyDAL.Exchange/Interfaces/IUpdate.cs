using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IUpdate
    {
        Task<int> UpdateAsync();
    }
}
