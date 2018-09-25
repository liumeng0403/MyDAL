using System.Threading.Tasks;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface IUpdate
    {
        Task<int> UpdateAsync();
    }
}
