using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IIsExistAsync
    {
        Task<bool> IsExistAsync();
    }
    internal interface IIsExist
    {
        bool IsExist();
    }

    internal interface IIsExistXAsync
    {
        Task<bool> IsExistAsync();
    }
    internal interface IIsExistX
    {
        bool IsExist();
    }
}
