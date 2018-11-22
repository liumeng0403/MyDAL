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
    internal class AllImpl<M>
        : Impler, IAll<M>
        where M : class
    {
        internal AllImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<List<M>> AllAsync()
        {
            PreExecuteHandle(UiMethodEnum.AllAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<M>();
        }

        public async Task<List<VM>> AllAsync<VM>()
            where VM : class
        {
            PreExecuteHandle(UiMethodEnum.AllAsync);
            return await DC.DS.ExecuteReaderMultiRowAsync<VM>();
        }

        public async Task<List<T>> AllAsync<T>(Expression<Func<M, T>> propertyFunc)
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
}
