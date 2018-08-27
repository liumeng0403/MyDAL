using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EasyDAL.Exchange.Core.Query
{
    public class Selecter<M>: Operator
    {
        internal Selecter(DbContext dc)
        {
            DC = dc;
        }

        public QueryFilter<M> Where(Expression<Func<M, bool>> func)
        {
            WhereHandle(func);
            return new QueryFilter<M>(DC);
        }

    }
}
