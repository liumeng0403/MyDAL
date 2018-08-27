using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Delete
{
    public class DeleteFilter<M>:Operator
    {        
        internal DeleteFilter(DbContext dc)
        {
            DC = dc;
        }

        public DeleteFilter<M> And(Expression<Func<M, bool>> func)
        {
            AndHandle(func);
            return this;
        }


        public DeleteFilter<M> Or(Expression<Func<M, bool>> func)
        {
            OrHandle(func);
            return this;
        }


        public async Task<int> DeleteAsync()
        {

            DC.OP.TryGetTableName<M>(out var tableName);

            var wherePart = DC.OP.GetWheres();
            var sql = $" delete from `{tableName}` where {wherePart} ; ";
            var paras = DC.OP.GetParameters();

            return await SqlMapper.ExecuteAsync(DC.Conn, sql, paras);

        }

    }
}
