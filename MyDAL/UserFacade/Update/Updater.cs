
using Yunyong.DataExchange.Core.Bases;

namespace Yunyong.DataExchange.UserFacade.Update
{
    public sealed class Updater<M> 
        : Operator
    {
        internal Updater(Context dc)
            : base(dc)
        { }
    }
}
