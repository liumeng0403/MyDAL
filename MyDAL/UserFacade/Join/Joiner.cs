using MyDAL.Core.Bases;

namespace MyDAL.UserFacade.Join
{
    public sealed class Queryer
        : Operator
    {
        internal Queryer(Context dc)
            : base(dc)
        { }

    }
}
