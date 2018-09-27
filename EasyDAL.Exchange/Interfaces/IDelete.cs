using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IDelete
    {
        Task<int> DeleteAsync();
    }
}
