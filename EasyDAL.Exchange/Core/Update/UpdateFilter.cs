using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Update
{
    public class UpdateFilter<M>:Operator
    {        
        public UpdateFilter(DbContext dc)
        {
            DC = dc;
        }

        public UpdateFilter<M> And(Expression<Func<M, bool>> func)
        {
            AndHandle(func);
            return this;
        }


        public UpdateFilter<M> Or(Expression<Func<M, bool>> func)
        {
            OrHandle(func);
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
