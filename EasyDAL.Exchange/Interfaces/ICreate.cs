using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ICreate<M>
    {
        Task<int> CreateAsync(M m);
    }
}
