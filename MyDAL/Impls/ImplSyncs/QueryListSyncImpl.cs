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

        public List<M> QueryList(IDbTransaction tran = null)
        {
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            DSS.Tran = tran;
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<VM> QueryList<VM>(IDbTransaction tran = null)
            where VM : class
        {
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            DSS.Tran = tran;
            return DSS.ExecuteReaderMultiRow<VM>();
        }
        public List<T> QueryList<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSS.Tran = tran;
                return DSS.ExecuteReaderSingleColumn(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSS.Tran = tran;
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

        public List<M> QueryList<M>(IDbTransaction tran = null)
            where M : class
        {
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryListAsync);
            DSS.Tran = tran;
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<T> QueryList<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSS.Tran = tran;
                return DSS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.QueryListAsync);
                DSS.Tran = tran;
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

        public List<T> QueryList<T>(IDbTransaction tran = null)
        {
            DC.Method = UiMethodEnum.QueryListAsync;
            if (typeof(T).IsSingleColumn())
            {
                DSS.Tran = tran;
                return DSS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                DSS.Tran = tran;
                return DSS.ExecuteReaderMultiRow<T>();
            }
        }
    }
}
