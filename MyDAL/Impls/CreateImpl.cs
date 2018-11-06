using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class CreateImpl<M>
        : Impler, ICreate<M>
        where M:class
    {
        public CreateImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<int> CreateAsync(M m)
        {
            DC.Action = ActionEnum.Insert;
            CreateMHandle(m);
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.CreateAsync;
            DC.SqlProvider.GetSQL<M>();
            return await DC.DS.ExecuteNonQueryAsync(
                DC.DPH.GetParameters(DC.Parameters));
        }
    }
}
