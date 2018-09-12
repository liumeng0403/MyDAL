using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Yunyong.DataExchange.Common;

namespace Yunyong.DataExchange.UserFacade.Join
{
    public class QueryFilterX : Operator,IMethodObject
    {

        internal QueryFilterX(DbContext dc)
        {
            DC = dc;
            DC.OP = this;
        }

        public async Task<List<VM>> QueryListAsync<VM>()
        {
            return (await SqlHelper.QueryAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(SqlTypeEnum.JoinQueryListAsync)[0],
                DC.GetParameters())).ToList();
        }

    }
}
