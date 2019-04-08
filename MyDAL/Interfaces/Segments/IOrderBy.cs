using HPC.DAL.UserFacade.Join;
using HPC.DAL.UserFacade.Query;

namespace HPC.DAL.Interfaces.Segments
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
