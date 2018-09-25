using EasyDAL.Exchange.Core;

namespace EasyDAL.Exchange.UserFacade.Delete
{
    public class Deleter<M> 
        : Operator
    {
        internal Deleter(Context dc)
            : base(dc)
        { }
    }
}
