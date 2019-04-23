using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls.ImplAsyncs
{
    internal sealed class QueryListAsyncImpl<M>
    : ImplerAsync
    , IQueryListAsync<M>
    where M : class
    {
        internal QueryListAsyncImpl(Context dc)
            : base(dc) { }

        public async Task<List<M>> QueryListAsync(IDbTransaction tran = null)
        {
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<VM>> QueryListAsync<VM>(IDbTransaction tran = null)
            where VM : class
        {
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteReaderMultiRowAsync<VM>();
        }
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSA.Tran = tran;
                return await DSA.ExecuteReaderSingleColumnAsync(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSA.Tran = tran;
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

        public async Task<List<M>> QueryListAsync<M>(IDbTransaction tran = null)
            where M : class
        {
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            DSA.Tran = tran;
            return await DSA.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSA.Tran = tran;
                return await DSA.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSA.Tran = tran;
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

        public async Task<List<T>> QueryListAsync<T>(IDbTransaction tran = null)
        {
            DC.Method = UiMethodEnum.QueryListAsync;
            if (typeof(T).IsSingleColumn())
            {
                DSA.Tran = tran;
                return await DSA.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                DSA.Tran = tran;
                return await DSA.ExecuteReaderMultiRowAsync<T>();
            }
        }

    }
}
