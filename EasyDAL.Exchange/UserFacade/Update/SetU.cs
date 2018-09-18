using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;

namespace EasyDAL.Exchange.UserFacade.Update
{
    public class SetU<M> : Operator, IMethodObject
    {

        internal SetU(Context dc)
            : base(dc)
        { }

    }
}
