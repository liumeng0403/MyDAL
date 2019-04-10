using HPC.DAL.UserFacade.Update;

namespace HPC.DAL.Interfaces.Segments
{
    internal interface ISetU<M>
        where M : class
    {
        SetU<M> SetSegment { get; }
    }
}
