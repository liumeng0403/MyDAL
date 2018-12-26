using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Join
{
    public class OrderByX
        : Operator, IFirstOrDefaultX, IListX, IPagingListX, IPagingListXO,ITopX
    {
        internal OrderByX(Context dc) 
            : base(dc)
        {
        }

        /// <summary>
        /// 请参阅: <see langword=".FirstOrDefaultAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<M> FirstOrDefaultAsync<M>()
            where M:class
        {
            return await new FirstOrDefaultXImpl(DC).FirstOrDefaultAsync<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".FirstOrDefaultAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<T> FirstOrDefaultAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new FirstOrDefaultXImpl(DC).FirstOrDefaultAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".ListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<M>> ListAsync<M>()
            where M:class
        {
            return await new ListXImpl(DC).ListAsync<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".ListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<T>> ListAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new ListXImpl(DC).ListAsync(columnMapFunc);
        }
        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<M>> ListAsync<M>(int topCount)
            where M : class
        {
            return await new ListXImpl(DC).ListAsync<M>(topCount);
        }
        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<VM>> ListAsync<VM>(int topCount, Expression<Func<VM>> columnMapFunc)
            where VM : class
        {
            return await new ListXImpl(DC).ListAsync<VM>(topCount, columnMapFunc);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingList<M>> PagingListAsync<M>(int pageIndex, int pageSize)
            where M:class
        {
            return await new PagingListXImpl(DC).PagingListAsync<M>(pageIndex, pageSize);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingList<T>> PagingListAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
        {
            return await new PagingListXImpl(DC).PagingListAsync(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingList<M>> PagingListAsync<M>(PagingQueryOption option)
            where M:class
        {
            return await new PagingListXOImpl(DC).PagingListAsync<M>(option);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        public async Task<PagingList<T>> PagingListAsync<T>(PagingQueryOption option, Expression<Func<T>> columnMapFunc)
        {
            return await new PagingListXOImpl(DC).PagingListAsync(option, columnMapFunc);
        }

        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<M>> TopAsync<M>(int count)
            where M : class
        {
            return await new TopXImpl(DC).TopAsync<M>(count);
        }
        /// <summary>
        /// 多表多条数据查询
        /// </summary>
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<T>> columnMapFunc)
        {
            return await new TopXImpl(DC).TopAsync(count, columnMapFunc);
        }
    }
}
