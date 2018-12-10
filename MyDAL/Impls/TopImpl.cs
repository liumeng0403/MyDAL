using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Extensions;
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
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<VM>> TopAsync<VM>(int count)
            where VM : class
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            SelectMHandle<M, VM>();
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }

        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<M, T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return await DC.DS.ExecuteReaderSingleColumnAsync(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return await DC.DS.ExecuteReaderMultiRowAsync<T>();
            }
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
            DC.PageIndex = 0;
            DC.PageSize = count;
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return await DC.DS.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return await DC.DS.ExecuteReaderMultiRowAsync<T>();
            }
        }
    }
}
