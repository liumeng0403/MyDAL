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

        public DeleteOperation<M> Where(Expression<Func<M, bool>> func)
        {
            var field = EH.ExpressionHandle(func);
            field.Action = ActionEnum.Where;
            Conditions.Add(field);
            return this;
        }

        public DeleteOperation<M> And(Expression<Func<M, bool>> func)
        {
            var field = EH.ExpressionHandle(func);
            field.Action = ActionEnum.And;
            Conditions.Add(field);
            return this;
        }

        public DeleteOperation<M> Or(Expression<Func<M, bool>> func)
        {
            var field = EH.ExpressionHandle(func);
            field.Action = ActionEnum.Or;
            Conditions.Add(field);
            return this;
        }

        public async Task<int> DeleteAsync()
        {

            TryGetTableName<M>(out var tableName);

            if (!Conditions.Any())
            {
                throw new Exception("没有设置任何删除条件!");
            }
            var wherePart = GetWheres();
            var sql = $" delete from `{tableName}` where {wherePart} ; ";
            var paras = GetParameters();

            return await SqlMapper.ExecuteAsync(Conn, sql, paras);

        }

    }
}
