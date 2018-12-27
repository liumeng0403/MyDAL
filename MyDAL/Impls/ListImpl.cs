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
    internal class ListImpl<M>
        : Impler, Interfaces.IList<M>
        where M : class
    {
        internal ListImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> ListAsync()
        {
            PreExecuteHandle(UiMethodEnum.ListAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<VM>> ListAsync<VM>()
            where VM:class
        {
            SelectMHandle<M, VM>();
            PreExecuteHandle(UiMethodEnum.ListAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }

        public async Task<List<T>> ListAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.ListAsync);
                return await DC.DS.ExecuteReaderSingleColumnAsync(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.ListAsync);
                return await DC.DS.ExecuteReaderMultiRowAsync<T>();
            }
        }
    }

    internal class ListXImpl
        : Impler, IListX
    {
        internal ListXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> ListAsync<M>()
            where M:class
        {
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.ListAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<T>> ListAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.ListAsync);
                return await DC.DS.ExecuteReaderSingleColumnAsync<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.ListAsync);
                return await DC.DS.ExecuteReaderMultiRowAsync<T>();
            }
        }
    }
}
