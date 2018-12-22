using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class ExistImpl<M>
        : Impler, IExist
        where M : class
    {
        internal ExistImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<bool> ExistAsync()
        {
            DC.Action = ActionEnum.Select;
            //DC.Option = OptionEnum.Count;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(typeof(M).FullName, "*") }));
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
