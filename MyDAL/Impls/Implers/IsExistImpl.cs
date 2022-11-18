using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using System.Collections.Generic;
using System.Linq;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers.Base;

namespace MyDAL.Impls.Implers
{
    internal sealed class IsExistImpl<M>
        : ImplerBase
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
        : ImplerBase
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
