using System.Threading.Tasks;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface IDelete
    {
        Task<int> DeleteAsync();
    }
}
