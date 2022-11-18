using System;
using MyDAL.Core.Bases;
using MyDAL.Impls.Constraints.Methods;
using MyDAL.Impls.Constraints.Segments;
using MyDAL.Impls.Implers;

namespace MyDAL.UserFacade.Delete
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class Deleter<M>
        : Operator
        , IWhereD<M>
        , IDelete
        where M : class
    {
        internal Deleter(Context dc)
            : base(dc)
        { }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        public WhereD<M> WhereSegment
        {
            get
            {
                return new WhereD<M>(DC);
            }
        }

        /*-------------------------------------------------------------------------------------------------------------------------------------------------------------*/

        /// <summary>
        /// 单表数据删除
        /// </summary>
        [Obsolete("警告：此 API 会删除表中所有数据！！！", false)]
        public int Delete()
        {
            return new DeleteImpl<M>(DC).Delete();
        }
    }
}
