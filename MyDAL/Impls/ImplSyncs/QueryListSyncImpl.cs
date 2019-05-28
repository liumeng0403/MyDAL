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
    internal sealed class QueryListImpl<M>
        : ImplerSync
        , IQueryList<M>
        where M : class
    {
        internal QueryListImpl(Context dc)
            : base(dc) { }

        public List<M> QueryList()
        {
            PreExecuteHandle(UiMethodEnum.QueryList);
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<VM> QueryList<VM>()
            where VM : class
        {
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.QueryList);
            return DSS.ExecuteReaderMultiRow<VM>();
        }
        public List<T> QueryList<T>(Expression<Func<M, T>> columnMapFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryList);
                return DSS.ExecuteReaderSingleColumn(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryList);
                return DSS.ExecuteReaderMultiRow<T>();
            }
        }
    }

    internal sealed class QueryListXImpl
        : ImplerSync
        , IQueryListX
    {
        internal QueryListXImpl(Context dc)
            : base(dc) { }

        public List<M> QueryList<M>()
            where M : class
        {
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryList);
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<T> QueryList<T>(Expression<Func<T>> columnMapFunc)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryList);
                return DSS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryList);
                return DSS.ExecuteReaderMultiRow<T>();
            }
        }
    }

    internal sealed class QueryListSQLImpl
        : ImplerSync
        , IQueryListSQL
    {
        public QueryListSQLImpl(Context dc)
            : base(dc)
        { }

        public List<T> QueryList<T>()
        {
            DC.Method = UiMethodEnum.QueryList;
            if (typeof(T).IsSingleColumn())
            {
                return DSS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                return DSS.ExecuteReaderMultiRow<T>();
            }
        }
    }
}
