using MyDAL.Core.Bases;
using MyDAL.Impls;
using MyDAL.Interfaces;
using System.Threading.Tasks;

namespace MyDAL.UserFacade.Delete
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class WhereD<M>
        : Operator
        , IDeleteAsync, IDelete
        where M : class
    {
        internal WhereD(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 单表数据删除
        /// </summary>
        /// <returns>删除条目数</returns>
        public async Task<int> DeleteAsync()
        {
            return await new DeleteAsyncImpl<M>(DC).DeleteAsync();
        }

        /// <summary>
        /// 单表数据删除
        /// </summary>
        /// <returns>删除条目数</returns>
        public int Delete()
        {
            return new DeleteImpl<M>(DC).Delete();
        }

    }
}
