using HPC.DAL.UserFacade.Delete;
using HPC.DAL.UserFacade.Join;
using HPC.DAL.UserFacade.Query;
using HPC.DAL.UserFacade.Update;

namespace HPC.DAL.Interfaces.Segments
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
