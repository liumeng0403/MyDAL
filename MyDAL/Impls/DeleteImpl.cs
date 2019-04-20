using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces;
using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Impls
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
    internal sealed class DeleteImpl<M>
        : ImplerSync
        , IDelete
        where M : class
    {
        internal DeleteImpl(Context dc)
            : base(dc)
        { }

        public int Delete(IDbTransaction tran = null)
        {
            PreExecuteHandle(UiMethodEnum.DeleteAsync);
            DSS.Tran = tran;
            return DSS.ExecuteNonQuery();
        }

    }
}
