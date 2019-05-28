using HPC.DAL.Core.Bases;
using HPC.DAL.Impls.ImplAsyncs;
using HPC.DAL.Impls.ImplSyncs;
using HPC.DAL.Interfaces.IAsyncs;
using HPC.DAL.Interfaces.ISyncs;
using HPC.DAL.Interfaces.Segments;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HPC.DAL.UserFacade.Query
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class WhereQ<M>
        : Operator
        , IOrderByQ<M>
        , IQueryOneAsync<M>, IQueryOne<M>
        , IQueryListAsync<M>, IQueryList<M>
        , IQueryPagingAsync<M>, IQueryPaging<M>
        , ITopAsync<M>, ITop<M>
        , IIsExistAsync, IIsExist
        , ICountAsync<M>, ICount<M>
        , ISumAsync<M>, ISum<M>
        where M : class
    {
        internal WhereQ(Context dc)
            : base(dc)
        { }

        public OrderByQ<M> OrderBySegment
        {
            get
            {
                return new OrderByQ<M>(DC);
            }
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<M> QueryOneAsync()
        {
            return await new QueryOneAsyncImpl<M>(DC).QueryOneAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<VM> QueryOneAsync<VM>()
            where VM : class
        {
            return await new QueryOneAsyncImpl<M>(DC).QueryOneAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<T> QueryOneAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new QueryOneAsyncImpl<M>(DC).QueryOneAsync<T>(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public M QueryOne()
        {
            return new QueryOneImpl<M>(DC).QueryOne();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public VM QueryOne<VM>()
            where VM : class
        {
            return new QueryOneImpl<M>(DC).QueryOne<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryOneAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public T QueryOne<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return new QueryOneImpl<M>(DC).QueryOne<T>(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<M>> QueryListAsync()
        {
            return await new QueryListAsyncImpl<M>(DC).QueryListAsync();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<VM>> QueryListAsync<VM>()
            where VM : class
        {
            return await new QueryListAsyncImpl<M>(DC).QueryListAsync<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<List<T>> QueryListAsync<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return await new QueryListAsyncImpl<M>(DC).QueryListAsync(columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<M> QueryList()
        {
            return new QueryListImpl<M>(DC).QueryList();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<VM> QueryList<VM>()
            where VM : class
        {
            return new QueryListImpl<M>(DC).QueryList<VM>();
        }
        /// <summary>
        /// 请参阅: <see langword=".QueryListAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public List<T> QueryList<T>(Expression<Func<M, T>> columnMapFunc)
        {
            return new QueryListImpl<M>(DC).QueryList(columnMapFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<M>> QueryPagingAsync(int pageIndex, int pageSize)
        {
            return await new QueryPagingAsyncImpl<M>(DC).QueryPagingAsync(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public async Task<PagingResult<VM>> QueryPagingAsync<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return await new QueryPagingAsyncImpl<M>(DC).QueryPagingAsync<VM>(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public async Task<PagingResult<T>> QueryPagingAsync<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            return await new QueryPagingAsyncImpl<M>(DC).QueryPagingAsync<T>(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<M> QueryPaging(int pageIndex, int pageSize)
        {
            return new QueryPagingImpl<M>(DC).QueryPaging(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        /// <typeparam name="VM">ViewModel</typeparam>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        public PagingResult<VM> QueryPaging<VM>(int pageIndex, int pageSize)
            where VM : class
        {
            return new QueryPagingImpl<M>(DC).QueryPaging<VM>(pageIndex, pageSize);
        }
        /// <summary>
        /// 单表分页查询
        /// </summary>
        public PagingResult<T> QueryPaging<T>(int pageIndex, int pageSize, Expression<Func<M, T>> columnMapFunc)
        {
            return new QueryPagingImpl<M>(DC).QueryPaging<T>(pageIndex, pageSize, columnMapFunc);
        }

        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<M>> TopAsync(int count)
        {
            return await new TopAsyncImpl<M>(DC).TopAsync(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<VM>> TopAsync<VM>(int count)
            where VM : class
        {
            return await new TopAsyncImpl<M>(DC).TopAsync<VM>(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public async Task<List<T>> TopAsync<T>(int count, Expression<Func<M, T>> columnMapFunc)
        {
            return await new TopAsyncImpl<M>(DC).TopAsync<T>(count, columnMapFunc);
        }

        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public List<M> Top(int count)
        {
            return new TopImpl<M>(DC).Top(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public List<VM> Top<VM>(int count)
            where VM : class
        {
            return new TopImpl<M>(DC).Top<VM>(count);
        }
        /// <summary>
        /// 单表数据查询
        /// </summary>
        /// <param name="count">top count</param>
        /// <returns>返回 top count 条数据</returns>
        public List<T> Top<T>(int count, Expression<Func<M, T>> columnMapFunc)
        {
            return new TopImpl<M>(DC).Top<T>(count, columnMapFunc);
        }

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<bool> IsExistAsync()
        {
            return await new IsExistAsyncImpl<M>(DC).IsExistAsync();
        }

        /// <summary>
        /// 请参阅: <see langword=".IsExistAsync() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public bool IsExist()
        {
            return new IsExistImpl<M>(DC).IsExist();
        }

        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public async Task<int> CountAsync()
        {
            return await new CountAsyncImpl<M>(DC).CountAsync();
        }
        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public async Task<int> CountAsync<F>(Expression<Func<M, F>> propertyFunc)
        {
            return await new CountAsyncImpl<M>(DC).CountAsync(propertyFunc);
        }

        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public int Count()
        {
            return new CountImpl<M>(DC).Count();
        }
        /// <summary>
        /// 查询符合条件数据条目数
        /// </summary>
        public int Count<F>(Expression<Func<M, F>> propertyFunc)
        {
            return new CountImpl<M>(DC).Count(propertyFunc);
        }

        /// <summary>
        /// 列求和 -- select sum(col) from ...
        /// </summary>
        public async Task<F> SumAsync<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            return await new SumAsyncImpl<M>(DC).SumAsync(propertyFunc);
        }
        /// <summary>
        /// 列求和 -- select sum(col) from ...
        /// </summary>
        public async Task<F?> SumAsync<F>(Expression<Func<M, F?>> propertyFunc)
            where F : struct
        {
            return await new SumAsyncImpl<M>(DC).SumAsync(propertyFunc);
        }

        /// <summary>
        /// 列求和 -- select sum(col) from ...
        /// </summary>
        public F Sum<F>(Expression<Func<M, F>> propertyFunc)
            where F : struct
        {
            return new SumImpl<M>(DC).Sum(propertyFunc);
        }
        /// <summary>
        /// 列求和 -- select sum(col) from ...
        /// </summary>
        public F? Sum<F>(Expression<Func<M, F?>> propertyFunc)
            where F : struct
        {
            return new SumImpl<M>(DC).Sum(propertyFunc);
        }

    }
}
