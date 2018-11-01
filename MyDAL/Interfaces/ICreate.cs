using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ICreate<M>
        where M:class
    {
        Task<int> CreateAsync(M m);
    }
}
