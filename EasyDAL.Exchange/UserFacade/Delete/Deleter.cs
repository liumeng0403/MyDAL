using Yunyong.DataExchange.Core;

namespace Yunyong.DataExchange.UserFacade.Delete
{
    public class Deleter<M> 
        : Operator
    {
        internal Deleter(Context dc)
            : base(dc)
        { }
    }
}
