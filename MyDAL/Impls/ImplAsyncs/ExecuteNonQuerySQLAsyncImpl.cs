using HPC.DAL.Core.Bases;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.IAsyncs;
using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Impls.ImplAsyncs
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
}
