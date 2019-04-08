using MyDAL.Core.Bases;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class ExecuteNonQuerySQLAsyncImpl
    : ImplerAsync
    , IExecuteNonQuerySQLAsync 
    {
        public ExecuteNonQuerySQLAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<int> ExecuteNonQueryAsync()
        {
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

        public int ExecuteNonQuery()
        {
            return DSS.ExecuteNonQuery();
        }
    }
}
