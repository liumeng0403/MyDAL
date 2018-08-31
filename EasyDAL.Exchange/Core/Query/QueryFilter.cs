using EasyDAL.Exchange.AdoNet;
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
            AndHandle(func,CrudTypeEnum.Query);
            return this;
        }

        public QueryFilter<M> Or(Expression<Func<M, bool>> func)
        {
            OrHandle(func, CrudTypeEnum.Query);
            return this;
        }


        public async Task<M> QueryFirstOrDefaultAsync()
        {
            return await SqlHelper.QueryFirstOrDefaultAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(SqlTypeEnum.QueryFirstOrDefaultAsync)[0],
                DC.SqlProvider.GetParameters());
        }


        public async Task<List<M>> QueryListAsync()
        {
            return (await SqlHelper.QueryAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(SqlTypeEnum.QueryListAsync)[0],
                DC.SqlProvider.GetParameters())).ToList();
        }


        public async Task<PagingList<M>> QueryPagingListAsync(int pageIndex, int pageSize)
        {
            var result = new PagingList<M>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;
            var paras = DC.SqlProvider.GetParameters();
            var sql = DC.SqlProvider.GetSQL<M>(SqlTypeEnum.QueryPagingListAsync, result);
            result.TotalCount = (int)(await SqlHelper.ExecuteScalarAsync<long>(DC.Conn, sql[0], paras));
            result.Data = (await SqlHelper.QueryAsync<M>(DC.Conn, sql[1], paras)).ToList();
            return result;
        }


    }
}
