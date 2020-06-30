using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace MyDAL.Impls.ImplSyncs
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
            PreExecuteHandle(UiMethodEnum.Top);
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<VM> Top<VM>(int count)
            where VM : class
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            SelectMQ<M, VM>();
            PreExecuteHandle(UiMethodEnum.Top);
            return DSS.ExecuteReaderMultiRow<VM>();
        }
        public List<T> Top<T>(int count, Expression<Func<M, T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.Top);
                return DSS.ExecuteReaderSingleColumn(columnMapFunc.Compile());
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.Top);
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
            PreExecuteHandle(UiMethodEnum.Top);
            return DSS.ExecuteReaderMultiRow<M>();
        }
        public List<T> Top<T>(int count, Expression<Func<T>> columnMapFunc)
        {
            DC.PageIndex = 0;
            DC.PageSize = count;
            if (typeof(T).IsSingleColumn())
            {
                SingleColumnHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.Top);
                return DSS.ExecuteReaderSingleColumn<T>();
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.Top);
                return DSS.ExecuteReaderMultiRow<T>();
            }
        }
    }
}
