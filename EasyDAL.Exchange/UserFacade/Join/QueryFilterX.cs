using System;
using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyDAL.Exchange.UserFacade.Join
{
    public class QueryFilterX : Operator, IMethodObject
    {

        internal QueryFilterX(DbContext dc)
            : base(dc)
        { }

        public async Task<List<VM>> QueryListAsync<VM>()
        {
            SelectMHandle<VM>();
            return (await SqlHelper.QueryAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryListAsync)[0],
                DC.GetParameters())).ToList();
        }

        public async Task<List<VM>> QueryListAsync<VM>(Expression<Func<VM>> func)
        {
            var list = DC.EH.ExpressionHandle(func);
            foreach (var dic in list)
            {
                dic.Action = ActionEnum.Select;
                dic.Option = OptionEnum.ColumnAs;
                dic.Crud = CrudTypeEnum.Join;
                DC.AddConditions(dic);
            }
            return (await SqlHelper.QueryAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(UiMethodEnum.JoinQueryListAsync)[0],
                DC.GetParameters())).ToList();
        }

    }
}
