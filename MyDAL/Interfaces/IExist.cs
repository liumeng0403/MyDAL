using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IExist
    {
        Task<bool> ExistAsync();
    }
}
