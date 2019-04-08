using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class TopAsyncImpl<M>
    : ImplerAsync
    , ITopAsync<M>
    where M : class
    {
        internal TopAsyncImpl(Context dc)
            : base(dc)
        {     }

        public async Task<List<M>> TopAsync(int count)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return await DSA.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<VM>> TopAsync<VM>(int count)
            where VM : class
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return await DSA.ExecuteReaderMultiRowAsync<VM>();
        }
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<M, T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return await DSA.ExecuteReaderSingleColumnAsync(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return await DSA.ExecuteReaderMultiRowAsync<T>();
            }
        }

    }
    internal sealed class TopImpl<M>
        : ImplerSync
        , ITop<M>
        where M : class
    {
        internal TopImpl(Context dc)
            : base(dc)
        {
        }

        public List<M> Top(int count)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<VM> Top<VM>(int count) 
            where VM : class
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return DSS.ExecuteReaderMultiRow<VM>();
        }
        public List<T> Top<T>(int count, Expression<Func<M, T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DSS.ExecuteReaderSingleColumn(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DSS.ExecuteReaderMultiRow<T>();
            }
        }

    }

    internal sealed class TopXAsyncImpl
    : ImplerAsync
    , ITopXAsync
    {
        public TopXAsyncImpl(Context dc)
            : base(dc)
        {    }

        public async Task<List<M>> TopAsync<M>(int count)
            where M : class
        {
            SelectMHandle<M>();
            DC.PageIndex = 0;
            DC.PageSize = count;
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return await DSA.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return await DSA.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return await DSA.ExecuteReaderMultiRowAsync<T>();
            }
        }

    }
    internal sealed class TopXImpl
        : ImplerSync
        , ITopX
    {
        public TopXImpl(Context dc)
            : base(dc)
        {
        }

        public List<M> Top<M>(int count)
            where M : class
        {
            SelectMHandle<M>();
            DC.PageIndex = 0;
            DC.PageSize = count;
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<T> Top<T>(int count, Expression<Func<T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DSS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DSS.ExecuteReaderMultiRow<T>();
            }
        }
    }
}
