using HPC.DAL.Core.Bases;
using HPC.DAL.Impls;
using HPC.DAL.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.UserFacade.Join
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class DistinctXO
        : Operator
        , IQueryPagingXOAsync, IQueryPagingXO
    {
        internal DistinctXO(Context dc)
            : base(dc)
        { }


        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingResult<M>> QueryPagingAsync<M>()
            where M : class
        {
            return await new PagingListXOAsyncImpl(DC).QueryPagingAsync<M>();
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingResult<T>> QueryPagingAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new PagingListXOAsyncImpl(DC).QueryPagingAsync(columnMapFunc);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        public PagingResult<M> QueryPaging<M>()
            where M : class
        {
            return new PagingListXOImpl(DC).QueryPaging<M>();
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        public PagingResult<T> QueryPaging<T>(Expression<Func<T>> columnMapFunc)
        {
            return new PagingListXOImpl(DC).QueryPaging(columnMapFunc);
        }
    }
}
