using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IExist
    {
        Task<bool> ExistAsync();
    }

    internal interface IExistX
    {
        Task<bool> ExistAsync();
    }
}
