using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;

namespace EasyDAL.Exchange.UserFacade.Join
{
    public class Joiner : Operator, IMethodObject
    {
        internal Joiner(Context dc)
            : base(dc)
        { }

    }
}
