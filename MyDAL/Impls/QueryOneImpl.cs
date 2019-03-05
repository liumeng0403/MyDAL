using MyDAL.Core.Bases;
using MyDAL.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.Impls
{
    internal class QueryOneImpl<M>
        : Impler, IQueryOne<M>
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
    }

    internal class QueryOneXImpl
        : Impler, IQueryOneX
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
    }

    internal class QueryOneSQLImpl
        : Impler, IQueryOneSQL
    {
        public QueryOneSQLImpl(Context dc)
            : base(dc)
        { }

        public Task<T> QueryOneAsync<T>()
        {
            throw new NotImplementedException();
        }
    }
}
