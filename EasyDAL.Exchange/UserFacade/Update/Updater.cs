using Yunyong.DataExchange.Common;
using Yunyong.DataExchange.Core;

namespace Yunyong.DataExchange.UserFacade.Update
{
    public sealed class Updater<M> : Operator, IMethodObject
    {
        internal Updater(Context dc)
            : base(dc)
        { }
    }
}
