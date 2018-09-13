using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;
using Yunyong.DataExchange.Enums;
using Yunyong.DataExchange.Helper;

namespace Yunyong.DataExchange.UserFacade.Join
{
    public class QueryFilterX : Operator, IMethodObject
    {

        internal QueryFilterX(DbContext dc)
            : base(dc)
        { }

        public async Task<List<VM>> QueryListAsync<VM>()
        {
            return (await SqlHelper.QueryAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(SqlTypeEnum.JoinQueryListAsync)[0],
                DC.GetParameters())).ToList();
        }

    }
}
