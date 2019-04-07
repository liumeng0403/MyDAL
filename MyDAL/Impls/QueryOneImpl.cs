using MyDAL.Core.Bases;
using MyDAL.Core.Enums;
using MyDAL.Core.Extensions;
using MyDAL.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal sealed class QueryOneImpl<M>
        : Impler
        , IQueryOneAsync<M>, IQueryOne<M>
        where M : class
    {
        internal QueryOneImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<M> QueryOneAsync()
        {
            return (await new TopImpl<M>(DC).TopAsync(1)).FirstOrDefault();
        }
        public async Task<VM> QueryOneAsync<VM>()
            where VM : class
        {
            return (await new TopImpl<M>(DC).TopAsync<VM>(1)).FirstOrDefault();
        }
        public async Task<T> QueryOneAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return (await new TopImpl<M>(DC).TopAsync(1, columnMapFunc)).FirstOrDefault();
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

    internal sealed class QueryOneXImpl
        : Impler
        , IQueryOneXAsync, IQueryOneX
    {
        internal QueryOneXImpl(Context dc)
            : base(dc)
        {
        }

        public async Task<M> QueryOneAsync<M>()
            where M : class
        {
            return (await new TopXImpl(DC).TopAsync<M>(1)).FirstOrDefault();
        }
        public async Task<T> QueryOneAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return (await new TopXImpl(DC).TopAsync(1, columnMapFunc)).FirstOrDefault();
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

    internal sealed class QueryOneSQLImpl
        : Impler
        , IQueryOneSQLAsync, IQueryOneSQL
    {
        public QueryOneSQLImpl(Context dc)
            : base(dc)
        { }

        public async Task<T> QueryOneAsync<T>()
        {
            DC.Method = UiMethodEnum.QueryOneAsync;
            if (typeof(T).IsSingleColumn())
            {
                return (await DC.DSA.ExecuteReaderSingleColumnAsync<T>()).FirstOrDefault();
            }
            else
            {
                return (await DC.DSA.ExecuteReaderMultiRowAsync<T>()).FirstOrDefault();
            }
        }

        public T QueryOne<T>()
        {
            DC.Method = UiMethodEnum.QueryOneAsync;
            if (typeof(T).IsSingleColumn())
            {
                return DC.DSS.ExecuteReaderSingleColumn<T>().FirstOrDefault();
            }
            else
            {
                return DC.DSS.ExecuteReaderMultiRow<T>().FirstOrDefault();
            }
        }
    }
}
