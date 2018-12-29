using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class QueryAllImpl<M>
        : Impler, IQueryAll<M>
        where M : class
    {
        internal QueryAllImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> QueryAllAsync()
        {
            PreExecuteHandle(UiMethodEnum.AllAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<VM>> QueryAllAsync<VM>()
            where VM : class
        {
            PreExecuteHandle(UiMethodEnum.AllAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }

        public async Task<List<T>> QueryAllAsync<T>(Expression<Func<M, T>> propertyFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(propertyFunc);
                PreExecuteHandle(UiMethodEnum.AllAsync);
                return await DC.DS.ExecuteReaderSingleColumnAsync(propertyFunc.Compile());
            }
            else
            {
                SelectMHandle(propertyFunc);
                PreExecuteHandle(UiMethodEnum.AllAsync);
                return await DC.DS.ExecuteReaderMultiRowAsync<T>();
            }
        }
    }

    internal class QueryAllXImpl
        : Impler, IQueryAllX
    {
        public QueryAllXImpl(Context dc) 
            : base(dc)
        {
        }

        public async Task<List<M>> QueryAllAsync<M>() where M : class
        {
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.AllAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<T>> QueryAllAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.AllAsync);
                return await DC.DS.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.AllAsync);
                return await DC.DS.ExecuteReaderMultiRowAsync<T>();
            }
        }
    }
}
