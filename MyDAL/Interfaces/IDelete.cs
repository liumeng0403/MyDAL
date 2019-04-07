using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IDeleteAsync
    {
        Task<int> DeleteAsync();
    }
    internal interface IDelete
    {
        int Delete();
    }
}
