using MyDAL.Core.Bases;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.IAsyncs;
using System.Threading.Tasks;

namespace MyDAL.Impls.ImplAsyncs
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
            return await DSA.ExecuteNonQueryAsync<None>(null);
        }

    }
}
