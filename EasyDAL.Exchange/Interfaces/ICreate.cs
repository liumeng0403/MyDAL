using System.Threading.Tasks;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface ICreate<M>
    {
        Task<int> CreateAsync(M m);
    }
}
