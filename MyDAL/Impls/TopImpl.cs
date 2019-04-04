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
    internal class TopImpl<M>
        : Impler
        , ITop<M>, ITopSync<M>
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
            SelectMQ<M, VM>();
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

        public List<M> Top(int count)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return DC.DS.ExecuteReaderMultiRow<M>();
        }
        public List<VM> Top<VM>(int count) 
            where VM : class
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return DC.DS.ExecuteReaderMultiRow<VM>();
        }
        public List<T> Top<T>(int count, Expression<Func<M, T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DC.DS.ExecuteReaderSingleColumn(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DC.DS.ExecuteReaderMultiRow<T>();
            }
        }

    }

    internal class TopXImpl
        : Impler
        , ITopX, ITopXSync
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

        public List<M> Top<M>(int count)
            where M : class
        {
            SelectMHandle<M>();
            DC.PageIndex = 0;
            DC.PageSize = count;
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return DC.DS.ExecuteReaderMultiRow<M>();
        }
        public List<T> Top<T>(int count, Expression<Func<T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DC.DS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DC.DS.ExecuteReaderMultiRow<T>();
            }
        }
    }
}
