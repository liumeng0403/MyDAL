using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.IAsyncs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Impls.ImplAsyncs
{
    internal sealed class InsertAsyncImpl<M>
        : ImplerAsync
        , IInsertAsync<M>
    where M : class
    {
        public InsertAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<int> InsertAsync(M m)
        {
            DC.Action = ActionEnum.Insert;
            CreateMHandle(new List<M> { m });
            PreExecuteHandle(UiMethodEnum.Create);
            return await DSA.ExecuteNonQueryAsync<M>(new List<M>()
            {
                m
            });
        }

    }
}
