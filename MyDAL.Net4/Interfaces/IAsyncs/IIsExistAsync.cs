using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface IIsExistAsync
    {
        Task<bool> IsExistAsync();
    }

    internal interface IIsExistXAsync
    {
        Task<bool> IsExistAsync();
    }
}
