using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Query
{
    public class QueryFilter<M>: Operator
    {
        internal QueryFilter(DbContext dc)
        {
            DC = dc;
        }


        public QueryFilter<M> And(Expression<Func<M, bool>> func)
        {
            AndHandle(func);
            return this;
        }

        public QueryFilter<M> Or(Expression<Func<M, bool>> func)
        {
            OrHandle(func);
            return this;
        }


        public async Task<M> QueryFirstOrDefaultAsync()
        {
            DC.OP.TryGetTableName<M>(out var tableName);

            var wherePart = DC.OP.GetWheres();
            var sql = $"SELECT * FROM `{tableName}` WHERE {wherePart} ; ";
            var paras = DC.OP.GetParameters();

            return await SqlMapper.QueryFirstOrDefaultAsync<M>(DC.Conn, sql, paras);
        }


        public async Task<List<M>> QueryListAsync()
        {
            DC.OP.TryGetTableName<M>(out var tableName);

            var wherePart = DC.OP.GetWheres();
            var sql = $"SELECT * FROM `{tableName}` WHERE {wherePart} ; ";
            var paras = DC.OP.GetParameters();

            return (await SqlMapper.QueryAsync<M>(DC.Conn, sql, paras)).ToList();
        }


        public async Task<PagingList<M>> QueryPagingListAsync(int pageIndex, int pageSize)
        {
            var result = new PagingList<M>();
            result.PageIndex = pageIndex;
            result.PageSize = pageSize;

            DC.OP.TryGetTableName<M>(out var tableName);

            var wherePart = DC.OP.GetWheres();
            var totalSql = $"SELECT count(*) FROM `{tableName}` WHERE {wherePart} ; ";
            var dataSql = $"SELECT * FROM `{tableName}` WHERE {wherePart} limit {(pageIndex - 1) * pageSize},{pageIndex * pageSize}  ; ";
            var paras = DC.OP.GetParameters();
            result.TotalCount = await SqlMapper.ExecuteScalarAsync<long>(DC.Conn, totalSql, paras);
            result.Data = (await SqlMapper.QueryAsync<M>(DC.Conn, dataSql, paras)).ToList();


            return result;
        }


    }
}
