using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IUpdate
    {
        Task<int> UpdateAsync();
    }
}
