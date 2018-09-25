using System.Threading.Tasks;

namespace EasyDAL.Exchange.UserFacade.Interfaces
{
    internal interface ICreate<M>
    {
        Task<int> CreateAsync(M m);
    }
}
