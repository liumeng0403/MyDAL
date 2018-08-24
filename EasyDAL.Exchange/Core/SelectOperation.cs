using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Base;
using EasyDAL.Exchange.DynamicParameter;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core
{
    public class SelectOperation<M> : DbOperation
    {
        private PagingList<M> PagingList { get; set; }

        public SelectOperation(IDbConnection conn)
            : base(conn)
        {
            PagingList = new PagingList<M>();
        }

        public SelectOperation<M> Where(Expression<Func<M, bool>> func)
        {
            var field = EH.ExpressionHandle(func);
            Conditions.Add(field);
            return this;
        }

        public SelectOperation<M> SetPageSize(int pageSize)
        {
            PagingList.PageSize = pageSize;
            return this;
        }

        public SelectOperation<M> SetPageIndex(int pageIndex)
        {
            PagingList.PageIndex = pageIndex;
            return this;
        }

        public async Task<M> QueryFirstOrDefaultAsync()
        {
            TryGetTableName<M>(out var tableName);

            if (!Conditions.Any())
            {
                throw new Exception("没有设置任何查询条件!");
            }

            var wherePart = string.Join(" and ", GetWheres());
            var sql = $"SELECT * FROM `{tableName}` WHERE {wherePart} ; ";
            var paras = GetParameters();

            return await SqlMapper.QueryFirstOrDefaultAsync<M>(Conn, sql, paras);
        }

        public async Task<List<M>> QueryListAsync()
        {
            TryGetTableName<M>(out var tableName);

            if (!Conditions.Any())
            {
                throw new Exception("没有设置任何查询条件!");
            }

            var wherePart = string.Join(" and ", GetWheres());
            var sql = $"SELECT * FROM `{tableName}` WHERE {wherePart} ; ";
            var paras = GetParameters();

            return (await SqlMapper.QueryAsync<M>(Conn, sql, paras)).ToList();
        }

        public async Task<PagingList<M>> QueryPagingListAsync()
        {
            TryGetTableName<M>(out var tableName);

            if (!Conditions.Any())
            {
                throw new Exception("没有设置任何查询条件!");
            }

            var wherePart = string.Join(" and ", GetWheres());
            var totalSql = $"SELECT count(*) FROM `{tableName}` WHERE {wherePart} ; ";
            var dataSql = $"SELECT * FROM `{tableName}` WHERE {wherePart} limit {(PagingList.PageIndex - 1) * PagingList.PageSize},{PagingList.PageIndex * PagingList.PageSize}  ; ";
            var paras = GetParameters();
            PagingList.TotalCount = await SqlMapper.QuerySingleOrDefaultAsync<long>(Conn, totalSql, paras);
            PagingList.Data = (await SqlMapper.QueryAsync<M>(Conn, dataSql, paras)).ToList();


            return PagingList;
        }
    }
}
