using System.Threading.Tasks;

namespace HPC.DAL.Interfaces
{
    internal interface ICreateAsync<M>
        where M:class
    {
        Task<int> CreateAsync(M m);
    }
    internal interface ICreate<M>
        where M : class
    {
        int Create(M m);
    }
}
