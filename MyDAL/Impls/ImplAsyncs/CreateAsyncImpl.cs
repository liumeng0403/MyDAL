using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MyDAL.Impls.ImplAsyncs
{
    internal sealed class CreateAsyncImpl<M>
        : ImplerAsync
        , ICreateAsync<M>
    where M : class
    {
        public CreateAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<int> CreateAsync(M m)
        {
            DC.Action = ActionEnum.Insert;
            CreateMHandle(new List<M> { m });
            PreExecuteHandle(UiMethodEnum.Create);
            return await DSA.ExecuteNonQueryAsync();
        }

    }
}
