using HPC.DAL.AdoNet;
using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HPC.DAL.Impls.Base
{
    internal abstract class ImplerSync
                : Impler
    {
        protected ImplerSync(Context dc)
            : base(dc)
        {
            DSS = new DataSourceSync(dc);
        }

        internal DataSourceSync DSS { get; private set; }

        protected PagingResult<T> PagingListAsyncHandleSync<T>(UiMethodEnum sqlType, bool single)
        {
            PreExecuteHandle(sqlType);
            return DSS.ExecuteReaderPaging<None, T>(single, null);
        }
        protected PagingResult<T> PagingListAsyncHandleSync<M, T>(UiMethodEnum sqlType, bool single, Func<M, T> mapFunc)
           where M : class
        {
            PreExecuteHandle(sqlType);
            return DSS.ExecuteReaderPaging(single, mapFunc);
        }
    }
}
