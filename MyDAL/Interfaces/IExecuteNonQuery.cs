using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Interfaces
{
    internal interface IExecuteNonQuerySQLAsync
    {
        Task<int> ExecuteNonQueryAsync(IDbTransaction tran = null);
    }
    internal interface IExecuteNonQuerySQL
    {
        int ExecuteNonQuery(IDbTransaction tran = null);
    }
}
