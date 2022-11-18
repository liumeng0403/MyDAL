using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers.Base;

namespace MyDAL.Impls.Implers
{
    internal sealed class SelectListImpl<M>
        : ImplerBase
        , ISelectList<M>
        where M : class
    {
        internal SelectListImpl(Context dc)
            : base(dc) { }

        public List<M> SelectList()
        {
            PreExecuteHandle(UiMethodEnum.QueryList);
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<VM> SelectList<VM>()
            where VM : class
        {
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.QueryList);
            return DSS.ExecuteReaderMultiRow<VM>();
        }
        public List<T> SelectList<T>(Expression<Func<M, T>> columnMapFunc)
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

    internal sealed class SelectListXImpl
        : ImplerBase
        , ISelectListX
    {
        internal SelectListXImpl(Context dc)
            : base(dc) { }

        public List<M> SelectList<M>()
            where M : class
        {
            SelectMHandle<M>();
            PreExecuteHandle(UiMethodEnum.QueryList);
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<T> SelectList<T>(Expression<Func<T>> columnMapFunc)
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

    internal sealed class SelectListSQLImpl
        : ImplerBase
        , ISelectListSQL
    {
        public SelectListSQLImpl(Context dc)
            : base(dc)
        { }

        public List<T> SelectList<T>()
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
