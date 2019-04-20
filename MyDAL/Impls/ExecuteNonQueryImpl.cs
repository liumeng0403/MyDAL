using HPC.DAL.Core.Bases;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Impls
{
    internal sealed class ExecuteNonQuerySQLAsyncImpl
    : ImplerAsync
    , IExecuteNonQuerySQLAsync 
    {
        public ExecuteNonQuerySQLAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<int> ExecuteNonQueryAsync(IDbTransaction tran = null)
        {
            DSA.Tran = tran;
            return await DSA.ExecuteNonQueryAsync();
        }
 
    }
    internal sealed class ExecuteNonQuerySQLImpl
        : ImplerSync
        , IExecuteNonQuerySQL
    {
        public ExecuteNonQuerySQLImpl(Context dc) 
            : base(dc)
        {   }

        public int ExecuteNonQuery(IDbTransaction tran = null)
        {
            DSS.Tran = tran;
            return DSS.ExecuteNonQuery();
        }
    }
}
