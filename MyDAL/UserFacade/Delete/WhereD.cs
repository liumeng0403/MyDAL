using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Bases.Facades;
using HPC.DAL.Impls.ImplAsyncs;
using HPC.DAL.Impls.ImplSyncs;
using HPC.DAL.Interfaces.IAsyncs;
using HPC.DAL.Interfaces.ISyncs;
using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.UserFacade.Delete
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class WhereD<M>
        : WhereBase
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
        public async Task<int> DeleteAsync(IDbTransaction tran = null)
        {
            return await new DeleteAsyncImpl<M>(DC).DeleteAsync(tran);
        }

        /// <summary>
        /// 单表数据删除
        /// </summary>
        /// <returns>删除条目数</returns>
        public int Delete(IDbTransaction tran = null)
        {
            return new DeleteImpl<M>(DC).Delete(tran);
        }

    }
}
