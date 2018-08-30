using EasyDAL.Exchange.Core.Sql;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.Core.Join
{
    public class QueryFilter : Operator
    {

        internal QueryFilter(DbContext dc)
        {
            DC = dc;
        }

        public QueryFilter And(Expression<Func<bool>> func)
        {
            var field = DC.EH.ExpressionHandle(func, ActionEnum.And);
            field.Crud = CrudTypeEnum.Join;
            DC.AddConditions(field);
            return this;
        }

        public async Task<List<VM>> QueryListAsync<VM>()
        {
            return (await SqlHelper.QueryAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(SqlTypeEnum.JoinQueryListAsync)[0],
                DC.SqlProvider.GetParameters())).ToList();
        }

    }
}
