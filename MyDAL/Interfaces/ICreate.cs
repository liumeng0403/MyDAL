using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface ICreate<M>
        where M:class
    {
        Task<int> CreateAsync(M m);
    }
    internal interface ICreateSync<M>
        where M : class
    {
        int Create(M m);
    }
}
