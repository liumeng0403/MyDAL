using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Core.Enums;
using Yunyong.DataExchange.Core.Extensions;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.Impls
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
                return default(List<T>);
            }
        }
    }
}
