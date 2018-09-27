using MyDAL.Core;

namespace MyDAL.UserFacade.Delete
{
    public class Deleter<M> 
        : Operator
    {
        internal Deleter(Context dc)
            : base(dc)
        { }
    }
}
