using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Join
{
    public sealed class WhereX
        : Operator, IFirstOrDefaultX, IQueryListX, IQueryPagingX, ICountX, ITopX, IExistX
    {

        internal WhereX(Context dc)
            : base(dc)
        { }

        public async Task<int> CountAsync()
        {
            return await new CountXImpl(DC).CountAsync();
        }
        public async Task<int> CountAsync<F>(Expression<Func<F>> propertyFunc)
        {
            return await new CountXImpl(DC).CountAsync(propertyFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".FirstOrDefaultAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<M> FirstOrDefaultAsync<M>()
            where M : class
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
        /// 请参阅: <see langword=".QueryListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<M>> QueryListAsync<M>()
            where M : class
        {
            return await new QueryListXImpl(DC).QueryListAsync<M>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<T>> columnMapFunc)
        {
            return await new QueryListXImpl(DC).QueryListAsync(columnMapFunc);
        }

        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<M>> QueryPagingAsync<M>(int pageIndex, int pageSize)
            where M : class
        {
            return await new QueryPagingXImpl(DC).QueryPagingAsync<M>(pageIndex, pageSize);
        }
        /// <summary>
        /// 多表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<T>> columnMapFunc)
        {
            return await new QueryPagingXImpl(DC).QueryPagingAsync(pageIndex, pageSize, columnMapFunc);
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

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 " cref="https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<bool> IsExistAsync()
        {
            return await new ExistXImpl(DC).IsExistAsync();
        }
    }
}
