using System.Threading.Tasks;

namespace HPC.DAL.Interfaces
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
