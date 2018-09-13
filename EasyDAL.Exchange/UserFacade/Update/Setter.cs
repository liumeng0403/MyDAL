using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;

namespace EasyDAL.Exchange.UserFacade.Update
{
    public sealed class Setter<M> : Operator, IMethodObject
    {
        internal Setter(DbContext dc)
            : base(dc)
        { }
    }
}
