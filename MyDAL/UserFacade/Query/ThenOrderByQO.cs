using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Query
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="M"></typeparam>
    public sealed class ThenOrderByQO<M>
        : Operator, IQueryPagingO<M>
        where M : class
    {
        internal ThenOrderByQO(Context dc)
            : base(dc)
        { }


        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<M>> QueryPagingAsync()
        {
            return await new QueryPagingOImpl<M>(DC).QueryPagingAsync();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<VM>> QueryPagingAsync<VM>()
            where VM : class
        {
            return await new QueryPagingOImpl<M>(DC).QueryPagingAsync<VM>();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<T>> QueryPagingAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new QueryPagingOImpl<M>(DC).QueryPagingAsync(columnMapFunc);
        }
    }
}
