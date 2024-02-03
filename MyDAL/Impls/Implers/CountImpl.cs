using MyDAL.Core.Bases;
using MyDAL.Core.Common;
using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers.Base;

namespace MyDAL.Impls.Implers
{
    internal sealed class CountImpl<M>
        : ImplerBase
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
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam>
            {
                DC.DPH.CountDic(typeof(M), "*")
            },ColFuncEnum.Count));
            PreExecuteHandle(UiMethodEnum.Count);
            return DSS.ExecuteScalar<int>();
        }
        public int Count<F>(Expression<Func<M, F>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareXEnum.None;
            var dic = DC.XE.FuncMFExpression(propertyFunc,ColFuncEnum.Count);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.Count);
            return DSS.ExecuteScalar<int>();
        }
    }

    internal sealed class CountXImpl
        : ImplerBase
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
            DC.DPH.AddParameter(DC.DPH.SelectColumnDic(new List<DicParam>
            {
                DC.DPH.CountDic(default(Type), "*", string.Empty)
            },ColFuncEnum.Count));
            PreExecuteHandle(UiMethodEnum.Count);
            return DSS.ExecuteScalar<int>();
        }
        public int Count<F>(Expression<Func<F>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Compare = CompareXEnum.None;
            var dic = DC.XE.FuncTExpression(propertyFunc,ColFuncEnum.Count);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.Count);
            return DSS.ExecuteScalar<int>();
        }
    }
}
