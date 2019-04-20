using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Common;
using HPC.DAL.Core.Enums;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace HPC.DAL.Impls
{
    internal sealed class IsExistAsyncImpl<M>
    : ImplerAsync
    , IIsExistAsync 
    where M : class
    {
        internal IsExistAsyncImpl(Context dc)
            : base(dc) { }

        public async Task<bool> IsExistAsync(IDbTransaction tran = null)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(typeof(M), "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            DSA.Tran = tran;
            var count = await DSA.ExecuteScalarAsync<long>();
            return count > 0;
        }
 
    }
    internal sealed class IsExistImpl<M>
        : ImplerSync
        , IIsExist
        where M : class
    {
        internal IsExistImpl(Context dc)
            : base(dc) { }
          
        public bool IsExist(IDbTransaction tran = null)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(typeof(M), "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            DSS.Tran = tran;
            var count = DSS.ExecuteScalar<long>();
            return count > 0;
        }
    }

    internal sealed class IsExistXAsyncImpl
    : ImplerAsync
    , IIsExistXAsync 
    {
        public IsExistXAsyncImpl(Context dc)
            : base(dc) { }

        public async Task<bool> IsExistAsync(IDbTransaction tran = null)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(dic.TbMType, "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            DSA.Tran = tran;
            var count = await DSA.ExecuteScalarAsync<long>();
            return count > 0;
        }
 
    }
    internal sealed class IsExistXImpl
        : ImplerSync
        , IIsExistX
    {
        public IsExistXImpl(Context dc) 
            : base(dc) {   }
         
        public bool IsExist(IDbTransaction tran = null)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.Parameters.FirstOrDefault(it => it.Action == ActionEnum.From);
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(dic.TbMType, "*") }));
            PreExecuteHandle(UiMethodEnum.ExistAsync);
            DSS.Tran = tran;
            var count = DSS.ExecuteScalar<long>();
            return count > 0;
        }
    }
}
