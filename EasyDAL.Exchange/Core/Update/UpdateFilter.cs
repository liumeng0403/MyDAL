using EasyDAL.Exchange.AdoNet;
using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Update
{
    public class UpdateFilter<M>:Operator
    {        
        internal UpdateFilter(DbContext dc)
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
            return await SqlHelper.ExecuteAsync(
                DC.Conn, 
                DC.SqlProvider.GetSQL<M>( SqlTypeEnum.UpdateAsync)[0],
                DC.SqlProvider.GetParameters());
        }

    }
}
