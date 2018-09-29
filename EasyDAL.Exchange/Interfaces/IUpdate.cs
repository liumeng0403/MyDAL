using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IUpdate<M>
    {
        Task<int> UpdateAsync();
    }
}
