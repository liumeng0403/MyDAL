using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Impls.Base;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MyDAL.Impls.ImplSyncs
{
    internal sealed class SelectOneImpl<M>
        : Impler
        , ISelectOne<M>
        where M : class
    {
        internal SelectOneImpl(Context dc)
            : base(dc)
        { }

        public M SelectOne()
        {
            return new TopImpl<M>(DC).Top(1).FirstOrDefault();
        }
        public VM SelectOne<VM>()
            where VM : class
        {
            return new TopImpl<M>(DC).Top<VM>(1).FirstOrDefault();
        }
        public T SelectOne<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return new TopImpl<M>(DC).Top(1, columnMapFunc).FirstOrDefault();
        }
    }

    internal sealed class SelectOneXImpl
        : Impler
        , ISelectOneX
    {
        internal SelectOneXImpl(Context dc)
            : base(dc)
        { }

        public M SelectOne<M>()
            where M : class
        {
            return new TopXImpl(DC).Top<M>(1).FirstOrDefault();
        }
        public T SelectOne<T>(Expression<Func<T>> columnMapFunc)
        {
            return new TopXImpl(DC).Top(1, columnMapFunc).FirstOrDefault();
        }
    }

    internal sealed class SelectOneSQLImpl
        : ImplerSync
        , ISelectOneSQL
    {
        public SelectOneSQLImpl(Context dc)
            : base(dc)
        { }

        public T SelectOne<T>()
        {
            DC.Method = UiMethodEnum.QueryOne;
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
