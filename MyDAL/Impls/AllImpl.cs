using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
{
    internal class AllImpl<M>
        : Impler, IAll<M>
        where M : class
    {
        internal AllImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> AllAsync()
        {
            DC.Method = UiMethodEnum.AllAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<VM>> AllAsync<VM>()
            where VM : class
        {
            DC.Method = UiMethodEnum.AllAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }

        public async Task<List<F>> AllAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.DPH.AddParameter(DC.EH.FuncMFExpression(propertyFunc)[0]);
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.AllAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderSingleColumnAsync(propertyFunc.Compile());
        }

        public async Task<List<string>> AllAsync(Expression<Func<M, string>> propertyFunc)
        {
            DC.Action = ActionEnum.Select;
            DC.Option = OptionEnum.Column;
            DC.DPH.AddParameter(DC.EH.FuncMFExpression(propertyFunc)[0]);
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.AllAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderSingleColumnAsync(propertyFunc.Compile());
        }
    }
}
