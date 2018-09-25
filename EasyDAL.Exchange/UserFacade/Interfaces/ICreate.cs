using System.Threading.Tasks;

namespace Yunyong.DataExchange.UserFacade.Interfaces
{
    internal interface ICreate<M>
    {
        Task<int> CreateAsync(M m);
    }
}
