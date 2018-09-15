using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;

namespace Yunyong.DataExchange.UserFacade.Join
{
    public class JoinX : Operator, IMethodObject
    {

        internal JoinX(DbContext dc)
            : base(dc)
        { }

    }
}
