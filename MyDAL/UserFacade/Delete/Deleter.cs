using MyDAL.Core.Bases;

namespace MyDAL.UserFacade.Delete
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Deleter<M> 
        : Operator
    {
        internal Deleter(Context dc)
            : base(dc)
        { }
    }
}
