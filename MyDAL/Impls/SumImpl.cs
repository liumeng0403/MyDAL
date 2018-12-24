using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
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
            //DC.Option = OptionEnum.Sum;
            DC.Option = OptionEnum.Column;
            DC.Compare = CompareEnum.None;
            DC.Func = FuncEnum.Sum;
            var dic = DC.EH.FuncMFExpression(propertyFunc);
            DC.DPH.AddParameter(dic);
            PreExecuteHandle(UiMethodEnum.SumAsync);
            return await DC.DS.ExecuteScalarAsync<F>();
        }
    }
}
