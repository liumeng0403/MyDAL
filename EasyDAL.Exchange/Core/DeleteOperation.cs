using EasyDAL.Exchange.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Enums;

namespace EasyDAL.Exchange.Core
{
    public class DeleteOperation<M> : DbOperation
    {
        public DeleteOperation(IDbConnection conn)
            : base(conn)
        {
        }

        public DeleteOperation<M> Where<T>(Expression<Func<M, T>> func)
        {
            var field = EH.ExpressionHandle(func);
            Conditions.Add(new DicModel<string, string>
            {
                key=field,
                Value=null,
                Option= OptionEnum.None
            });
            return this;
        }

        public async Task<int> DeleteAsync(M m)
        {

            TryGetTableName(m, out var tableName);

            if (!Conditions.Any())
            {
                throw new Exception("没有设置任何删除条件!");
            }
            var whereFields = " where " + string.Join(" and ", Conditions.Select(p => $"`{p.key}`=@{p.key}"));
            var sql = $" delete from `{tableName}`{whereFields} ; ";

            return await SqlMapper.ExecuteAsync(Conn, sql, m);

        }

    }
}
