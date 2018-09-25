using EasyDAL.Exchange.Core;

namespace EasyDAL.Exchange.UserFacade.Update
{
    public sealed class Updater<M> 
        : Operator
    {
        internal Updater(Context dc)
            : base(dc)
        { }
    }
}
