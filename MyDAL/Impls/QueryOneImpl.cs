using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Impls.Base;
using MyDAL.Interfaces;
using System;
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
        {     }

        public async Task<M> QueryOneAsync()
        {
            return (await new TopAsyncImpl<M>(DC).TopAsync(1)).FirstOrDefault();
        }
        public async Task<VM> QueryOneAsync<VM>()
            where VM : class
        {
            return (await new TopAsyncImpl<M>(DC).TopAsync<VM>(1)).FirstOrDefault();
        }
        public async Task<T> QueryOneAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return (await new TopAsyncImpl<M>(DC).TopAsync(1, columnMapFunc)).FirstOrDefault();
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

    internal sealed class QueryOneXAsyncImpl
    : Impler
    , IQueryOneXAsync
    {
        internal QueryOneXAsyncImpl(Context dc)
            : base(dc)
        {    }

        public async Task<M> QueryOneAsync<M>()
            where M : class
        {
            return (await new TopXAsyncImpl(DC).TopAsync<M>(1)).FirstOrDefault();
        }
        public async Task<T> QueryOneAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return (await new TopXAsyncImpl(DC).TopAsync(1, columnMapFunc)).FirstOrDefault();
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

    internal sealed class QueryOneSQLAsyncImpl
    : ImplerAsync
    , IQueryOneSQLAsync 
    {
        public QueryOneSQLAsyncImpl(Context dc)
            : base(dc)
        { }

        public async Task<T> QueryOneAsync<T>()
        {
            DC.Method = UiMethodEnum.QueryOneAsync;
            if (typeof(T).IsSingleColumn())
            {
                return (await DSA.ExecuteReaderSingleColumnAsync<T>()).FirstOrDefault();
            }
            else
            {
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
