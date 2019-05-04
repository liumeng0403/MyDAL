using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Impls.ImplAsyncs
{
    internal sealed class DeleteAsyncImpl<M>
        : ImplerAsync
        , IDeleteAsync
    where M : class
    {
        internal DeleteAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<int> DeleteAsync()
        {
            PreExecuteHandle(UiMethodEnum.DeleteAsync);
            return await DSA.ExecuteNonQueryAsync();
        }

    }
}
