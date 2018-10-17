
using Yunyong.DataExchange.Core.Bases;

namespace Yunyong.DataExchange.UserFacade.Delete
{
    public sealed class Deleter<M> 
        : Operator
    {
        internal Deleter(Context dc)
            : base(dc)
        { }
    }
}
