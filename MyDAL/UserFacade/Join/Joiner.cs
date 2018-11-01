using MyDAL.Core.Bases;

namespace MyDAL.UserFacade.Join
{
    public sealed class Joiner 
        : Operator
    {
        internal Joiner(Context dc)
            : base(dc)
        { }

    }
}
