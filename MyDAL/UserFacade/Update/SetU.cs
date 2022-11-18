using System;
using MyDAL.Core.Bases;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Constraints.Segments;
using MyDAL.Impls.Implers;

namespace MyDAL.UserFacade.Update
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class SetU<M>
        : Operator
        , IWhereU<M>
        , IUpdate<M>
        where M : class
    {

        internal SetU(Context dc)
            : base(dc)
        { }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public WhereU<M> WhereSegment
        {
            get
            {
                return new WhereU<M>(DC);
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 请参阅: <see langword=".Update() 之 .Set() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        [Obsolete("警告：此 API 会更新表中所有数据！！！", false)]
        public int Update()
        {
            return new UpdateImpl<M>(DC).Update();
        }
    }
}
