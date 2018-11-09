using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class ExistImpl<M>
        : Impler, IExist
        where M:class
    {
        internal ExistImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<bool> ExistAsync()
        {
            DC.Action = ActionEnum.Select;

            DC.Option = OptionEnum.Count;
            DC.Compare = CompareEnum.None;
            DC.DPH.AddParameter(DC.DPH.CountDic(typeof(M).FullName, "*"));
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.ExistAsync;
            DC.SqlProvider.GetSQL();
            var count = await DC.DS.ExecuteScalarAsync<long>();
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
