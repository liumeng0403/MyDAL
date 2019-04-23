using HPC.DAL.Core.Bases;
using HPC.DAL.Core.Bases.Facades;
using HPC.DAL.Impls;
using HPC.DAL.Impls.ImplAsyncs;
using HPC.DAL.Impls.ImplSyncs;
using HPC.DAL.Interfaces.IAsyncs;
using HPC.DAL.Interfaces.ISyncs;
using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.UserFacade.Update
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class WhereU<M>
        : WhereBase
        , IUpdateAsync<M>, IUpdate<M>
        where M : class
    {
        internal WhereU(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 之 .Set() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public async Task<int> UpdateAsync(IDbTransaction tran = null,SetEnum set = SetEnum.AllowedNull)
        {
            return await new UpdateAsyncImpl<M>(DC).UpdateAsync(tran,set);
        }

        /// <summary>
        /// 请参阅: <see langword=".UpdateAsync() 之 .Set() 使用 https://www.cnblogs.com/Meng-NET/"/>
        /// </summary>
        public int Update(IDbTransaction tran = null,SetEnum set = SetEnum.AllowedNull)
        {
            return new UpdateImpl<M>(DC).Update(tran,set);
        }
    }
}
