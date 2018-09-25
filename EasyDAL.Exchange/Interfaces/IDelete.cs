using System.Threading.Tasks;

namespace Yunyong.DataExchange.Interfaces
{
    internal interface IDelete
    {
        Task<int> DeleteAsync();
    }
}
