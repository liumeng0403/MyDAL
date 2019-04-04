using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IIsExist
    {
        Task<bool> IsExistAsync();
    }
    internal interface IIsExistSync
    {
        bool IsExist();
    }

    internal interface IIsExistX
    {
        Task<bool> IsExistAsync();
    }
    internal interface IIsExistXSync
    {
        bool IsExist();
    }
}
