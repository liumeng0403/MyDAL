using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yunyong.Core;
using Yunyong.DataExchange.Core.Bases;
using Yunyong.DataExchange.Impls;
using Yunyong.DataExchange.Interfaces;

namespace Yunyong.DataExchange.UserFacade.Query
{
    public sealed class WhereQ<M>
        : Operator, IExist, IFirstOrDefault<M>, Interfaces.IList<M>, IPagingList<M>, IPagingListO<M>, ICount<M>,ITop<M>,ISum<M>
        where M : class
    {
        internal WhereQ(Context dc)
            : base(dc)
        { }

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
        public async Task<int> CountAsync()
        {
            return await new CountImpl<M>(DC).CountAsync();
        }
        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public async Task<int> CountAsync<F>(Expression<Func<M, F>> propertyFunc)
        {
            return await new CountImpl<M>(DC).CountAsync(propertyFunc);
        }

        /// <summary>
        /// 列求和 -- select sum(col) from ...
        /// </summary>
        public async Task<F> SumAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            return await new SumImpl<M>(DC).SumAsync(propertyFunc);
        }

        /// <summary>
        /// 单表单条数据查询
        /// </summary>
        public async Task<M> FirstOrDefaultAsync()
        {
            return await new FirstOrDefaultImpl<M>(DC).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 单表单条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<VM> FirstOrDefaultAsync<VM>()
            where VM:class
        {
            return await new FirstOrDefaultImpl<M>(DC).FirstOrDefaultAsync<VM>();
        }
        /// <summary>
        /// 单表单条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new FirstOrDefaultImpl<M>(DC).FirstOrDefaultAsync<T>(columnMapFunc);
        }

        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<M>> ListAsync()
        {
            return await new ListImpl<M>(DC).ListAsync();
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        public async Task<List<VM>> ListAsync<VM>()
            where VM:class
        {
            return await new ListImpl<M>(DC).ListAsync<VM>();
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<T>> ListAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new ListImpl<M>(DC).ListAsync(columnMapFunc);
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<M>> ListAsync(int topCount)
        {
            return await new ListImpl<M>(DC).ListAsync(topCount);
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<VM>> ListAsync<VM>(int topCount) 
            where VM : class
        {
            return await new ListImpl<M>(DC).ListAsync<VM>(topCount);
        }
        /// <summary>
        /// 单表多条数据查询
        /// </summary>
        public async Task<List<T>> ListAsync<T>(int topCount, Expression<Func<M, T>> columnMapFunc) 
        {
            return await new ListImpl<M>(DC).ListAsync(topCount, columnMapFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> PagingListAsync(int pageIndex, int pageSize)
        {
            return await new PagingListImpl<M>(DC).PagingListAsync(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> PagingListAsync<VM>(int pageIndex, int pageSize)
            where VM:class
        {
            return await new PagingListImpl<M>(DC).PagingListAsync<VM>(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<T>> PagingListAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            return await new PagingListImpl<M>(DC).PagingListAsync<T>(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> PagingListAsync(PagingQueryOption option)
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync(option);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<VM>> PagingListAsync<VM>(PagingQueryOption option)
            where VM:class
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync<VM>(option);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<T>> PagingListAsync<T>(PagingQueryOption option, Expression<Func<M, T>> columnMapFunc)
        {
            return await new PagingListOImpl<M>(DC).PagingListAsync<T>(option, columnMapFunc);
        }

        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<M>> TopAsync(int count)
        {
            return await new TopImpl<M>(DC).TopAsync(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<VM>> TopAsync<VM>(int count)
            where VM : class
        {
            return await new TopImpl<M>(DC).TopAsync<VM>(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<M, T>> columnMapFunc)
        {
            return await new TopImpl<M>(DC).TopAsync<T>(count, columnMapFunc);
        }


    }
}
