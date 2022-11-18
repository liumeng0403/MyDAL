using MyDAL.UserFacade.Delete;
using MyDAL.UserFacade.Join;
using MyDAL.UserFacade.Query;
using MyDAL.UserFacade.Update;

namespace MyDAL.Impls.Constraints.Segments
{
    internal interface IWhereD<M>
        where M : class
    {
        WhereD<M> WhereSegment { get; }
    }

    internal interface IWhereU<M>
        where M : class
    {
        WhereU<M> WhereSegment { get; }
    }

    internal interface IWhereQ<M>
        where M : class
    {
        WhereQ<M> WhereSegment { get; }
    }

    internal interface IWhereX
    {
        WhereX WhereSegment { get; }
    }
}
