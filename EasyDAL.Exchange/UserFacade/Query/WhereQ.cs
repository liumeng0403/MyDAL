using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.Impls;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.UserFacade.Query
{
    public class WhereQ<M> 
        : Operator, IExist, IQueryFirstOrDefault<M>, IQueryList<M>, IQueryPagingList<M>, ICount<M>
    {
        internal WhereQ(Context dc)
            : base(dc)
        {  }

        /// <summary>
        /// 查询是否存在符合条件的数据
        /// </summary>
        public async Task<bool> ExistAsync()
        {
            return await new ExistImpl<M>(DC).ExistAsync();
        }

        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public async Task<long> CountAsync()
        {
            return await new CountImpl<M>(DC).CountAsync();
        }
        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public async Task<long> CountAsync<F>(Expression<Func<M, F>> func)
        {
            return await new CountImpl<M>(DC).CountAsync(func);
        }

        /// <summary>
        /// 单表单条数据查询
        /// </summary>
        public async Task<M> QueryFirstOrDefaultAsync()
        {
            return await QueryFirstOrDefaultAsyncHandle<M, M>();
        }
        /// <summary>
        /// 单表单条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<VM> QueryFirstOrDefaultAsync<VM>()
        {
            return await QueryFirstOrDefaultAsyncHandle<M, VM>();
        }
        /// <summary>
        /// 单表单条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<VM> QueryFirstOrDefaultAsync<VM>(Expression<Func<M, VM>> func)
        {
            SelectMHandle(func);
            return await QueryFirstOrDefaultAsyncHandle<M, VM>();
        }

        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<M>> QueryListAsync()
        {
            return await QueryListAsyncHandle<M, M>();
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<List<VM>> QueryListAsync<VM>()
        {
            return await QueryListAsyncHandle<M, VM>();
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<VM>> QueryListAsync<VM>(Expression<Func<M,VM>> func)
        {
            SelectMHandle(func);
            return await QueryListAsyncHandle<M, VM>();
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> QueryPagingListAsync(int pageIndex, int pageSize)
        {
            return await QueryPagingListAsyncHandle<M, M>(pageIndex, pageSize, UiMethodEnum.QueryPagingListAsync);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize)
        {
            return await QueryPagingListAsyncHandle<M, VM>(pageIndex, pageSize, UiMethodEnum.QueryPagingListAsync);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize, Expression<Func<M, VM>> func)
        {
            SelectMHandle(func);
            return await QueryPagingListAsyncHandle<M, VM>(pageIndex, pageSize, UiMethodEnum.QueryPagingListAsync);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> QueryPagingListAsync(PagingQueryOption option)
        {
            OrderByOptionHandle(option);
            return await QueryPagingListAsyncHandle<M, M>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option)
        {
            OrderByOptionHandle(option);
            return await QueryPagingListAsyncHandle<M, VM>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option, Expression<Func<M, VM>> func)
        {
            SelectMHandle(func);
            OrderByOptionHandle(option);
            return await QueryPagingListAsyncHandle<M, VM>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }

    }
}
