using HPC.DAL.Core.Bases;
using HPC.DAL.Impls;
using HPC.DAL.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HPC.DAL.UserFacade.Create
{
    /// <summary>
    /// 请参阅: <see langword="目录索引 https://www.cnblogs.com/Meng-NET/"/>
    /// </summary>
    public sealed class Creater<M>
        : Operator
        , ICreateAsync<M>, ICreate<M>
        , ICreateBatchAsync<M>, ICreateBatch<M>
        where M : class
    {
        internal Creater(Context dc)
            : base(dc)
        { }

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public async Task<int> CreateAsync(M m, IDbTransaction tran = null)
        {
            return await new CreateAsyncImpl<M>(DC).CreateAsync(m, tran);
        }

        /// <summary>
        /// 插入单条数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public int Create(M m, IDbTransaction tran = null)
        {
            return new CreateImpl<M>(DC).Create(m, tran);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public async Task<int> CreateBatchAsync(IEnumerable<M> mList, IDbTransaction tran = null)
        {
            return await new CreateBatchAsyncImpl<M>(DC).CreateBatchAsync(mList, tran);
        }

        /// <summary>
        /// 批量插入数据
        /// </summary>
        /// <returns>插入条目数</returns>
        public int CreateBatch(IEnumerable<M> mList, IDbTransaction tran = null)
        {
            return new CreateBatchImpl<M>(DC).CreateBatch(mList, tran);
        }

    }
}
