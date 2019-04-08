using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class CreateAsyncImpl<M>
    : ImplerAsync
    , ICreateAsync<M> 
    where M : class
    {
        public CreateAsyncImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<int> CreateAsync(M m)
        {
            DC.Action = ActionEnum.Insert;
            CreateMHandle(new List<M> { m });
            PreExecuteHandle(UiMethodEnum.CreateAsync);
            return await DSA.ExecuteNonQueryAsync();
        }
 
    }
    internal sealed class CreateImpl<M>
        : ImplerSync
        , ICreate<M>
        where M : class
    {
        public CreateImpl(Context dc)
            : base(dc)
        {
        }
         
        public int Create(M m)
        {
            DC.Action = ActionEnum.Insert;
            CreateMHandle(new List<M> { m });
            PreExecuteHandle(UiMethodEnum.CreateAsync);
            return DSS.ExecuteNonQuery();
        }
    }
}
