using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.UserFacade.Query
{
    public class WhereQ<M> : Operator,IMethodObject
    {
        internal WhereQ(Context dc)
            : base(dc)
        {  }

        /// <summary>
        /// select count(column)
        /// </summary>
        /// <param name="func">格式: it => it.Id</param>
        public SingleFilter<M> Count<F>(Expression<Func<M, F>> func)
        {
            var field = DC.EH.ExpressionHandle(func);
            DC.AddConditions(new DicModel
            {
                ColumnOne = field,
                Param = field,
                ParamRaw=field,
                Action = ActionEnum.Select,
                Option = OptionEnum.Count,
                Crud = CrudTypeEnum.Query
            });
            return new SingleFilter<M>(DC);
        }

        /// <summary>
        /// 查询是否存在符合条件的数据
        /// </summary>
        public async Task<bool> ExistAsync()
        {
            var count = await SqlHelper.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(UiMethodEnum.ExistAsync)[0],
                DC.GetParameters());
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(PagingQueryOption option)
        {
            OrderByOptionHandle(option);
            return await QueryPagingListAsyncHandle<M, VM>(option.PageIndex, option.PageSize, UiMethodEnum.QueryPagingListAsync);
        }

    }
}
