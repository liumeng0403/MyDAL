using MyDAL.Core.Bases;

namespace MyDAL.UserFacade.Update
{
    public sealed class Updater<M> 
        : Operator
    {
        internal Updater(Context dc)
            : base(dc)
        { }
    }
}
