using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class UpdateImpl<M>
        : Impler, IUpdate<M>
        where M:class
    {
        internal UpdateImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> UpdateAsync()
        {
            DC.Method = UiMethodEnum.UpdateAsync;
            DC.SqlProvider.GetSQL<M>();
            return await DC.DS.ExecuteNonQueryAsync(
                DC.DPH.GetParameters(DC.Parameters));
        }
    }
}
