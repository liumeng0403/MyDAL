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
    internal sealed class SelectListAsyncImpl<M>
        : ImplerAsync
        , ISelectListAsync<M>
    where M : class
    {
        internal SelectListAsyncImpl(Context dc)
            : base(dc) { }

        public async Task<List<M>> SelectListAsync()
        {
            PreExecuteHandle(UiMethodEnum.QueryList);
            return await DSA.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<VM>> SelectListAsync<VM>()
            where VM : class
        {
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.QueryList);
            return await DSA.ExecuteReaderMultiRowAsync<VM>();
        }
        public async Task<List<T>> SelectListAsync<T>(Expression<Func<M, T>> columnMapFunc)
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

    internal sealed class SelectListXAsyncImpl
        : ImplerAsync
        , ISelectListXAsync
    {
        internal SelectListXAsyncImpl(Context dc)
            : base(dc) { }

        public async Task<List<M>> SelectListAsync<M>()
            where M : class
        {
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryList);
            return await DSA.ExecuteReaderMultiRowAsync<M>();
        }
        public async Task<List<T>> SelectListAsync<T>(Expression<Func<T>> columnMapFunc)
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

    internal sealed class SelectListSQLAsyncImpl
        : ImplerAsync
        , ISelectListSQLAsync
    {
        public SelectListSQLAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<List<T>> SelectListAsync<T>()
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
