using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using MyDAL.Interfaces.IAsyncs;
using MyDAL.Interfaces.ISyncs;
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class QueryOneAsyncImpl<M>
     : Impler
     , IQueryOneAsync<M>
     where M : class
    {
        internal QueryOneAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<M> QueryOneAsync(IDbTransaction tran = null)
        {
            return (await new TopAsyncImpl<M>(DC).TopAsync(1,tran)).FirstOrDefault();
        }
        public async Task<VM> QueryOneAsync<VM>(IDbTransaction tran = null)
            where VM : class
        {
            return (await new TopAsyncImpl<M>(DC).TopAsync<VM>(1,tran)).FirstOrDefault();
        }
        public async Task<T> QueryOneAsync<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
        {
            return (await new TopAsyncImpl<M>(DC).TopAsync(1, columnMapFunc,tran)).FirstOrDefault();
        }

    }
    internal sealed class QueryOneImpl<M>
        : Impler
        , IQueryOne<M>
        where M : class
    {
        internal QueryOneImpl(Context dc)
            : base(dc)
        {
        }

        public M QueryOne(IDbTransaction tran = null)
        {
            return new TopImpl<M>(DC).Top(1,tran).FirstOrDefault();
        }
        public VM QueryOne<VM>(IDbTransaction tran = null)
            where VM : class
        {
            return new TopImpl<M>(DC).Top<VM>(1,tran).FirstOrDefault();
        }
        public T QueryOne<T>(Expression<Func<M, T>> columnMapFunc, IDbTransaction tran = null)
        {
            return new TopImpl<M>(DC).Top(1, columnMapFunc,tran).FirstOrDefault();
        }
    }

    internal sealed class QueryOneXAsyncImpl
    : Impler
    , IQueryOneXAsync
    {
        internal QueryOneXAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<M> QueryOneAsync<M>(IDbTransaction tran = null)
            where M : class
        {
            return (await new TopXAsyncImpl(DC).TopAsync<M>(1,tran)).FirstOrDefault();
        }
        public async Task<T> QueryOneAsync<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            return (await new TopXAsyncImpl(DC).TopAsync(1, columnMapFunc,tran)).FirstOrDefault();
        }

    }
    internal sealed class QueryOneXImpl
        : Impler
        , IQueryOneX
    {
        internal QueryOneXImpl(Context dc)
            : base(dc)
        {
        }

        public M QueryOne<M>(IDbTransaction tran = null)
            where M : class
        {
            return new TopXImpl(DC).Top<M>(1,tran).FirstOrDefault();
        }
        public T QueryOne<T>(Expression<Func<T>> columnMapFunc, IDbTransaction tran = null)
        {
            return new TopXImpl(DC).Top(1, columnMapFunc,tran).FirstOrDefault();
        }
    }

    internal sealed class QueryOneSQLAsyncImpl
    : ImplerAsync
    , IQueryOneSQLAsync
    {
        public QueryOneSQLAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<T> QueryOneAsync<T>(IDbTransaction tran = null)
        {
            DC.Method = UiMethodEnum.QueryOneAsync;
            if (typeof(T).IsSingleColumn())
            {
                DSA.Tran = tran;
                return (await DSA.ExecuteReaderSingleColumnAsync<T>()).FirstOrDefault();
            }
            else
            {
                DSA.Tran = tran;
                return (await DSA.ExecuteReaderMultiRowAsync<T>()).FirstOrDefault();
            }
        }

    }
    internal sealed class QueryOneSQLImpl
        : ImplerSync
        , IQueryOneSQL
    {
        public QueryOneSQLImpl(Context dc)
            : base(dc)
        { }

        public T QueryOne<T>(IDbTransaction tran = null)
        {
            DC.Method = UiMethodEnum.QueryOneAsync;
            if (typeof(T).IsSingleColumn())
            {
                DSS.Tran = tran;
                return DSS.ExecuteReaderSingleColumn<T>().FirstOrDefault();
            }
            else
            {
                DSS.Tran = tran;
                return DSS.ExecuteReaderMultiRow<T>().FirstOrDefault();
            }
        }
    }
}
