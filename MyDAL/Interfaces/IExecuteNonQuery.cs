using System.Threading.Tasks;

namespace HPC.DAL.Interfaces
{
    internal interface IExecuteNonQuerySQLAsync
    {
        Task<int> ExecuteNonQueryAsync();
    }
    internal interface IExecuteNonQuerySQL
    {
        int ExecuteNonQuery();
    }
}
