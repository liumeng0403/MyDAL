using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IExecuteNonQuerySQL
    {
        Task<int> ExecuteNonQueryAsync();
    }
}
