using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;

namespace EasyDAL.Exchange.UserFacade.Update
{
    public sealed class Updater<M> : Operator, IMethodObject
    {
        internal Updater(Context dc)
            : base(dc)
        { }
    }
}
