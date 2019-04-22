using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Interfaces.IAsyncs
{
    internal interface IExecuteNonQuerySQLAsync
    {
        Task<int> ExecuteNonQueryAsync(IDbTransaction tran = null);
    }
}
