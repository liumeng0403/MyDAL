using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.IAsyncs;
using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.Impls.ImplAsyncs
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
            PreExecuteHandle(UiMethodEnum.Update);
            return await DSA.ExecuteNonQueryAsync();
        }

    }
}
