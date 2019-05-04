using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace MyDAL.Impls.ImplSyncs
{
    internal sealed class QueryOneImpl<M>
        : Impler
        , IQueryOne<M>
        where M : class
    {
        internal QueryOneImpl(Context dc)
            : base(dc)
        { }

        public M QueryOne()
        {
            return new TopImpl<M>(DC).Top(1).FirstOrDefault();
        }
        public VM QueryOne<VM>()
            where VM : class
        {
            return new TopImpl<M>(DC).Top<VM>(1).FirstOrDefault();
        }
        public T QueryOne<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return new TopImpl<M>(DC).Top(1, columnMapFunc).FirstOrDefault();
        }
    }

    internal sealed class QueryOneXImpl
        : Impler
        , IQueryOneX
    {
        internal QueryOneXImpl(Context dc)
            : base(dc)
        { }

        public M QueryOne<M>()
            where M : class
        {
            return new TopXImpl(DC).Top<M>(1).FirstOrDefault();
        }
        public T QueryOne<T>(Expression<Func<T>> columnMapFunc)
        {
            return new TopXImpl(DC).Top(1, columnMapFunc).FirstOrDefault();
        }
    }

    internal sealed class QueryOneSQLImpl
        : ImplerSync
        , IQueryOneSQL
    {
        public QueryOneSQLImpl(Context dc)
            : base(dc)
        { }

        public T QueryOne<T>()
        {
            DC.Method = UiMethodEnum.QueryOneAsync;
            if (typeof(T).IsSingleColumn())
            {
                return DSS.ExecuteReaderSingleColumn<T>().FirstOrDefault();
            }
            else
            {
                return DSS.ExecuteReaderMultiRow<T>().FirstOrDefault();
            }
        }
    }
}
