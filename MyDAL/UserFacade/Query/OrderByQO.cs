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
    public sealed class OrderByQO<M>
        : Operator, IPagingListO<M>
        where M : class
    {
        internal OrderByQO(Context dc)
            : base(dc) { }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<M>> PagingListAsync()
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<VM>> PagingListAsync<VM>()
            where VM : class
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync<VM>();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<T>> PagingListAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync(columnMapFunc);
        }
    }
}
