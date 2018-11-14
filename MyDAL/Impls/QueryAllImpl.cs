using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class QueryAllImpl<M>
        : Impler, IQueryAll<M>
        where M : class
    {
        internal QueryAllImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> QueryAllAsync()
        {
            DC.Method = UiMethodEnum.QueryAllAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<VM>> QueryAllAsync<VM>()
            where VM : class
        {
            DC.Method = UiMethodEnum.QueryAllAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }

        public async Task<List<F>> QueryAllAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.DPH.AddParameter(DC.EH.FuncMFExpression(propertyFunc)[0]);
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.QueryAllAsync;
            DC.SqlProvider.GetSQL();
            return (await DC.DS.ExecuteReaderSingleColumnAsync(propertyFunc.Compile())).ToList();
        }

        public async Task<List<string>> QueryAllAsync(Expression<Func<M, string>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.DPH.AddParameter(DC.EH.FuncMFExpression(propertyFunc)[0]);
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.QueryAllAsync;
            DC.SqlProvider.GetSQL();
            return (await DC.DS.ExecuteReaderSingleColumnAsync(propertyFunc.Compile())).ToList();
        }
    }
}
