using EasyDAL.Exchange.Common;
using EasyDAL.Exchange.Core;

namespace EasyDAL.Exchange.UserFacade.Delete
{
    public class Deleter<M> : Operator, IMethodObject
    {
        internal Deleter(DbContext dc)
            : base(dc)
        { }


    }
}
