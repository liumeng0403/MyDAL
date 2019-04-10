using HPC.DAL.Core.Bases;
using HPC.DAL.Interfaces.Segments;

namespace HPC.DAL.UserFacade.Update
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class Updater<M> 
        : Operator
        , ISetU<M>
        where M : class
    {
        internal Updater(Context dc)
            : base(dc)
        { }

        public SetU<M> SetSegment
        {
            get
            {
                return new SetU<M>(DC);
            }
        }

    }
}
