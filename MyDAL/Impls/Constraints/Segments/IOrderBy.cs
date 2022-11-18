using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;

namespace MyDAL.Impls.Constraints.Segments
{
    internal interface IOrderByQ<M>
        where M : class
    {
        OrderByQ<M> OrderBySegment { get; }
    }

    internal interface IOrderByX
    {
        OrderByX OrderBySegment { get; }
    }
}
