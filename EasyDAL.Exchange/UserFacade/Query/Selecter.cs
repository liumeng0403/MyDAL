using System.Collections.Generic;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Impls;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.UserFacade.Query
{
    public sealed class Selecter<M>
        : Operator, IQueryAll<M>, IQueryAllPagingList<M>
        where M : class
    {
        internal Selecter(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <returns>返回全表数据</returns>
        public async Task<List<M>> QueryAllAsync()
        {
            return await new QueryAllImpl<M>(DC).QueryAllAsync();
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <returns>返回全表数据</returns>
        public async Task<List<VM>> QueryAllAsync<VM>()
            where VM : class
        {
            return await new QueryAllImpl<M>(DC).QueryAllAsync<VM>();
        }
        public async Task<List<F>> QueryAllAsync<F>(System.Linq.Expressions.Expression<System.Func<M, F>> propertyFunc)
            where F : struct
        {
            return await new QueryAllImpl<M>(DC).QueryAllAsync<F>(propertyFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>返回全表分页数据</returns>
        public async Task<PagingList<M>> QueryAllPagingListAsync(int pageIndex, int pageSize)
        {
            return await new QueryAllPagingListImpl<M>(DC).QueryAllPagingListAsync(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>返回全表分页数据</returns>
        public async Task<PagingList<VM>> QueryAllPagingListAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return await new QueryAllPagingListImpl<M>(DC).QueryAllPagingListAsync<VM>(pageIndex, pageSize);
        }

    }
}
