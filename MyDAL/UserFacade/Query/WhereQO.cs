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
    public sealed class WhereQO<M>
        : Operator, IPagingListO<M>
        where M : class
    {
        internal WhereQO(Context dc)
            : base(dc) { }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingList<M>> PagingListAsync()
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingList<VM>> PagingListAsync<VM>()
            where VM : class
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync<VM>();
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingList<T>> PagingListAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync(columnMapFunc);
        }

    }
}
