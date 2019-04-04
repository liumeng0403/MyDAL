using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class QueryListImpl<M>
        : Impler
        , IQueryList<M>, IQueryListSync<M>
        where M : class
    {
        internal QueryListImpl(Context dc)
            : base(dc) { }

        public async Task<List<M>> QueryListAsync()
        {
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<VM>> QueryListAsync<VM>()
            where VM : class
        {
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                return await DC.DS.ExecuteReaderSingleColumnAsync(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                return await DC.DS.ExecuteReaderMultiRowAsync<T>();
            }
        }

        public List<M> QueryList()
        {
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            return DC.DS.ExecuteReaderMultiRow<M>();
        }
        public List<VM> QueryList<VM>()
            where VM : class
        {
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            return DC.DS.ExecuteReaderMultiRow<VM>();
        }
        public List<T> QueryList<T>(Expression<Func<M, T>> columnMapFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                return DC.DS.ExecuteReaderSingleColumn(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                return DC.DS.ExecuteReaderMultiRow<T>();
            }
        }
    }

    internal class QueryListXImpl
        : Impler
        , IQueryListX, IQueryListXSync
    {
        internal QueryListXImpl(Context dc)
            : base(dc) { }

        public async Task<List<M>> QueryListAsync<M>()
            where M : class
        {
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                return await DC.DS.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                return await DC.DS.ExecuteReaderMultiRowAsync<T>();
            }
        }

        public List<M> QueryList<M>()
            where M : class
        {
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            return DC.DS.ExecuteReaderMultiRow<M>();
        }
        public List<T> QueryList<T>(Expression<Func<T>> columnMapFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                return DC.DS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                return DC.DS.ExecuteReaderMultiRow<T>();
            }
        }
    }

    internal class QueryListSQLImpl
        : Impler
        , IQueryListSQL, IQueryListSQLSync
    {
        public QueryListSQLImpl(Context dc)
            : base(dc)
        { }

        public async Task<List<T>> QueryListAsync<T>()
        {
            DC.Method = UiMethodEnum.QueryListAsync;
            if (typeof(T).IsSingleColumn())
            {
                return await DC.DS.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                return await DC.DS.ExecuteReaderMultiRowAsync<T>();
            }
        }

        public List<T> QueryList<T>()
        {
            DC.Method = UiMethodEnum.QueryListAsync;
            if (typeof(T).IsSingleColumn())
            {
                return DC.DS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                return DC.DS.ExecuteReaderMultiRow<T>();
            }
        }
    }
}
