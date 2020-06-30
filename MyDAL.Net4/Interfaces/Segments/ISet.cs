using MyDAL.UserFacade.Update;

namespace MyDAL.Interfaces.Segments
{
    internal interface ISetU<M>
        where M : class
    {
        SetU<M> SetSegment { get; }
    }
}
