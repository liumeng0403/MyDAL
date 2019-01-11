using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class ExistImpl<M>
        : Impler, IExist
        where M : class
    {
        internal ExistImpl(Context dc)
            : base(dc) { }

        public async Task<bool> ExistAsync()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(typeof(M).FullName, "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            var count = await DC.DS.ExecuteScalarAsync<long>();
            return count > 0;
        }
    }

    internal class ExistXImpl
        : Impler, IExistX
    {
        public ExistXImpl(Context dc) 
            : base(dc) {   }

        public async Task<bool> ExistAsync()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(dic.TbMFullName, "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            var count = await DC.DS.ExecuteScalarAsync<long>();
            return count > 0;
        }
    }
}
