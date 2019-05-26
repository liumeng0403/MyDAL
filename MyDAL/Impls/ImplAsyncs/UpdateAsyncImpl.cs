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
    internal sealed class UpdateAsyncImpl<M>
        : ImplerAsync
        , IUpdateAsync<M>
        where M : class
    {
        internal UpdateAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<int> UpdateAsync()
        {
            PreExecuteHandle(UiMethodEnum.UpdateAsync);
            return await DSA.ExecuteNonQueryAsync();
        }

    }
}
