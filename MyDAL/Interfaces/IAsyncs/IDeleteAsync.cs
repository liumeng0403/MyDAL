using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Interfaces.IAsyncs
{
    internal interface IDeleteAsync
    {
        Task<int> DeleteAsync();
    }
}
