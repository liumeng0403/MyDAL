using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Enums;
using HPC.DAL.Core.Extensions;
using HPC.DAL.Impls.Base;
using HPC.DAL.Interfaces.ISyncs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace HPC.DAL.Impls.ImplSyncs
{
    internal sealed class TopImpl<M>
        : ImplerSync
        , ITop<M>
        where M : class
    {
        internal TopImpl(Context dc)
            : base(dc)
        { }

        public List<M> Top(int count)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<VM> Top<VM>(int count)
            where VM : class
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return DSS.ExecuteReaderMultiRow<VM>();
        }
        public List<T> Top<T>(int count, Expression<Func<M, T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DSS.ExecuteReaderSingleColumn(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DSS.ExecuteReaderMultiRow<T>();
            }
        }

    }

    internal sealed class TopXImpl
        : ImplerSync
        , ITopX
    {
        public TopXImpl(Context dc)
            : base(dc)
        { }

        public List<M> Top<M>(int count)
            where M : class
        {
            SelectMHandle<M>();
            DC.PageIndex = 0;
            DC.PageSize = count;
            PreExecuteHandle(UiMethodEnum.TopAsync);
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<T> Top<T>(int count, Expression<Func<T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DSS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.TopAsync);
                return DSS.ExecuteReaderMultiRow<T>();
            }
        }
    }
}
