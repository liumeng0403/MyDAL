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
    internal class TopImpl<M>
        : Impler, ITop<M>
        where M : class
    {
        internal TopImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> TopAsync(int count)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            DC.Method = UiMethodEnum.TopAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<VM>> TopAsync<VM>(int count)
            where VM : class
        {
            SelectMHandle<M, VM>();
            DC.DPH.SetParameter();
            DC.PageIndex = 0;
            DC.PageSize = count;
            DC.Method = UiMethodEnum.TopAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }

        public async Task<List<VM>> TopAsync<VM>(int count, Expression<Func<M, VM>> columnMapFunc)
            where VM : class
        {
            SelectMHandle(columnMapFunc);
            DC.DPH.SetParameter();
            DC.PageIndex = 0;
            DC.PageSize = count;
            DC.Method = UiMethodEnum.TopAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }
    }

    internal class TopXImpl
        : Impler, ITopX
    {
        public TopXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> TopAsync<M>(int count)
            where M : class
        {
            SelectMHandle<M>();
            DC.DPH.SetParameter();
            DC.PageIndex = 0;
            DC.PageSize = count;
            DC.Method = UiMethodEnum.JoinTopAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<VM>> TopAsync<VM>(int count,Expression<Func<VM>> columnMapFunc)
            where VM : class
        {
            SelectMHandle(columnMapFunc);
            DC.DPH.SetParameter();
            DC.PageIndex = 0;
            DC.PageSize = count;
            DC.Method = UiMethodEnum.JoinTopAsync;
            DC.SqlProvider.GetSQL();
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>(
                );
        }
    }
}
