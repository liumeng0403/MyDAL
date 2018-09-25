using System.Threading.Tasks;

namespace EasyDAL.Exchange.Interfaces
{
    internal interface IExist
    {
        Task<bool> ExistAsync();
    }
}
