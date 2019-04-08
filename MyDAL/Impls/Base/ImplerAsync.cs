using MyDAL.AdoNet;
using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyDAL.Impls.Base
{
    internal abstract class ImplerAsync
        : Impler
    {
        protected ImplerAsync(Context dc)
            : base(dc)
        {
            DSA = new DataSourceAsync(dc);
        }

        internal DataSourceAsync DSA { get; private set; }

        protected async Task<PagingResult<T>> PagingListAsyncHandle<T>(UiMethodEnum sqlType, bool single)
        {
            PreExecuteHandle(sqlType);
            return await DSA.ExecuteReaderPagingAsync<None, T>(single, null);
        }
        protected async Task<PagingResult<T>> PagingListAsyncHandle<M, T>(UiMethodEnum sqlType, bool single, Func<M, T> mapFunc)
            where M : class
        {
            PreExecuteHandle(sqlType);
            return await DSA.ExecuteReaderPagingAsync(single, mapFunc);
        }
    }
}
