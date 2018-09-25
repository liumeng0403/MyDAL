using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface ICreate<M>
    {
        Task<int> CreateAsync(M m);
    }
}
