using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;

namespace Yunyong.DataExchange.UserFacade.Join
{
    public class Joiner : Operator, IMethodObject
    {
        internal Joiner(DbContext dc)
            : base(dc)
        { }

    }
}
