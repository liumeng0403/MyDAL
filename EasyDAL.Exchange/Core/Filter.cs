using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core
{
    public class Filter<M>
    {
        private DbContext DC { get; set; }

        public Filter(DbContext dc)
        {
            DC = dc;
        }

        public Filter<M> And(Expression<Func<M, bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.And;
            DC.Conditions.Add(field);
            return this;
        }

        public Filter<M> Or(Expression<Func<M, bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func);
            field.Action = ActionEnum.Or;
            DC.Conditions.Add(field);
            return this;
        }

        public async Task<int> UpdateAsync()
        {
            DC.OP.TryGetTableName<M>(out var tableName);

            var updateFields = DC.OP.GetUpdates();
            var wherePart = DC.OP.GetWheres();
            var paras = DC.OP.GetParameters();
            var sql = $" update `{tableName}` set {updateFields} where {wherePart} ;";

            return await SqlMapper.ExecuteAsync(DC.Conn, sql, paras);
        }

    }
}
