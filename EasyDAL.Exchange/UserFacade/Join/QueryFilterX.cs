using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;
using EasyDAL.Exchange.Enums;
using EasyDAL.Exchange.Helper;
using System.Collections.Generic;
using System.Linq;
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
            return (await SqlHelper.QueryAsync<VM>(
                DC.Conn,
                DC.SqlProvider.GetSQL<VM>(SqlTypeEnum.JoinQueryListAsync)[0],
                DC.GetParameters())).ToList();
        }

    }
}
