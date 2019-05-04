using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.IAsyncs;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HPC.DAL.Impls.ImplAsyncs
{
    internal sealed class IsExistAsyncImpl<M>
        : ImplerAsync
        , IIsExistAsync
    where M : class
    {
        internal IsExistAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<bool> IsExistAsync()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(typeof(M), "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            var count = await DSA.ExecuteScalarAsync<long>();
            return count > 0;
        }

    }

    internal sealed class IsExistXAsyncImpl
        : ImplerAsync
        , IIsExistXAsync
    {
        public IsExistXAsyncImpl(Context dc)
            : base(dc) { }

        public async Task<bool> IsExistAsync()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(dic.TbMType, "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            var count = await DSA.ExecuteScalarAsync<long>();
            return count > 0;
        }

    }
}
