using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MyDAL.Impls.ImplSyncs
{
    internal sealed class IsExistImpl<M>
        : ImplerSync
        , IIsExist
        where M : class
    {
        internal IsExistImpl(Context dc)
            : base(dc) { }

        public bool IsExist()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(typeof(M), "*") }));
            PreExecuteHandle(UiMethodEnum.IsExist);
            var count = DSS.ExecuteScalar<long>();
            return count > 0;
        }
    }

    internal sealed class IsExistXImpl
        : ImplerSync
        , IIsExistX
    {
        public IsExistXImpl(Context dc)
            : base(dc) { }

        public bool IsExist()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(dic.TbMType, "*") }));
            PreExecuteHandle(UiMethodEnum.IsExist);
            var count = DSS.ExecuteScalar<long>();
            return count > 0;
        }
    }
}
