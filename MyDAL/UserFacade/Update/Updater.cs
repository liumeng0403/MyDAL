using MyDAL.Core.Bases;
using MyDAL.Impls.Constraints.Segments;

namespace MyDAL.UserFacade.Update
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

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 之 .SetSegment 根据条件 动态设置 要更新的字段 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public SetU<M> SetSegment
        {
            get
            {
                return new SetU<M>(DC);
            }
        }

    }
}
