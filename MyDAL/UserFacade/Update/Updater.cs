using HPC.DAL.Core.Bases;

namespace HPC.DAL.UserFacade.Update
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class Updater<M> 
        : Operator
    {
        internal Updater(Context dc)
            : base(dc)
        { }
    }
}
