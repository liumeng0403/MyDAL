using MyDAL.Core.Bases;
using MyDAL.Impls;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Join
{
    public sealed class WhereXO
        : Operator
    {
        internal WhereXO(Context dc)
            : base(dc)
        { }


        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingResult<M>> PagingListAsync<M>(PagingOption option)
            where M : class
        {
            return await new PagingListXOImpl(DC).PagingListAsync<M>(option);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingResult<T>> PagingListAsync<T>(PagingOption option, Expression<Func<T>> columnMapFunc)
        {
            return await new PagingListXOImpl(DC).PagingListAsync(option, columnMapFunc);
        }

    }
}
