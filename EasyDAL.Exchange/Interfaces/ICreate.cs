using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface ICreate<M>
        where M:class
    {
        Task<int> CreateAsync(M m);
    }
}
