using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class IsExistImpl<M>
        : Impler
        , IIsExist, IIsExistSync
        where M : class
    {
        internal IsExistImpl(Context dc)
            : base(dc) { }

        public async Task<bool> IsExistAsync()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(typeof(M), "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            var count = await DC.DS.ExecuteScalarAsync<long>();
            return count > 0;
        }

        public bool IsExist()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(typeof(M), "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            var count = DC.DS.ExecuteScalar<long>();
            return count > 0;
        }
    }

    internal class IsExistXImpl
        : Impler
        , IIsExistX, IIsExistXSync
    {
        public IsExistXImpl(Context dc) 
            : base(dc) {   }

        public async Task<bool> IsExistAsync()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(dic.TbMType, "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            var count = await DC.DS.ExecuteScalarAsync<long>();
            return count > 0;
        }

        public bool IsExist()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(dic.TbMType, "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            var count = DC.DS.ExecuteScalar<long>();
            return count > 0;
        }
    }
}
