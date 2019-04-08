using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class UpdateAsyncImpl<M>
    : ImplerAsync
    , IUpdateAsync<M>
    where M : class
    {
        internal UpdateAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<int> UpdateAsync(SetEnum set = SetEnum.AllowedNull)
        {
            DC.Set = set;
            PreExecuteHandle(UiMethodEnum.UpdateAsync);
            return await DSA.ExecuteNonQueryAsync();
        }

    }
    internal sealed class UpdateImpl<M>
        : ImplerSync
        , IUpdate<M>
        where M : class
    {
        internal UpdateImpl(Context dc)
            : base(dc)
        { }

        public int Update(SetEnum set = SetEnum.AllowedNull)
        {
            DC.Set = set;
            PreExecuteHandle(UiMethodEnum.UpdateAsync);
            return DSS.ExecuteNonQuery();
        }
    }
}
