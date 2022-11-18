using MyDAL.UserFacade.Update;

namespace MyDAL.Impls.Constraints.Segments
{
    internal interface ISetU<M>
        where M : class
    {
        SetU<M> SetSegment { get; }
    }
}
