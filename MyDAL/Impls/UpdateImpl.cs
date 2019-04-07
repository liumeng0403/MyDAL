using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class UpdateImpl<M>
        : Impler
        , IUpdateAsync<M>, IUpdate<M>
        where M : class
    {
        internal UpdateImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<int> UpdateAsync(SetEnum set = SetEnum.AllowedNull)
        {
            DC.Set = set;
            PreExecuteHandle(UiMethodEnum.UpdateAsync);
            return await DC.DSA.ExecuteNonQueryAsync();
        }

        public int Update(SetEnum set = SetEnum.AllowedNull)
        {
            DC.Set = set;
            PreExecuteHandle(UiMethodEnum.UpdateAsync);
            return DC.DSS.ExecuteNonQuery();
        }
    }
}
