using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IExist
    {
        Task<bool> IsExistAsync();
    }

    internal interface IExistX
    {
        Task<bool> IsExistAsync();
    }
}
