using System.Threading.Tasks;

namespace HPC.DAL.Interfaces
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
