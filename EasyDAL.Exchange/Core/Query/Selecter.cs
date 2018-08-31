using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
            WhereHandle(func,CrudTypeEnum.Query);
            return new QueryFilter<M>(DC);
        }

        public async Task<List<M>> QueryAllAsync()
        {
            return (await SqlHelper.QueryAsync<M>(
                DC.Conn,
                DC.SqlProvider.GetSQL<M>(SqlTypeEnum.QueryAllAsync)[0],
                DC.SqlProvider.GetParameters())).ToList();
        }

    }
}
