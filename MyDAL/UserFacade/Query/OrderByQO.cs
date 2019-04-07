using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Query
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class OrderByQO<M>
        : Operator
        , IQueryPagingOAsync<M>, IQueryPagingO<M>
        where M : class
    {
        internal OrderByQO(Context dc)
            : base(dc) { }

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

        /// <summary>
        /// 单表分页查询
        /// </summary>
        public PagingResult<M> QueryPaging()
        {
            return new QueryPagingOImpl<M>(DC).QueryPaging();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public PagingResult<VM> QueryPaging<VM>()
            where VM : class
        {
            return new QueryPagingOImpl<M>(DC).QueryPaging<VM>();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public PagingResult<T> QueryPaging<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return new QueryPagingOImpl<M>(DC).QueryPaging(columnMapFunc);
        }
    }
}
