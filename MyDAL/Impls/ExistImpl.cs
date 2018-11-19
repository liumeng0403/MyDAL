using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.Impls
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
            PreExecuteHandle(UiMethodEnum.ExistAsync);
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
