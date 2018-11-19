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
    internal class ListImpl<M>
        : Impler, Interfaces.IList<M>
        where M : class
    {
        internal ListImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> ListAsync()
        {
            DC.Method = UiMethodEnum.ListAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<VM>> ListAsync<VM>()
            where VM:class
        {
            SelectMHandle<M, VM>();
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.ListAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }

        public async Task<List<VM>> ListAsync<VM>(Expression<Func<M, VM>> columnMapFunc)
            where VM:class
        {
            SelectMHandle(columnMapFunc);
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.ListAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }

        public async Task<List<M>> ListAsync(int topCount)
        {
            return await new TopImpl<M>(DC).TopAsync(topCount);
        }

        public async Task<List<VM>> ListAsync<VM>(int topCount) 
            where VM : class
        {
            return await new TopImpl<M>(DC).TopAsync<VM>(topCount);
        }

        public async Task<List<VM>> ListAsync<VM>(int topCount, Expression<Func<M, VM>> columnMapFunc) 
            where VM : class
        {
            return await new TopImpl<M>(DC).TopAsync<VM>(topCount, columnMapFunc);
        }
    }

    internal class ListXImpl
        : Impler, IListX
    {
        internal ListXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> ListAsync<M>()
            where M:class
        {
            SelectMHandle<M>();
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.JoinListAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<VM>> ListAsync<VM>(Expression<Func<VM>> columnMapFunc)
            where VM:class
        {
            SelectMHandle(columnMapFunc);
            DC.DPH.SetParameter();
            DC.Method = UiMethodEnum.JoinListAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }

        public async Task<List<M>> ListAsync<M>(int topCount) 
            where M : class
        {
            return await new TopXImpl(DC).TopAsync<M>(topCount);
        }

        public async Task<List<VM>> ListAsync<VM>(int topCount, Expression<Func<VM>> columnMapFunc) 
            where VM : class
        {
            return await new TopXImpl(DC).TopAsync<VM>(topCount, columnMapFunc);
        }
    }
}
