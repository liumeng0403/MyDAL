using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.ISyncs;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HPC.DAL.Impls.ImplSyncs
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
            PreExecuteHandle(UiMethodEnum.ExistAsync);
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
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            var count = DSS.ExecuteScalar<long>();
            return count > 0;
        }
    }
}
