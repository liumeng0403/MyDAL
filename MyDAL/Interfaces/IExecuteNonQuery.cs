using System.Threading.Tasks;

namespace MyDAL.Interfaces
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
