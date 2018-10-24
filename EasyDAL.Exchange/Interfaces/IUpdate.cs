using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IUpdate<M>
        where M:class
    {
        Task<int> UpdateAsync();
    }
}
