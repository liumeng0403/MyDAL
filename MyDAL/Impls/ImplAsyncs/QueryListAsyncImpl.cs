using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Core.Extensions;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.IAsyncs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Impls.ImplAsyncs
{
    internal sealed class QueryListAsyncImpl<M>
        : ImplerAsync
        , IQueryListAsync<M>
    where M : class
    {
        internal QueryListAsyncImpl(Context dc)
            : base(dc) { }

        public async Task<List<M>> QueryListAsync()
        {
            PreExecuteHandle(UiMethodEnum.QueryList);
            return await DSA.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<VM>> QueryListAsync<VM>()
            where VM : class
        {
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.QueryList);
            return await DSA.ExecuteReaderMultiRowAsync<VM>();
        }
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryList);
                return await DSA.ExecuteReaderSingleColumnAsync(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryList);
                return await DSA.ExecuteReaderMultiRowAsync<T>();
            }
        }

    }

    internal sealed class QueryListXAsyncImpl
        : ImplerAsync
        , IQueryListXAsync
    {
        internal QueryListXAsyncImpl(Context dc)
            : base(dc) { }

        public async Task<List<M>> QueryListAsync<M>()
            where M : class
        {
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryList);
            return await DSA.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryList);
                return await DSA.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryList);
                return await DSA.ExecuteReaderMultiRowAsync<T>();
            }
        }

    }

    internal sealed class QueryListSQLAsyncImpl
        : ImplerAsync
        , IQueryListSQLAsync
    {
        public QueryListSQLAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<List<T>> QueryListAsync<T>()
        {
            DC.Method = UiMethodEnum.QueryList;
            if (typeof(T).IsSingleColumn())
            {
                return await DSA.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                return await DSA.ExecuteReaderMultiRowAsync<T>();
            }
        }

    }
}
