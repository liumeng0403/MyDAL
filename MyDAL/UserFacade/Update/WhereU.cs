using MyDAL.Core.Bases;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Implers;

namespace MyDAL.UserFacade.Update
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class WhereU<M>
        : Operator
        , IUpdate<M>
        where M : class
    {
        internal WhereU(Context dc)
            : base(dc)
        { }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/
        
        /// <summary>
        /// 请参阅: <see langword=".Update() 之 .Set() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public int Update()
        {
            return new UpdateImpl<M>(DC).Update();
        }
    }
}
