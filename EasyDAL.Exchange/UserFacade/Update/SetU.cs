using MyDAL.Core;

namespace MyDAL.UserFacade.Update
{
    public class SetU<M> 
        : Operator
    {

        internal SetU(Context dc)
            : base(dc)
        { }

    }
}
