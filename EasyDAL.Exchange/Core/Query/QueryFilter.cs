using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using Rainbow.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Query
{
    public class QueryFilter<M> : Operator
    {
        internal QueryFilter(DbContext dc)
            : base()
        {
            DC = dc;
        }


        public QueryFilter<M> And(Expression<Func<M, bool>> func)
        {
            AndHandle(func, CrudTypeEnum.Query);
            return this;
        }

        public QueryFilter<M> Or(Expression<Func<M, bool>> func)
        {
            OrHandle(func, CrudTypeEnum.Query);
            return this;
        }

        public SingleFilter<M> Count<F>(Expression<Func<M, F>> func)
        {
            var field = DC.EH.ExpressionHandle(func);
            DC.AddConditions(new DicModel
            {
                KeyOne = field,
                Param = field,
                Action = ActionEnum.Select,
                Option = OptionEnum.Count,
                Crud = CrudTypeEnum.Query
            });
            return new SingleFilter<M>(DC);
        }

        public async Task<bool> ExistAsync()
        {
            var count = await SqlHelper.ExecuteScalarAsync<long>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(SqlTypeEnum.ExistAsync)[0],
                DC.SqlProvider.GetParameters());
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public async Task<M> QueryFirstOrDefaultAsync()
        {
            return await QueryFirstOrDefaultAsyncHandle<M, M>();
        }
        public async Task<VM> QueryFirstOrDefaultAsync<VM>()
        {
            return await QueryFirstOrDefaultAsyncHandle<M, VM>();
        }


        public async Task<List<M>> QueryListAsync()
        {
            return await QueryListAsyncHandle<M, M>();
        }
        public async Task<List<VM>> QueryListAsync<VM>()
        {
            return await QueryListAsyncHandle<M, VM>();
        }


        public async Task<PagingList<M>> QueryPagingListAsync(int pageIndex, int pageSize)
        {
            return await QueryPagingListAsyncHandle<M, M>(pageIndex, pageSize);
        }
        public async Task<PagingList<VM>> QueryPagingListAsync<VM>(int pageIndex, int pageSize)
        {
            return await QueryPagingListAsyncHandle<M, VM>(pageIndex, pageSize);
        }

    }
}
