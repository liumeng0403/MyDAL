using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class SumImpl<M>
        : Impler, ISum<M>
        where M : class
    {
        public SumImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<F> SumAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            var keyDic = DC.EH.FuncMFExpression(propertyFunc)[0];
            var key = keyDic.ColumnOne;
            DC.Option = OptionEnum.Sum;
            DC.Compare = CompareEnum.None;
            DC.DPH.AddParameter(DC.DPH.SumDic(typeof(M).FullName, key));
            PreExecuteHandle(UiMethodEnum.SumAsync);
            return await DC.DS.ExecuteScalarAsync<F>();
        }
    }
}
