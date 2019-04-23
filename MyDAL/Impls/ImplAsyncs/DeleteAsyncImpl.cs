using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.IAsyncs;
using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Impls.ImplAsyncs
{
    internal sealed class DeleteAsyncImpl<M>
    : ImplerAsync
    , IDeleteAsync
    where M : class
    {
        internal DeleteAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<int> DeleteAsync(IDbTransaction tran = null)
        {
            PreExecuteHandle(UiMethodEnum.DeleteAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteNonQueryAsync();
        }

    }
}
