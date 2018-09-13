using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;

namespace Yunyong.DataExchange.UserFacade.Update
{
    public sealed class Setter<M> : Operator, IMethodObject
    {
        internal Setter(DbContext dc)
            : base(dc)
        { }
    }
}
