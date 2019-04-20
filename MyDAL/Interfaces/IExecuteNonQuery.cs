using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces
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
