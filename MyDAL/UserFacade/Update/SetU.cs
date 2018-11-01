using MyDAL.Core.Bases;

namespace MyDAL.UserFacade.Update
{
    public sealed class SetU<M> 
        : Operator
    {

        internal SetU(Context dc)
            : base(dc)
        { }

    }
}
