using EasyDAL.Exchange.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core
{
    public class SelectOperation<M> : DbOperation
    {
        public SelectOperation(IDbConnection conn) 
            : base(conn)
        {
        }


        public SelectOperation<M> Where<T>(Expression<Func<M, T>> func)
        {
            var field = EH.GetFieldName(func);
            Conditions.Add(field);
            return this;
        }

        public async Task<M> QueryFirstOrDefaultAsync(M m)
        {

            return default(M);
        }

    }
}
