using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IIsExist
    {
        Task<bool> IsExistAsync();
    }

    internal interface IIsExistX
    {
        Task<bool> IsExistAsync();
    }
}
