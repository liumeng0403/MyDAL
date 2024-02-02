using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers.Base;

namespace MyDAL.Impls.Implers
{
    internal sealed class TopImpl<M>
        : ImplerBase
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
                if (是函数单值())
                {
                    return new List<T>()
                    {
                        DSS.ExecuteScalar<T>()
                    };
                }
                else
                {
                    return DSS.ExecuteReaderSingleColumn(columnMapFunc.Compile()); 
                }
            }
            else
            {
                SelectMHandle(columnMapFunc);
                PreExecuteHandle(UiMethodEnum.Top);
                return DSS.ExecuteReaderMultiRow<T>();
            }
        }

        /// <summary>
        /// 1-count(col)<br/>
        /// 2-
        /// </summary>
        private bool 是函数单值()
        {
            if (DC.Parameters.Any(it => it.Func.Equals(ColFuncEnum.Count))) // 1-count(col)
            {
                return true;
            }

            return false;
        }

    }

    internal sealed class TopXImpl
        : ImplerBase
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
