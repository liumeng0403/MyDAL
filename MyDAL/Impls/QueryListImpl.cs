using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Core.Extensions;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.Impls
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
    internal sealed class QueryListImpl<M>
        : ImplerSync
        , IQueryList<M>
        where M : class
    {
        internal QueryListImpl(Context dc)
            : base(dc) { }
         
        public List<M> QueryList(IDbTransaction tran = null)
        {
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            DSS.Tran = tran;
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<VM> QueryList<VM>(IDbTransaction tran = null)
            where VM : class
        {
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            DSS.Tran = tran;
            return DSS.ExecuteReaderMultiRow<VM>();
        }
        public List<T> QueryList<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSS.Tran = tran;
                return DSS.ExecuteReaderSingleColumn(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSS.Tran = tran;
                return DSS.ExecuteReaderMultiRow<T>();
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
    internal sealed class QueryListXImpl
        : ImplerSync
        , IQueryListX
    {
        internal QueryListXImpl(Context dc)
            : base(dc) { }
         
        public List<M> QueryList<M>(IDbTransaction tran = null)
            where M : class
        {
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            DSS.Tran = tran;
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<T> QueryList<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSS.Tran = tran;
                return DSS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSS.Tran = tran;
                return DSS.ExecuteReaderMultiRow<T>();
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
    internal sealed class QueryListSQLImpl
        : ImplerSync
        , IQueryListSQL
    {
        public QueryListSQLImpl(Context dc)
            : base(dc)
        { }
         
        public List<T> QueryList<T>(IDbTransaction tran = null)
        {
            DC.Method = UiMethodEnum.QueryListAsync;
            if (typeof(T).IsSingleColumn())
            {
                DSS.Tran = tran;
                return DSS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                DSS.Tran = tran;
                return DSS.ExecuteReaderMultiRow<T>();
            }
        }
    }
}
