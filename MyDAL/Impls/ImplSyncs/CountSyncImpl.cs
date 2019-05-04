using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace MyDAL.Impls.ImplSyncs
{
    internal sealed class CountImpl<M>
        : ImplerSync
        , ICount<M>
        where M : class
    {
        internal CountImpl(Context dc)
            : base(dc)
        {  }

        public int Count()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(typeof(M), "*") }));
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return DSS.ExecuteScalar<int>();
        }
        public int Count<F>(Expression<Func<M, F>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.XE.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return DSS.ExecuteScalar<int>();
        }
    }

    internal sealed class CountXImpl
        : ImplerSync
        , ICountX
    {
        public CountXImpl(Context dc)
            : base(dc)
        { }

        public int Count()
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam> { DC.DPH.CountDic(default(Type), "*", string.Empty) }));
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return DSS.ExecuteScalar<int>();
        }
        public int Count<F>(Expression<Func<F>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Compare = CompareXEnum.None;
            DC.Func = FuncEnum.Count;
            var dic = DC.XE.FuncTExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.CountAsync);
            return DSS.ExecuteScalar<int>();
        }
    }
}
